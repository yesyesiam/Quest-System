using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
    private int coinsToComplete = 6;
    public override void Init(string questId, QuestManager manager, Action<string> action)
    {
        base.Init(questId, manager, action);
        CheckBalance(manager.Wallet.currentCoins);
        manager.Wallet.CoinsChanged += CheckBalance;
    }

    /*private void OnEnable()
    {
        manager.Wallet.CoinsChanged += CheckBalance;
    }*/

    private void OnDisable()
    {
        manager.Wallet.CoinsChanged -= CheckBalance;
    }

    private void CheckBalance(int coins)
    {
        if (coins >= coinsToComplete)
        {
            FinishQuestStep();
        }
    }
}
