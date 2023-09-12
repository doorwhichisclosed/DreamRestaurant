using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerDataContainer
{
    public PlayerData playerData;
    public List<Ingredient> ingredients;
    private Dictionary<string, Sprite> ingredientsIcon;
    public Dictionary<string, Sprite> IngredientsIcon { get { return ingredientsIcon; } }
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

    public async UniTask LoadPlayerData()
    {

        if (ES3.KeyExists("PlayerData"))
            playerData = ES3.Load("PlayerData") as PlayerData;
        if (null == playerData)
            playerData = new PlayerData();
        if (ingredients == null)
        {
            ingredients= new List<Ingredient>();
            List<Dictionary<string, object>> ingredientsData = new List<Dictionary<string, object>>();
            ingredientsData = await UtilClasses.CSVReader.Read("Ingredients");
            foreach (var data in ingredientsData)
            {
                Ingredient ingredient = new Ingredient((string)data["Name"], (string)data["Description"], (string)data["Category"], ((int)data["Cost"]),
                    ((int)data["Condition"]), (string)data["Address"]);
                ingredients.Add(ingredient);
            }
            ingredientsIcon = new Dictionary<string, Sprite>();
            foreach(var i in ingredients)
            {
                UniTask<Sprite> asycSprite
                    =Addressables.LoadAssetAsync<Sprite>(i.IngredientAddress).Task.AsUniTask<Sprite>();
                Sprite sprite=await asycSprite;
                ingredientsIcon.Add(i.IngredientName, sprite);
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
                if (playerData.moneyNum + value < 0)
                    return;
                playerData.moneyNum += value;
                GameObject.FindObjectOfType<Coin>().SetCoinText(PlayerDataContainer.instance.playerData.moneyNum);
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

    public async void InitStat()
    {
        playerData = new PlayerData();
        SavePlayerData();
        await LoadPlayerData();
    }
}
