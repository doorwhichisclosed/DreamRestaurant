using TMPro;
using UnityEngine;

public class UpgradePage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playTimeLevelText;
    [SerializeField] private TextMeshProUGUI foodEfficiencyLevelText;
    [SerializeField] private TextMeshProUGUI foodCostLevelText;

    private void Start()
    {
        PlayerDataContainer.Instance.InitStat();
        UpgradeText("PlayTime");
        UpgradeText("FoodEfficiency");
        UpgradeText("FoodCost");
    }
    public void UpgradeStat(string key)
    {
        float tempMoneyValue = 0;
        switch (key)
        {
            case "PlayTime":
                tempMoneyValue = 500 * (Mathf.Pow(1.1f,
                    PlayerDataContainer.Instance.playerData.playTimeLevel));
                break;
            case "FoodEfficiency":
                tempMoneyValue = 500 * (Mathf.Pow(1.1f,
                    PlayerDataContainer.Instance.playerData.foodEfficiencyLevel));
                break;
            case "FoodCost":
                tempMoneyValue = 500 * (Mathf.Pow(1.1f,
                    PlayerDataContainer.Instance.playerData.foodCostLevel));
                break;
        }
        int moneyValue = Mathf.FloorToInt(tempMoneyValue);
        PlayerDataContainer.Instance.ChangeValue(key, 1, false);
        PlayerDataContainer.Instance.ChangeValue("Money", -moneyValue, true);
        UpgradeText(key);
    }

    public void UpgradeText(string key)
    {
        float tempMoneyValue = 0;
        switch (key)
        {
            case "PlayTime":
                tempMoneyValue = 500 * (Mathf.Pow(1.1f,
                    PlayerDataContainer.Instance.playerData.playTimeLevel));
                break;
            case "FoodEfficiency":
                tempMoneyValue = 500 * (Mathf.Pow(1.1f,
                    PlayerDataContainer.Instance.playerData.foodEfficiencyLevel));
                break;
            case "FoodCost":
                tempMoneyValue = 500 * (Mathf.Pow(1.1f,
                    PlayerDataContainer.Instance.playerData.foodCostLevel));
                break;
        }
        int moneyValue = Mathf.FloorToInt(tempMoneyValue);
        switch (key)
        {
            case "PlayTime":
                string s = "PlayTime: " +
                    PlayerDataContainer.Instance.playerData.playTimeLevel +
                    "\nUpgrade Cost: " + moneyValue;
                playTimeLevelText.text = s;
                break;
            case "FoodEfficiency":
                s = "FoodEfficiency: " +
                    PlayerDataContainer.Instance.playerData.foodEfficiencyLevel +
                                    "\nUpgrade Cost: " + moneyValue;
                foodEfficiencyLevelText.text = s;
                break;
            case "FoodCost":
                s = "FoodCost: " +
                    PlayerDataContainer.Instance.playerData.foodCostLevel +
                                    "\nUpgrade Cost: " + moneyValue;
                foodCostLevelText.text = s;
                break;
        }
    }
}
