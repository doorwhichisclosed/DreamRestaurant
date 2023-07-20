public class PlayerDataContainer
{
    public PlayerData playerData;
    private static PlayerDataContainer instance;
    public static PlayerDataContainer Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new PlayerDataContainer();
            }
            return instance;
        }
    }

    public PlayerDataContainer()
    {
        LoadPlayerData();
    }

    public void LoadPlayerData()
    {
        playerData = ES3.Load("PlayerData", new PlayerData()) as PlayerData;
    }

    public void SavePlayerData()
    {
        ES3.Save("PlayerData", playerData);
    }

    public void ChangeValue(string key, int value, bool isSaveNow)
    {
        switch (key)
        {
            case "PlayTime":
                playerData.playTimeLevel += value;
                break;
            case "FoodEfficiency":
                playerData.foodEfficiencyLevel += value;
                break;
            case "FoodCost":
                playerData.foodCostLevel += value;
                break;
            case "Money":
                playerData.moneyNum += value;
                break;
            case "RealMoney":
                playerData.realMoneyNum += value;
                break;
            case "CurStoryNum":
                playerData.curStoryNum += value;
                break;
        }
        if (isSaveNow)
        {
            SavePlayerData();
        }
    }

    public void InitStat()
    {
        playerData = new PlayerData();
        SavePlayerData();
        LoadPlayerData();
    }
}
