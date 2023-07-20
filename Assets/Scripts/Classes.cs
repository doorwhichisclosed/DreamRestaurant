using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ParentEmotion
{
    Anger = 0,
    Sadness = 1,
    Anxiety = 2,
    Hurt = 3,
    Shame = 4,
    Pleasure = 5,
    Love = 6,
    Wish = 7
}
[System.Serializable]
public class Emotion
{
    public ParentEmotion parentEmotion;
    public List<string> childEmotion;
}
[System.Serializable]
public enum IngredientCategory
{
    Bread,
    Vegetable,
    Main,
    Cheese,
    Sauce,
    Kick
}
[System.Serializable]
public class BaseIngredient
{
    [SerializeField] private string ingredientName;
    [SerializeField] private ParentEmotion parentEmotion;
    [SerializeField] private int dreamCost;
    [SerializeField] private IngredientCategory ingredinetCategory;

    public string IngredientName { get { return ingredientName; } }
    public ParentEmotion ParentEmotion { get { return parentEmotion; } }
    public int DreamCost { get { return dreamCost; } }
    public IngredientCategory IngredientCategory { get { return ingredinetCategory; } }

    public BaseIngredient(string _ingredientName, ParentEmotion _parentEmotion, int _dreamCost, IngredientCategory _ingredientCategory)
    {
        ingredientName = _ingredientName;
        parentEmotion = _parentEmotion;
        dreamCost = _dreamCost;
        ingredinetCategory = _ingredientCategory;
    }

}
[System.Serializable]
public class Ingredient
{
    private string ingredientName;
    private ParentEmotion parentEmotion;
    private int dreamCost;
    private IngredientCategory ingredinetCategory;

    public string IngredientName { get { return ingredientName; } }
    public ParentEmotion ParentEmotion { get { return parentEmotion; } }
    public int DreamCost { get { return dreamCost; } }
    public IngredientCategory IngredinetCategory { get { return ingredinetCategory; } }

    public Ingredient(string _ingredientName, ParentEmotion _parentEmotion, int _dreamCost, IngredientCategory _ingredientCategory)
    {
        ingredientName = _ingredientName;
        parentEmotion = _parentEmotion;
        dreamCost = _dreamCost;
        ingredinetCategory = _ingredientCategory;
    }
    public void ChnageParentEmotionTemporarily(IngredientCategory _ingredientCategory)
    {
        ingredinetCategory = _ingredientCategory;
    }

    public void ChangeParentCostTemporarily(int _dreamCost)
    {
        dreamCost = _dreamCost;
    }
}
[System.Serializable]
public class Order
{
    public string preferMain;
    public string preferBread;
    public List<string> preferVegetables;
    public List<string> preferCheeses;
    public ParentEmotion preferParentEmotion;
    public string preferEmotion;

    public Order(string _preferMain, string _preferBread, List<string> _unlikeVegetables,
        List<string> _preferCheeses, ParentEmotion _preferParentEmotion, string _preferEmotion)
    {
        this.preferMain = _preferMain;
        this.preferBread = _preferBread;
        preferVegetables = _unlikeVegetables;
        preferCheeses = _preferCheeses;
        preferParentEmotion = _preferParentEmotion;
        preferEmotion = _preferEmotion;
    }
}
[System.Serializable]
public class Sandwich
{
    public Ingredient bread;
    public List<Ingredient> vegetables;
    public Ingredient main;
    public List<Ingredient> cheeses;
    public Ingredient sauces;

    public Sandwich(Ingredient _bread, List<Ingredient> _vegetables, Ingredient _main,
        List<Ingredient> _cheeses, Ingredient _sauces)
    {
        bread = _bread;
        vegetables = _vegetables;
        main = _main;
        cheeses = _cheeses;
        sauces = _sauces;
    }
}

[System.Serializable]
public class PlayerData
{
    public int playTimeLevel;
    public int foodEfficiencyLevel;
    public int foodCostLevel;
    public int moneyNum;
    public int realMoneyNum;
    public int curStoryNum;

    public PlayerData()
    {
        playTimeLevel = 0;
        foodEfficiencyLevel = 0;
        foodCostLevel = 0;
        moneyNum = 0;
        realMoneyNum = 0;
        curStoryNum = 0;
    }
}
