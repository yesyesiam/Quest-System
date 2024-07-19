using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] QuestItemUI slotPrefab;

    [SerializeField] QuestManager questManager;

    [SerializeField] QuestLogUI questLog;

    private QuestItemUI selectedQuestView;
    private string selectedQuestId;

    private void RenderAll()
    {
        Clear();
        QuestItemUI slot;
        foreach (var quest in questManager.GetAll())
        {
            if (quest != null)
            {
                slot = Instantiate(slotPrefab, transform);
                slot.Initialize(quest.info, SelectQuest, quest.state == QuestState.FINISHED);

                if (quest.info.id == selectedQuestId)
                {
                    SelectQuest(slot);
                }
            }
        }
    }

    private void SelectQuest(QuestItemUI questViewItem)
    {
        var quest = questManager.GetQuestById(questViewItem.questId);
        questLog.SetQuestLogInfo(quest);


        if (selectedQuestView != null)
        {
            selectedQuestView.UnHighlight();
        }
        selectedQuestView = questViewItem;
        selectedQuestId = questViewItem.questId;
        questViewItem.Highlight();
    }

    private void Clear()
    {
        questLog.SetEmpty();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnEnable()
    {
        questManager.QuestChanged += RenderAll;
        RenderAll();
    }
    private void OnDisable()
    {
        questManager.QuestChanged -= RenderAll;
        Clear();
    }
}
