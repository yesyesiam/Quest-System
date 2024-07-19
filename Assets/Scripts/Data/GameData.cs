using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public List<QuestData> quests;
    public int money;
    public GameData(int money, List<QuestData> quests=null)
    {
        this.quests = quests;
        this.money = money;
    }
}
