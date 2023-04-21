using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public List<Emotion> emotions;
    public IngredientManager ingredientManager;
    // Start is called before the first frame update
    void Start()
    {
        InitEmotions();
    }
    /// <summary>
    /// emotion을 읽어온다.
    /// </summary>
    private void InitEmotions()
    {
        if (File.Exists(Application.persistentDataPath + "/" + "BaseEmotions"))
        {
            string jsonFile = File.ReadAllText(Application.persistentDataPath + "/" + "BaseEmotions");
            emotions = JsonUtility.FromJson<UtilClasses.SerializationList<Emotion>>(jsonFile).ToList();
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
        for (int i = 0; i < ingredientManager.vegetableIngredients.Count; i++)
        {
            vegetableInt.Add(i);
        }
        List<int> vegetableIntRand = new List<int>();
        int hateVegetable = Random.Range(0, ingredientManager.vegetableIngredients.Count);
        for (int i = 0; i < hateVegetable; i++)
        {
            int temp = Random.Range(0, vegetableInt.Count);
            int tempRand = vegetableInt[temp];
            vegetableIntRand.Add(tempRand);
            vegetableInt.Remove(tempRand);
        }

        string preferMain = ingredientManager.mainIngredients[mainRand].IngredientName;
        string preferBread = ingredientManager.breadIngredients[breadRand].IngredientName;
        List<string> unlikeVegetables = new List<string>();
        foreach (int i in vegetableIntRand)
        {
            unlikeVegetables.Add(ingredientManager.vegetableIngredients[i].IngredientName);
        }
        if (unlikeVegetables.Contains("None") || unlikeVegetables.Count == 0)
        {
            unlikeVegetables.Clear();
            unlikeVegetables.Add("None");
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
        int randEmotionNum = Random.Range(1, 4);
        List<ParentEmotion> preferParentEmotion = new List<ParentEmotion>();
        List<string> preferEmotion = new List<string>();
        for (int i = 0; i < randEmotionNum; i++)
        {
            int temp = Random.Range(0, tempEmotionsNum.Count);
            preferParentEmotion.Add(emotions[temp].parentEmotion);
            tempEmotionsNum.Remove(temp);
            int childEmotionRand = Random.Range(0, emotions[temp].childEmotion.Count);
            preferEmotion.Add(emotions[temp].childEmotion[childEmotionRand]);
        }
        return new Order(preferMain, preferBread, unlikeVegetables, preferCheeses, preferParentEmotion, preferEmotion);
    }
}
