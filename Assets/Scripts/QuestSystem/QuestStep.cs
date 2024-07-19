using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;
    protected QuestManager manager;
    protected Action<string> advance;

    public virtual void Init(string questId, QuestManager manager, Action<string> action)
    {
        this.manager = manager;
        this.questId = questId;
        advance = action;
    }

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            // TODO - advance the quest forward now that we'he finished this step
            advance?.Invoke(questId);

            Destroy(this.gameObject);
        }
    }
}
