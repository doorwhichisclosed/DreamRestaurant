using Cysharp.Threading.Tasks;
using System.Collections.Generic;


public class PlayerDataContainer
{
    public PlayerData playerData;
    public List<Ingredient> ingredients;
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

    public async void LoadPlayerData()
    {
        playerData = ES3.Load("PlayerData", new PlayerData()) as PlayerData;
        if (ingredients == null)
        {
            ingredients= new List<Ingredient>();
            List<Dictionary<string, object>> ingredientsData = new List<Dictionary<string, object>>();
            ingredientsData = await UtilClasses.CSVReader.Read("Ingredients");
            foreach (var data in ingredientsData)
            {
                Ingredient ingredient = new Ingredient((string)data["Name"], (string)data["Description"], (string)data["Category"], ((int)data["Cost"]),
                    ((int)data["Condition"]));
                ingredients.Add(ingredient);
            }
            UnityEngine.Debug.Log(ingredients.Count);
        }
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
