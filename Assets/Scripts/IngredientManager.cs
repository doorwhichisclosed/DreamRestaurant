using System.Collections.Generic;
using System.IO;
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
    [SerializeField] private ToggleGroup ingredientSelectToggleGroup;
    [SerializeField] private List<Toggle> tempToggles;
    [SerializeField] private Button nextButton;
    [SerializeField] private SandwichChecker sandwichMaker;

    private Ingredient curBread;
    private List<Ingredient> curVegetables;
    private Ingredient curMain;
    private List<Ingredient> curCheeses;
    private List<Ingredient> curSauces;

    /// <summary>
    /// 상점을 연다. 이때, json파일로부터 모든 음식 정보를 읽어온다.
    /// 상태 이상을 추후에 추가한다.
    /// </summary>
    public void OpenStore()
    {
        if (File.Exists(Application.persistentDataPath + "/" + "BaseIngredients"))
        {
            baseIngredients = new List<BaseIngredient>();
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
        nextButton.onClick.RemoveAllListeners();
        foreach (Toggle toggle in tempToggles)
        {
            toggle.gameObject.SetActive(false);
        }
        ingredientSelectToggleGroup.enabled = true;
        for (int i = 0; i < breadIngredients.Count; i++)
        {
            Toggle temp = tempToggles[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = false;
            temp.GetComponentInChildren<Text>().text = breadIngredients[i].IngredientName;
            temp.gameObject.SetActive(true);
            int x = i;
            temp.onValueChanged.AddListener((bool isOn) =>
            {
                if (isOn)
                {
                    curBread = breadIngredients[x];
                }
            });
        }
        nextButton.onClick.AddListener(() => { if (ingredientSelectToggleGroup.AnyTogglesOn()) SelectVegetable(); });
    }

    private void SelectVegetable()
    {
        Debug.Log("vegetable");
        List<Ingredient> tempCurVegetables = new List<Ingredient>();
        curVegetables = new List<Ingredient>();
        nextButton.onClick.RemoveAllListeners();
        foreach (Toggle toggle in tempToggles)
        {
            toggle.gameObject.SetActive(false);
        }
        ingredientSelectToggleGroup.enabled = false;
        for (int i = 0; i < vegetableIngredients.Count; i++)
        {
            Toggle temp = tempToggles[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = false;
            temp.GetComponentInChildren<Text>().text = vegetableIngredients[i].IngredientName;
            temp.gameObject.SetActive(true);
            int x = i;
            temp.onValueChanged.AddListener((bool isOn) =>
            {
                if (isOn)
                {
                    tempCurVegetables.Add(vegetableIngredients[x]);
                }
                else
                {
                    if (tempCurVegetables.Contains(vegetableIngredients[x]))
                    {
                        tempCurVegetables.Remove(vegetableIngredients[x]);
                    }
                }
            });
        }
        nextButton.onClick.AddListener(() =>
        {
            if (tempCurVegetables.Count > 0)
            {
                foreach (Ingredient i in tempCurVegetables)
                {
                    curVegetables.Add(i);
                }
                SelectMain();
            }
        });
    }

    private void SelectMain()
    {
        Debug.Log("main");
        nextButton.onClick.RemoveAllListeners();
        foreach (Toggle toggle in tempToggles)
        {
            toggle.gameObject.SetActive(false);
        }
        ingredientSelectToggleGroup.enabled = true;
        for (int i = 0; i < mainIngredients.Count; i++)
        {
            Toggle temp = tempToggles[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = false;
            temp.GetComponentInChildren<Text>().text = mainIngredients[i].IngredientName;
            temp.gameObject.SetActive(true);
            int x = i;
            temp.onValueChanged.AddListener((bool isOn) =>
            {
                if (isOn)
                {
                    curMain = mainIngredients[x];
                }
            });
        }
        nextButton.onClick.AddListener(() => { if (ingredientSelectToggleGroup.AnyTogglesOn()) SelectCheese(); });
    }

    private void SelectCheese()
    {
        Debug.Log("cheese");
        List<Ingredient> tempCurCheeses = new List<Ingredient>();
        curCheeses = new List<Ingredient>();
        nextButton.onClick.RemoveAllListeners();
        foreach (Toggle toggle in tempToggles)
        {
            toggle.gameObject.SetActive(false);
        }
        ingredientSelectToggleGroup.enabled = false;
        for (int i = 0; i < cheeseIngredients.Count; i++)
        {
            Toggle temp = tempToggles[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = false;
            temp.GetComponentInChildren<Text>().text = cheeseIngredients[i].IngredientName;
            temp.gameObject.SetActive(true);
            int x = i;
            temp.onValueChanged.AddListener((bool isOn) =>
            {
                if (isOn)
                {
                    tempCurCheeses.Add(cheeseIngredients[x]);
                }
                else
                {
                    if (tempCurCheeses.Contains(cheeseIngredients[x]))
                        tempCurCheeses.Add(cheeseIngredients[x]);
                }
            });
        }
        nextButton.onClick.AddListener(() =>
        {
            if (tempCurCheeses.Count > 0)
            {
                foreach (Ingredient i in tempCurCheeses)
                {
                    curCheeses.Add(i);
                }
                SelectSauce();
            }
        });
    }

    private void SelectSauce()
    {
        Debug.Log("sauce");
        List<Ingredient> tempCurSauces = new List<Ingredient>();
        curSauces = new List<Ingredient>();
        nextButton.onClick.RemoveAllListeners();
        foreach (Toggle toggle in tempToggles)
        {
            toggle.gameObject.SetActive(false);
        }
        ingredientSelectToggleGroup.enabled = false;
        for (int i = 0; i < sauceIngredients.Count; i++)
        {
            Toggle temp = tempToggles[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = false;
            temp.GetComponentInChildren<Text>().text = sauceIngredients[i].IngredientName;
            temp.gameObject.SetActive(true);
            int x = i;
            temp.onValueChanged.AddListener((bool isOn) =>
            {
                if (isOn)
                {
                    tempCurSauces.Add(sauceIngredients[x]);
                }
                else
                {
                    if (tempCurSauces.Contains(sauceIngredients[x]))
                        tempCurSauces.Remove(sauceIngredients[x]);
                }
            });
        }
        nextButton.onClick.AddListener(() =>
        {
            if (tempCurSauces.Count > 0)
            {
                foreach (Ingredient i in tempCurSauces)
                {
                    curSauces.Add(i);
                }
                sandwichMaker.CheckSandwich(FinishSandwich());
            }
        });
    }
    /// <summary>
    /// 샌드위치를 만들고 return해준다. 이는 SandwichMaker+SandwichChecker(추후에 합칩)에서 평가한다.
    /// </summary>
    /// <returns></returns>
    public Sandwich FinishSandwich()
    {
        Sandwich sandwich = new Sandwich(curBread, curVegetables, curMain, curCheeses, curSauces);
        ingredientSelectCanvas.SetActive(false);
        return sandwich;
    }
}
