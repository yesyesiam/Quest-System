using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLocalProvider : IDataProvider
{
    private IPersistentData _persistentData;
    private const string gameDataKey = "gameData";

    public DataLocalProvider(IPersistentData persistentData) => _persistentData = persistentData;
    public void Save()
    {
        try
        {
            // serialize using JsonUtility, but use whatever you want here (like JSON.NET)
            string serializedData = JsonUtility.ToJson(_persistentData.GameData);
            PlayerPrefs.SetString(gameDataKey, serializedData);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save quest with id  " + e);
        }
    }

    public bool TryLoad()
    {
        try
        {
            if (IsDataAlreadyExist() == false)
            {
                return false;
            }
            else
            {
                string serializedData = PlayerPrefs.GetString(gameDataKey);
                GameData gameData = JsonUtility.FromJson<GameData>(serializedData);

                _persistentData.GameData = gameData;

                return true;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load quest with id " + e);
            return false;
        }
    }

    private bool IsDataAlreadyExist() => PlayerPrefs.HasKey(gameDataKey);
}
