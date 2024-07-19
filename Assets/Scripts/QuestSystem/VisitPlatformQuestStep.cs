using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitPlatformQuestStep : QuestStep
{
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }
}
