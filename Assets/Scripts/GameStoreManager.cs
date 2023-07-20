using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class GameStoreManager : MonoBehaviour
{
    public List<Emotion> emotions;
    public IngredientManager ingredientManager;
    private Order order;
    public Order Order { get { return order; } }
    public TextMeshProUGUI orderText;
    public GameObject orderPage;
    private List<Dictionary<string, object>> orderSentences;
    // Start is called before the first frame update
    async void Start()
    {

        InitEmotions();
        if (orderSentences == null)
        {
            orderSentences = new List<Dictionary<string, object>>();
            orderSentences = await UtilClasses.CSVReader.Read("OrderSentences");
        }
    }
    /// <summary>
    /// emotion을 읽어온다.
    /// </summary>
    private void InitEmotions()
    {
        if (File.Exists(Application.persistentDataPath + "/" + "BaseEmotions"))
        {
            string jsonFile = File.ReadAllText(Application.persistentDataPath +
                "/" + "BaseEmotions");
            emotions = JsonUtility.
                FromJson<UtilClasses.SerializationList<Emotion>>(jsonFile).ToList();
        }
    }
    /// <summary>
    /// 주문을 만든다
    /// </summary>
    /// <returns></returns>
    public Order MakeOrder()
    {
        //for main
        int mainRand = Random.Range(0, ingredientManager.mainIngredients.Count);
        //for bread
        int breadRand = Random.Range(0, ingredientManager.breadIngredients.Count);
        //for cheese
        List<int> cheeseInt = new List<int>();
        for (int i = 0; i < ingredientManager.cheeseIngredients.Count; i++)
        {
            cheeseInt.Add(i);
        }
        List<int> cheeseIntRand = new List<int>();
        for (int i = 0; i < 2; i++)
        {
            int temp = Random.Range(0, cheeseInt.Count);
            int tempRand = cheeseInt[temp];
            cheeseIntRand.Add(tempRand);
            cheeseInt.Remove(tempRand);
        }
        //for vegetable
        List<int> vegetableInt = new List<int>();
        for (int i = 1; i < ingredientManager.vegetableIngredients.Count; i++)
        {
            vegetableInt.Add(i);
        }
        List<int> vegetableIntRand = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int temp = Random.Range(0, vegetableInt.Count);
            int tempRand = vegetableInt[temp];
            vegetableIntRand.Add(tempRand);
            vegetableInt.Remove(tempRand);
        }

        string preferMain = ingredientManager.mainIngredients[mainRand].IngredientName;
        string preferBread = ingredientManager.breadIngredients[breadRand].IngredientName;
        List<string> preferVegetables = new List<string>();
        foreach (int i in vegetableIntRand)
        {
            preferVegetables.Add(ingredientManager.vegetableIngredients[i].IngredientName);
        }
        List<string> preferCheeses = new List<string>();
        foreach (int i in cheeseIntRand)
        {
            preferCheeses.Add(ingredientManager.cheeseIngredients[i].IngredientName);
        }
        //for Sauce
        List<int> tempEmotionsNum = new List<int>();
        for (int i = 0; i < emotions.Count; i++)
        {
            tempEmotionsNum.Add(i);
        }
        int tempEmotionInt = Random.Range(0, tempEmotionsNum.Count);
        ParentEmotion preferParentEmotion = emotions[tempEmotionInt].parentEmotion;
        int childEmotionRand = Random.Range(0, emotions[tempEmotionInt].childEmotion.Count);
        string preferEmotion = emotions[tempEmotionInt].childEmotion[childEmotionRand];

        return new Order(preferMain, preferBread, preferVegetables,
            preferCheeses, preferParentEmotion, preferEmotion);
    }

    public void SetOrder()
    {
        orderPage.SetActive(true);
        order = MakeOrder();
        StringBuilder sb = new StringBuilder();
        sb.Append(orderSentences[0]["Sentences"].ToString());
        sb.Replace("{Main}", order.preferMain);
        sb.Replace("{Bread}", order.preferBread);
        StringBuilder vegetableSB = new StringBuilder();
        for (int i = 0; i < order.preferVegetables.Count; i++)
        {
            vegetableSB.Append(order.preferVegetables[i]);
            if (i == order.preferVegetables.Count - 1)
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
        sb.Replace("{Child Emotion}", order.preferEmotion);
        orderText.text = sb.ToString();
        sb.Clear();
    }
}
