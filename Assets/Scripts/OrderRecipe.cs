using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class OrderRecipe : MonoBehaviour
{
    public Order order;
    public TextMeshProUGUI orderText;
    public StoreManager storeManager;
    public GameObject orderPage;
    private List<Dictionary<string, object>> orderSentences;
    private void Start()
    {
        orderSentences = new List<Dictionary<string, object>>();
        orderSentences = UtilClasses.CSVReader.Read("OrderSentences");
    }
    /// <summary>
    /// 새로운 주문을 생성한 것을 orderRecipe로부터 받아온다.
    /// </summary>
    public void SetOrder()
    {
        orderPage.SetActive(true);
        order = storeManager.MakeOrder();
        StringBuilder sb = new StringBuilder();
        sb.Append(orderSentences[0]["Sentences"].ToString());
        sb.Replace("{Main}", order.preferMain);
        sb.Replace("{Bread}", order.preferBread);
        StringBuilder vegetableSB = new StringBuilder();
        for (int i = 0; i < order.unlikeVegetables.Count; i++)
        {
            vegetableSB.Append(order.unlikeVegetables[i]);
            if (i == order.unlikeVegetables.Count - 1)
                break;
            vegetableSB.Append(", ");
        }
        sb.Replace("{Unlike Vegetables}", vegetableSB.ToString());
        StringBuilder cheeseSB = new StringBuilder();
        for (int i = 0; i < order.preferCheeses.Count; i++)
        {
            cheeseSB.Append(order.preferCheeses[i]);
            if (i == order.preferCheeses.Count - 1)
                break;
            cheeseSB.Append(", ");
        }
        sb.Replace("{Cheese}", cheeseSB.ToString());
        StringBuilder childEmotionSB = new StringBuilder();
        for (int i = 0; i < order.preferEmotion.Count; i++)
        {
            childEmotionSB.Append(order.preferEmotion[i]);
            if (i == order.preferEmotion.Count - 1)
                break;
            childEmotionSB.Append(", ");
        }
        sb.Replace("{Child Emotion}", childEmotionSB.ToString());
        orderText.text = sb.ToString();
        sb.Clear();
    }
}
