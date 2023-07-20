using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class IngredientManager : MonoBehaviour
{
    private List<BaseIngredient> baseIngredients;
    public List<Ingredient> breadIngredients;
    public List<Ingredient> vegetableIngredients;
    public List<Ingredient> mainIngredients;
    public List<Ingredient> cheeseIngredients;
    public List<Ingredient> sauceIngredients;

    [SerializeField] private GameObject ingredientSelectCanvas;
    [SerializeField] private List<Button> tempButtons;
    [SerializeField] private SandwichChecker sandwichChecker;

    private Ingredient curBread;
    private List<Ingredient> curVegetables;
    private Ingredient curMain;
    private List<Ingredient> curCheeses;
    private Ingredient curSauce;
    private int vegetableNum = 0;
    private int cheeseNum = 0;

    /// <summary>
    /// 상점을 연다. 이때, json파일로부터 모든 음식 정보를 읽어온다.
    /// 상태 이상을 추후에 추가한다.
    /// </summary>
    public void PrepareIngredients()
    {
        baseIngredients = new List<BaseIngredient>();
        breadIngredients.Clear();
        vegetableIngredients.Clear();
        mainIngredients.Clear();
        cheeseIngredients.Clear();
        sauceIngredients.Clear();
        if (File.Exists(Application.persistentDataPath + "/" + "BaseIngredients"))
        {
            string jsonFile = File.ReadAllText(Application.persistentDataPath + "/" + "BaseIngredients");
            baseIngredients = JsonUtility.FromJson<UtilClasses.SerializationList<BaseIngredient>>(jsonFile).ToList();
        }
        foreach (BaseIngredient baseIngredient in baseIngredients)
        {
            //maybe effect here
            if (baseIngredient.IngredientCategory == IngredientCategory.Bread)
            {
                breadIngredients.Add(new Ingredient(baseIngredient.IngredientName, baseIngredient.ParentEmotion,
                    baseIngredient.DreamCost, baseIngredient.IngredientCategory));
            }
            else
            {
                if (baseIngredient.IngredientCategory == IngredientCategory.Vegetable)
                {
                    vegetableIngredients.Add(new Ingredient(baseIngredient.IngredientName, baseIngredient.ParentEmotion,
                    baseIngredient.DreamCost, baseIngredient.IngredientCategory));
                }
                else if (baseIngredient.IngredientCategory == IngredientCategory.Main)
                {
                    mainIngredients.Add(new Ingredient(baseIngredient.IngredientName, baseIngredient.ParentEmotion,
                    baseIngredient.DreamCost, baseIngredient.IngredientCategory));
                }
                else if (baseIngredient.IngredientCategory == IngredientCategory.Cheese)
                {
                    cheeseIngredients.Add(new Ingredient(baseIngredient.IngredientName, baseIngredient.ParentEmotion,
                    baseIngredient.DreamCost, baseIngredient.IngredientCategory));
                }
                else if (baseIngredient.IngredientCategory == IngredientCategory.Sauce)
                {
                    sauceIngredients.Add(new Ingredient(baseIngredient.IngredientName, baseIngredient.ParentEmotion,
                    baseIngredient.DreamCost, baseIngredient.IngredientCategory));
                }
            }
        }
    }
    /// <summary>
    /// 버튼을 통해 음식 제작 시작 자동으로 넘어간다.
    /// </summary>
    public void StartToSelectIngredient()
    {
        ingredientSelectCanvas.SetActive(true);
        SelectBread();
    }
    private void SelectBread()
    {
        Debug.Log("bread");
        foreach (Button button in tempButtons)
        {
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < breadIngredients.Count; i++)
        {
            Button temp = tempButtons[i];
            temp.onClick.RemoveAllListeners();
            temp.GetComponentInChildren<TextMeshProUGUI>().text
                = breadIngredients[i].IngredientName;
            temp.gameObject.SetActive(true);
            int x = i;
            temp.onClick.AddListener(() =>
            {
                curBread = breadIngredients[x];
                SelectVegetable();
            });
        }
    }

    private void SelectVegetable()
    {
        vegetableNum = 0;
        Debug.Log("vegetable");
        List<Ingredient> tempCurVegetables = new List<Ingredient>();
        curVegetables = new List<Ingredient>();
        foreach (Button button in tempButtons)
        {
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < vegetableIngredients.Count; i++)
        {
            Button temp = tempButtons[i];
            temp.onClick.RemoveAllListeners();
            temp.GetComponentInChildren<TextMeshProUGUI>().text
                = vegetableIngredients[i].IngredientName;
            temp.gameObject.SetActive(true);
            int x = i;
            temp.onClick.AddListener(() =>
            {

                tempCurVegetables.Add(vegetableIngredients[x]);
                vegetableNum++;
                if (vegetableNum == 3)
                {
                    SelectMain();
                }
                else
                    temp.gameObject.SetActive(false);
            });
        }
    }

    private void SelectMain()
    {
        Debug.Log("main");
        foreach (Button button in tempButtons)
        {
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < mainIngredients.Count; i++)
        {
            Button temp = tempButtons[i];
            temp.onClick.RemoveAllListeners();
            temp.GetComponentInChildren<TextMeshProUGUI>().text
                = mainIngredients[i].IngredientName;
            temp.gameObject.SetActive(true);
            int x = i;
            temp.onClick.AddListener(() =>
            {

                curMain = mainIngredients[x];
                SelectCheese();
            });
        }
    }

    private void SelectCheese()
    {
        cheeseNum = 0;
        Debug.Log("cheese");
        List<Ingredient> tempCurCheeses = new List<Ingredient>();
        curCheeses = new List<Ingredient>();
        foreach (Button button in tempButtons)
        {
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < cheeseIngredients.Count; i++)
        {
            Button temp = tempButtons[i];
            temp.onClick.RemoveAllListeners();
            temp.GetComponentInChildren<TextMeshProUGUI>().text
                = cheeseIngredients[i].IngredientName;
            temp.gameObject.SetActive(true);
            int x = i;
            temp.onClick.AddListener(() =>
            {
                tempCurCheeses.Add(cheeseIngredients[x]);
                cheeseNum++;
                if (cheeseNum == 2)
                {
                    SelectSauce();
                }
                else
                    temp.gameObject.SetActive(false);
            });
        }
    }

    private void SelectSauce()
    {
        Debug.Log("sauce");
        List<Ingredient> tempCurSauces = new List<Ingredient>();
        foreach (Button button in tempButtons)
        {
            button.gameObject.SetActive(false);
        }
        for (int i = 0; i < sauceIngredients.Count; i++)
        {
            Button temp = tempButtons[i];
            temp.onClick.RemoveAllListeners();
            temp.GetComponentInChildren<TextMeshProUGUI>().text
                = sauceIngredients[i].IngredientName;
            temp.gameObject.SetActive(true);
            int x = i;
            temp.onClick.AddListener(() =>
            {

                curSauce = sauceIngredients[x];
                sandwichChecker.CheckSandwich(FinishSandwich());
            });
        }
    }
    /// <summary>
    /// 샌드위치를 만들고 return해준다. 이는 SandwichMaker+SandwichChecker(추후에 합칩)에서 평가한다.
    /// </summary>
    /// <returns></returns>
    public Sandwich FinishSandwich()
    {
        Sandwich sandwich = new Sandwich(curBread, curVegetables, curMain, curCheeses, curSauce);
        ingredientSelectCanvas.SetActive(false);
        return sandwich;
    }
}
