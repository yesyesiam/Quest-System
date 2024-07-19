using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleQuestStep : QuestStep
{
    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    private int stepsToComplete;
    private string questId;

    public override void Init(string questId, QuestManager manager, Action<string> action)
    {
        base.Init(questId, manager, action);
        this.questId = questId;
        stepsToComplete = 0;
        InstantiateCurrentQuestStep(transform);
    }
    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        foreach (var questStepPrefab in questStepPrefabs)
        {
            if (questStepPrefab != null)
            {
                QuestStep questStep = UnityEngine.Object.Instantiate<GameObject>(questStepPrefab, parentTransform, true)
                    .GetComponent<QuestStep>();
                questStep.Init(questId, manager, CompleteQuest);
            }
        }
    }

    private void CompleteQuest(string id)
    {
        stepsToComplete++;
        Debug.Log(stepsToComplete);
        if(stepsToComplete >= questStepPrefabs.Length)
        {
            FinishQuestStep();
        }
    }
}
