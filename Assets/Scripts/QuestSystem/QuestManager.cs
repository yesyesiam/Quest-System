using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private IDataProvider _dataProvider;
    private IPersistentData _persistentGameData;

    public event Action QuestChanged;
    private Wallet _wallet;
    public Wallet Wallet => _wallet;

    [SerializeField] private QuestInfoSO[] allQuests;

    private Dictionary<string, Quest> questMap;

    public void Initialize(IDataProvider dataProvider, IPersistentData persistentData, Wallet wallet)
    {
        _dataProvider = dataProvider;
        _persistentGameData = persistentData;

        _wallet = wallet;

        questMap = CreateQuestMap();
        LoadQuests();

        UpdateQuestStates();
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] Quests = allQuests;

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in Quests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, MakeQuest(questInfo));
        }
        return idToQuestMap;
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        if (quest.state != QuestState.FINISHED)
        {
            quest.InstantiateCurrentQuestStep(this.transform, this);
            quest.state = QuestState.IN_PROGRESS;
        }
    }

    public Quest GetQuestById(string id)
    {
        Quest quest = questMap.GetValueOrDefault(id);
        if (quest == null)
        {
            Debug.LogError("ID not found in the Quest Map: " + id);
        }
        return quest;
    }

    public void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform, this);
        }
        else
        {
            FinishQuest(quest.info.id);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.state = QuestState.FINISHED;

        UpdateQuestStates();

        QuestChanged?.Invoke();
        SaveQuest(quest);
    }

    private void UpdateQuestStates()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                StartQuest(quest.info.id);
            }
        }
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;

        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private Quest MakeQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;
        
        quest = new Quest(questInfo);
        return quest;
    }

    private void LoadQuests()
    {
        if (_persistentGameData.GameData.quests!=null && _persistentGameData.GameData.quests.Count>0)
        {
            var savedQuests = _persistentGameData.GameData.quests;
            foreach (var questData in savedQuests)
            {
                var q = questMap.GetValueOrDefault(questData.questId);
                if (q != null)
                {
                    q.state = questData.state;
                }
            }
        }
    }

    private void SaveQuest(Quest quest)
    {
        var newQuestData = new QuestData();

        newQuestData.questId = quest.info.id;
        newQuestData.state = quest.state;

        if (_persistentGameData.GameData.quests == null)
        {
            _persistentGameData.GameData.quests = new List<QuestData>();
            _persistentGameData.GameData.quests.Add(newQuestData);
        }
        else
        {
            var index = _persistentGameData.GameData.quests.FindIndex(x => x.questId == quest.info.id);
            if (index == -1)
            {
                _persistentGameData.GameData.quests.Add(newQuestData);
            }
            else{
                _persistentGameData.GameData.quests[index] = newQuestData;
            }
        }
        _dataProvider.Save();
    }

    public IEnumerable<Quest> GetAll()
    {
        return questMap.Values.Where(x=>x.state!=QuestState.REQUIREMENTS_NOT_MET).Reverse();
    }
}
