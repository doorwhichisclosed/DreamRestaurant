using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Emotion
{   
    None = 0,
    Anger = 1,
    Sadness = 2,
    Anxiety = 3,
    Hurt = 4,
    Shame = 5,
    Pleasure = 6,
    Love = 7,
    Wish = 8
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
    private int condition;
    public int Condition { get { return condition; } }
    private string ingredientAddress;
    public string IngredientAddress { get { return ingredientAddress; } }

    public Ingredient(string _ingredientName, string _ingredintDescription,
        string _ingredientCategory, int _cost,int _condition,string _ingredientAddress)
    {
        ingredientName = _ingredientName;
        ingredientDescription = _ingredintDescription;
        emotion = Emotion.None;
        ingredientCategory=(IngredientCategory)Enum.Parse(typeof(IngredientCategory), _ingredientCategory);
        cost = _cost;
        condition=_condition;
        ingredientAddress = _ingredientAddress;
    }
    public void SelectEmotion(int emotionNum)
    {
        this.emotion = (Emotion)emotionNum;
    }
}

[System.Serializable]
public class Sandwich
{
    private Ingredient bread;
    public Ingredient Bread { get { return bread; } }
    private Ingredient vegetable;
    public Ingredient Vegetable { get {  return vegetable; } }
    private Ingredient main;    
    public Ingredient Main { get {  return main; } }
    private Ingredient cheese;
    public Ingredient Cheese { get {  return cheese; } }
    private Ingredient sauce;
    public Ingredient Sauce { get { return sauce; } }
    public Sandwich(Ingredient bread, Ingredient vegetable, Ingredient main, Ingredient cheese, Ingredient sauce)
    {
        this.bread = bread;
        this.vegetable = vegetable;
        this.main = main;
        this.cheese = cheese;
        this.sauce = sauce;
    }
    public Sandwich() { }
    public void SetBread(Ingredient bread)
    {
        this.bread = bread;
    }
    public void SetVegetable(Ingredient vegetable)
    {
        this.vegetable=vegetable;
    }
    public void SetMain(Ingredient main)
    {
        this.main=main;
    }
    public void SetCheese(Ingredient cheese)
    {
        this.cheese=cheese;
    }
    public void SetSauce(Ingredient sauce)
    {
        this.sauce=sauce;
    }
}
