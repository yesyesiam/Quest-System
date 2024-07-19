using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuestInitializer : MonoBehaviour
{
    [SerializeField] private QuestInfoSO questInfo;
    private QuestManager manager;
    public void Initialize(QuestManager manager)
    {
        this.manager = manager;


        var quest = manager.GetQuestById(questInfo.id);

        if(quest!=null && quest.state == QuestState.IN_PROGRESS)
        {
            Debug.Log("kill the boss !!!");
        }
        else
        {
            Debug.Log("you can not kill the boss at the moment !!!");
        }  
    }
}
