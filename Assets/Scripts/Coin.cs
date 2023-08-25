using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    // Start is called before the first frame update
    private void Start()
    {
        SetCoinText(PlayerDataContainer.Instance.playerData.moneyNum);
    }
    public void SetCoinText(int coinNum)
    {
        coinText.text = coinNum.ToString();
    }

    public void RewardAfterAD()
    {
        PlayerDataContainer.Instance.ChangeValue("Money", (int)(500 * (1 + 0.1f * PlayerDataContainer.Instance.playerData.curStoryNum)), true);
    }
}
