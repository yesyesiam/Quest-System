using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBootstrap : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private QuestManager _questManager;

    [Header("Configuration")]
    [SerializeField] private int startingCoins = 5;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentGameData;

    private void Awake()
    {
        InitializeData();

        _wallet.Initialize(_dataProvider, _persistentGameData);

        _questManager.Initialize(_dataProvider, _persistentGameData, _wallet);
    }

    private void InitializeData()
    {
        _persistentGameData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentGameData);

        if (_dataProvider.TryLoad() == false)
            _persistentGameData.GameData = new GameData(startingCoins, new List<QuestData>());
    }
}
