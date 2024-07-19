using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<int> CoinsChanged;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentGameData;

    public int currentCoins { get; private set; }

    public void Initialize(IDataProvider dataProvider, IPersistentData persistentGameData)
    {
        _dataProvider = dataProvider;
        _persistentGameData = persistentGameData;

        currentCoins = _persistentGameData.GameData.money;
    }

    public void AddCoins(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        currentCoins += coins;

        ChangeCoinsAmmount(currentCoins);
    }

    public void Spend(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        currentCoins -= coins;

        ChangeCoinsAmmount(currentCoins);
    }

    private void ChangeCoinsAmmount(int newAmmount)
    {
        CoinsChanged?.Invoke(currentCoins);
        _persistentGameData.GameData.money = newAmmount;
        _dataProvider.Save();
    }
}
