using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStoreManager : MonoBehaviour
{
    [SerializeField] private Stamina stamina;
    [SerializeField] private GameObject mainGameObject;
    [SerializeField] private GameObject endOfGameObject;
    [SerializeField] private GameObject fireflyParticle;
    [SerializeField] private SwipeUI swipeUI;
    private int enablePerson;
    [SerializeField] private Button customerImage;
    public TextMeshProUGUI orderText;
    public GameObject orderPage;
    public Button orderPageButton;
    public GameObject SelectFoodPage;
    public List<Button> foodButtons;
    private List<Dictionary<string, object>> orderSentences;
    [SerializeField] private List<Ingredient> breadIngredient;
    private List<Ingredient> vegetableIngredient;
    private List<Ingredient> mainIngredient;
    private List<Ingredient> cheeseIngredient;
    private List<Ingredient> sauceIngredient;
    private Sandwich sandwichOrder;
    private Sandwich sandwichResult;
    private int curPerson = 0;
    public async void Init()
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
    }
    public void OnClickedCustomer()
    {
        customerImage.gameObject.SetActive(false);
        curPerson++;
        MakeOrder();
        orderPage.SetActive(true);
        string temp = (string)orderSentences[0]["Sentences"];
        temp = temp.Replace("{Main}", sandwichOrder.Main.IngredientName);
        temp = temp.Replace("{Bread}",sandwichOrder.Bread.IngredientName);
        temp = temp.Replace("{Cheese}",sandwichOrder.Cheese.IngredientName);
        temp = temp.Replace("{Vegetable}",sandwichOrder.Vegetable.IngredientName);
        temp = temp.Replace("{Emotion}", sandwichOrder.Sauce.Emotion.ToString());
        orderText.text = temp;
        orderPageButton.onClick.AddListener(() => {
            SelectBread();
            orderPage.SetActive(false); });
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
        sauceRandNum = Random.Range(1, 9);
        sandwichOrder = new Sandwich(breadIngredient[breadRandNum], vegetableIngredient[vegeRandNum],
            mainIngredient[mainRandNum], cheeseIngredient[cheeseRandNum], sauceIngredient[sauceRandNum]);
    }
    private void SelectBread()
    {
        SelectFoodPage.SetActive(true);
        sandwichResult = new Sandwich();
        foreach(var button in foodButtons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
        for(int i=0;i<breadIngredient.Count;i++)
        {
            int x = i;
            foodButtons[x].gameObject.SetActive(true);
            foodButtons[x].onClick.AddListener(() =>
            {
                sandwichResult.SetBread(breadIngredient[x]);
                SelectMain();
            }
            );
        }
    }
    private void SelectMain()
    {
        foreach (var button in foodButtons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < mainIngredient.Count; i++)
        {
            int x = i;
            foodButtons[x].gameObject.SetActive(true);
            foodButtons[x].onClick.AddListener(() => {
                sandwichResult.SetMain(mainIngredient[x]);
                SelectCheese();
            });
        }
    }
    private void SelectCheese()
    {
        foreach (var button in foodButtons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < cheeseIngredient.Count; i++)
        {
            int x = i;
            foodButtons[x].gameObject.SetActive(true);
            foodButtons[x].onClick.AddListener(() => {
                sandwichResult.SetCheese(cheeseIngredient[x]);
                SelectVegetable();
            });
        }
    }
    private void SelectVegetable()
    {
        foreach (var button in foodButtons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < vegetableIngredient.Count; i++)
        {
            int x = i;
            foodButtons[x].gameObject.SetActive(true);
            foodButtons[x].onClick.AddListener(() => {
                sandwichResult.SetVegetable(vegetableIngredient[x]);
                SelectSauce();
            });
        }
    }
    private void SelectSauce()
    {
        foreach (var button in foodButtons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < sauceIngredient.Count; i++)
        {
            int x = i;
            foodButtons[x].gameObject.SetActive(true);
            foodButtons[x].onClick.AddListener(() => {
                sandwichResult.SetSauce(sauceIngredient[x]);
                SetCustomer();
                SelectFoodPage.SetActive(false);
            });
        }
    }
    /// <summary>
    /// 게임 매니저로 씬 변경없이 UI변경으로 게임 시작
    /// </summary>
    public void StartGame()
    {
        stamina.UseStamina();
        fireflyParticle.SetActive(false);
        mainGameObject.SetActive(true);
        Init();
        swipeUI.enabled = false;
        enablePerson = Mathf.FloorToInt(5f * Mathf.Pow(1.05f,
            PlayerDataContainer.Instance.playerData.playTimeLevel));
        SetCustomer();
    }
    public void EndGame()
    {
        curPerson = 0;
        mainGameObject.SetActive(false);
        swipeUI.enabled = true;
        fireflyParticle?.SetActive(true);
    }
    public void SetCustomer()
    {
        customerImage.onClick.RemoveAllListeners();
        if (curPerson == enablePerson)
        {
            EndGame();
            return;
        }
        customerImage.onClick.AddListener
            (() =>
            {
                OnClickedCustomer();
            });
        customerImage.gameObject.SetActive(true);
    }
    private void OnApplicationQuit()
    {
        PlayerDataContainer.Instance.SavePlayerData();
    }
}
