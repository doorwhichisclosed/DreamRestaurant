using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Emotion
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
public enum IngredientCategory
{
    None,
    Bread,
    Vegetable,
    Main,
    Cheese,
    Sauce,
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
[System.Serializable]
public class Ingredient
{
    private string ingredientName;
    public string IngredientName { get { return ingredientName; } }
    private string ingredientDescription;
    public string IngredientDescription { get { return ingredientDescription; } }
    private Emotion emotion;
    public Emotion Emotion { get { return emotion; } }
    private IngredientCategory ingredientCategory;
    public IngredientCategory IngredientCategory { get { return ingredientCategory; } } 
    private int cost;
    public int Cost { get { return cost; } }

    public Ingredient(string _ingredientName, string _ingredintDescription, string _parentEmotion,
        string _ingredientCategory, int _cost)
    {
        ingredientName = _ingredientName;
        ingredientDescription = _ingredintDescription;
        emotion = (Emotion)Enum.Parse(typeof(Emotion), _parentEmotion);
        ingredientCategory=(IngredientCategory)Enum.Parse(typeof(IngredientCategory), _ingredientCategory);
        cost = _cost;
    }
}
