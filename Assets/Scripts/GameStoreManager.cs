using System.Collections.Generic;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Collections;

public class GameStoreManager : MonoBehaviour
{
    [SerializeField] private Stamina stamina;
    [SerializeField] private GameObject mainGameObject;
    [SerializeField] private GameObject endOfGameObject;
    [SerializeField] private GameObject fireflyParticle;
    [SerializeField] private SwipeUI swipeUI;
    [SerializeField] private Customer customerImage;
    public TextMeshProUGUI orderText;
    public GameObject orderPage;
    public Button orderPageButton;
    public GameObject SelectFoodPage;
    public List<GameObject> foodObject;
    private List<Dictionary<string, object>> orderSentences;
    [SerializeField] private List<Ingredient> breadIngredient;
    private List<Ingredient> vegetableIngredient;
    private List<Ingredient> mainIngredient;
    private List<Ingredient> cheeseIngredient;
    private List<Ingredient> sauceIngredient;
    private Sandwich sandwichOrder;
    private Sandwich sandwichResult;
    public TextMeshProUGUI sauceText;
    public Plate plate;
    public async UniTask Init()
    {
        if (orderSentences == null)
        {
            orderSentences = new List<Dictionary<string, object>>();
            orderSentences = await UtilClasses.CSVReader.Read("OrderSentences");
        }
        breadIngredient = new List<Ingredient>();
        vegetableIngredient = new List<Ingredient>();
        mainIngredient = new List<Ingredient>();
        cheeseIngredient = new List<Ingredient>();
        sauceIngredient = new List<Ingredient>();
        foreach (var i in PlayerDataContainer.Instance.ingredients)
        {
            if (i.Condition <= PlayerDataContainer.Instance.playerData.curStoryNum)
            {
                if (i.IngredientCategory == IngredientCategory.Bread)
                {
                    breadIngredient.Add(i);
                }
                else if (i.IngredientCategory == IngredientCategory.Vegetable)
                {
                    vegetableIngredient.Add(i);
                }
                else if (i.IngredientCategory == IngredientCategory.Main)
                {
                    mainIngredient.Add(i);
                }
                else if (i.IngredientCategory == IngredientCategory.Cheese)
                {
                    cheeseIngredient.Add(i);
                }
                else
                {
                    sauceIngredient.Add(i);
                }
            }
        }
        List<int> elist = new List<int>();
        for (int i = 1; i < 9; i++)
        {
            elist.Add(i);
        }
        foreach(var e in sauceIngredient)
        {
            int rand = elist[Random.Range(0, elist.Count)];
            e.SelectEmotion(rand);
            elist.Remove(rand);
        }
        sauceText.text=string.Format("{0}: {1}\n{2}: {3}\n{4}: {5}\n{6}: {7}\n{8}: {9}\n{10}: {11}\n{12}: {13}\n{14}: {15}", sauceIngredient[0].IngredientName, sauceIngredient[0].Emotion.ToString(), sauceIngredient[1].IngredientName, sauceIngredient[1].Emotion.ToString(), sauceIngredient[2].IngredientName, sauceIngredient[2].Emotion.ToString(), sauceIngredient[3].IngredientName, sauceIngredient[3].Emotion.ToString(), sauceIngredient[4].IngredientName, sauceIngredient[4].Emotion.ToString(), sauceIngredient[5].IngredientName, sauceIngredient[5].Emotion.ToString(), sauceIngredient[6].IngredientName, sauceIngredient[6].Emotion.ToString(), sauceIngredient[7].IngredientName, sauceIngredient[7].Emotion.ToString());
    }
    public void MakeOrder()
    {
        int breadRandNum;
        breadRandNum = Random.Range(0, breadIngredient.Count);
        int vegeRandNum;
        vegeRandNum = Random.Range(0, vegetableIngredient.Count);
        int mainRandNum;
        mainRandNum = Random.Range(0, mainIngredient.Count);
        int cheeseRandNum;
        cheeseRandNum = Random.Range(0, cheeseIngredient.Count);
        int sauceRandNum;
        sauceRandNum = Random.Range(0, 8);
        sandwichOrder = new Sandwich(breadIngredient[breadRandNum], vegetableIngredient[vegeRandNum],
            mainIngredient[mainRandNum], cheeseIngredient[cheeseRandNum], sauceIngredient[sauceRandNum]);
    }
    private void SelectBreadPhase()
    {
        SelectFoodPage.SetActive(true);
        sandwichResult = new Sandwich();
        foreach(var food in foodObject)
        {
            food.GetComponentInChildren<Image>().sprite = null;
            food.SetActive(false);
        }
        for(int i=0;i<breadIngredient.Count;i++)
        {
            int x = i;
            foodObject[x].gameObject.SetActive(true);
            foodObject[x].GetComponentInChildren<DragNDropFood>().SetIngredient(breadIngredient[x]);
            foodObject[x].GetComponentInChildren<Image>().sprite=
                PlayerDataContainer.Instance.IngredientsIcon[foodObject[x].GetComponentInChildren<DragNDropFood>().Ingredient.IngredientName];
            Debug.Log(foodObject[x].GetComponentInChildren<Image>().sprite);
            foodObject[x].GetComponentInChildren<Image>().preserveAspect = true;
        }
        plate.OnDropIngredient.RemoveAllListeners();
        plate.OnDropIngredient.AddListener(i => SelectBread(i));
    }
    private void SelectMainPhase()
    {
        foreach (var food in foodObject)
        {
            food.GetComponentInChildren<Image>().sprite = null;
            food.SetActive(false);
        }
        for (int i = 0; i < mainIngredient.Count; i++)
        {
            int x = i;
            foodObject[x].gameObject.SetActive(true);
            foodObject[x].GetComponentInChildren<DragNDropFood>().SetIngredient(mainIngredient[x]);
            Debug.Log(PlayerDataContainer.Instance.IngredientsIcon[foodObject[x].GetComponentInChildren<DragNDropFood>().Ingredient.IngredientName]);
            foodObject[x].GetComponentInChildren<Image>().sprite = PlayerDataContainer.Instance.IngredientsIcon[foodObject[x].GetComponentInChildren<DragNDropFood>().Ingredient.IngredientName];
            foodObject[x].GetComponentInChildren<Image>().preserveAspect = true;
        }
        plate.OnDropIngredient.RemoveAllListeners();
        plate.OnDropIngredient.AddListener(i=>SelectMain(i));
    }
    private void SelectVegetablePhase()
    {
        foreach (var food in foodObject)
        {
            food.GetComponentInChildren<Image>().sprite = null;
            food.SetActive(false);
        }
        for (int i = 0; i < vegetableIngredient.Count; i++)
        {
            int x = i;
            foodObject[x].gameObject.SetActive(true);
            foodObject[x].GetComponentInChildren<DragNDropFood>().SetIngredient(vegetableIngredient[x]);
            foodObject[x].GetComponentInChildren<Image>().sprite = PlayerDataContainer.Instance.IngredientsIcon[foodObject[x].GetComponentInChildren<DragNDropFood>().Ingredient.IngredientName];
            foodObject[x].GetComponentInChildren<Image>().preserveAspect = true;
        }
        plate.OnDropIngredient.RemoveAllListeners();
        plate.OnDropIngredient.AddListener(i => SelectVegetable(i));
    }
    private void SelectCheesePhase()
    {
        foreach (var food in foodObject)
        {
            food.GetComponentInChildren<Image>().sprite = null;
            food.SetActive(false);
        }
        for (int i = 0; i < cheeseIngredient.Count; i++)
        {
            int x = i;
            foodObject[x].gameObject.SetActive(true);
            foodObject[x].GetComponentInChildren<DragNDropFood>().SetIngredient(cheeseIngredient[x]);
            foodObject[x].GetComponentInChildren<Image>().sprite = PlayerDataContainer.Instance.IngredientsIcon[foodObject[x].GetComponentInChildren<DragNDropFood>().Ingredient.IngredientName];
            foodObject[x].GetComponentInChildren<Image>().preserveAspect = true;
        }
        plate.OnDropIngredient.RemoveAllListeners();
        plate.OnDropIngredient.AddListener(i => SelectCheese(i));
    }
    private void SelectSaucePhase()
    {
        foreach (var food in foodObject)
        {
            food.GetComponentInChildren<Image>().sprite = null;
            food.SetActive(false);
        }
        for (int i = 0; i < sauceIngredient.Count; i++)
        {
            int x = i;
            foodObject[x].SetActive(true);
            foodObject[x].GetComponentInChildren<DragNDropFood>().SetIngredient(sauceIngredient[x]);
            foodObject[x].GetComponentInChildren<Image>().sprite = PlayerDataContainer.Instance.IngredientsIcon[foodObject[x].GetComponentInChildren<DragNDropFood>().Ingredient.IngredientName];
            foodObject[x].GetComponentInChildren<Image>().preserveAspect = true;
        }
        plate.OnDropIngredient.RemoveAllListeners();
        plate.OnDropIngredient.AddListener(i => SelectSauce(i));
    }
    public void SelectBread(Ingredient ingredient)
    {
        sandwichResult.SetBread(ingredient);
        SelectMainPhase();
    }
    public void SelectMain(Ingredient ingredient)
    {
        sandwichResult.SetMain(ingredient);
        SelectVegetablePhase();
    }
    public void SelectVegetable(Ingredient ingredient)
    {
        sandwichResult.SetVegetable(ingredient);
        SelectCheesePhase();
    }
    public void SelectCheese(Ingredient ingredient)
    {
        sandwichResult.SetCheese(ingredient);
        SelectSaucePhase();
    }

    public void SelectSauce(Ingredient ingredient)
    {
        sandwichResult.SetSauce(ingredient);
        CheckSandwich();
        foreach (var food in foodObject)
        {
            food.SetActive(false);
        }
        customerImage.gameObject.SetActive(true);
    }
    public void CheckSandwich()
    {
        if(!sandwichOrder.Bread.Equals(sandwichResult.Bread)
            ||!sandwichOrder.Main.Equals(sandwichResult.Main)
            ||!sandwichOrder.Vegetable.Equals(sandwichResult.Vegetable)
            ||!sandwichOrder.Cheese.Equals(sandwichResult.Cheese)
            ||!sandwichOrder.Sauce.Equals(sandwichOrder.Sauce)
            ) 
        {
            Debug.Log("틀림");
        }
        else
        {
            Debug.Log("맞음");
            PlayerDataContainer.Instance.ChangeValue("Money",(int)(50*(1+0.1f*PlayerDataContainer.Instance.playerData.curStoryNum)),true);
        }
        SetCustomer();
    }
    /// <summary>
    /// 게임 매니저로 씬 변경없이 UI변경으로 게임 시작
    /// </summary>
    public async void StartGame()
    {
        if (stamina.currentStamina < 1)
            return;
        stamina.UseStamina();
        fireflyParticle.SetActive(false);
        mainGameObject.SetActive(true);
        swipeUI.enabled = false;
        StartCoroutine(EndCoroutine());
        await Init();
        SetCustomer();
        FindAnyObjectByType<BGMPlayer>().PlayBGM(1);
    }
    public void EndGame()
    {
        
        endOfGameObject.SetActive(true);
        endOfGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        endOfGameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            mainGameObject.SetActive(false);
            fireflyParticle?.SetActive(true);
            swipeUI.enabled = true;
            endOfGameObject.SetActive(false);
            endOfGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            FindAnyObjectByType<BGMPlayer>().PlayBGM(0);
        });
        
    }
    public void SetCustomer()
    {
        MakeOrder();
        orderPage.SetActive(true);
        string temp = (string)orderSentences[0]["Sentences"];
        temp = temp.Replace("{Main}", sandwichOrder.Main.IngredientName);
        temp = temp.Replace("{Bread}", sandwichOrder.Bread.IngredientName);
        temp = temp.Replace("{Cheese}", sandwichOrder.Cheese.IngredientName);
        temp = temp.Replace("{Vegetable}", sandwichOrder.Vegetable.IngredientName);
        temp = temp.Replace("{Emotion}", sandwichOrder.Sauce.Emotion.ToString());
        orderText.text = string.Format("메인: {0}\n빵: {1}\n치즈: {2}\n채소: {3}\n감정: {4}", sandwichOrder.Main.IngredientName, sandwichOrder.Bread.IngredientName, sandwichOrder.Cheese.IngredientName, sandwichOrder.Vegetable.IngredientName, sandwichOrder.Sauce.Emotion.ToString());
        customerImage.SetCustomer();
        customerImage.GetComponent<Button>().onClick.RemoveAllListeners();
        customerImage.GetComponent<Button>().onClick.AddListener
            (() =>
            {
                SelectBreadPhase();
                orderPage.SetActive(false);
            });
    }
    private void OnApplicationQuit()
    {
        PlayerDataContainer.Instance.SavePlayerData();
    }
    IEnumerator EndCoroutine()
    {
        yield return new WaitForSeconds(60);
        EndGame();
    }
}
