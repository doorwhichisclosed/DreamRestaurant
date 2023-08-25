using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientArchivePage : MonoBehaviour
{
    public List<Archive> archives;
    public void InitArchives()
    {
        List<Ingredient> ingredients = new List<Ingredient>();
        ingredients = PlayerDataContainer.Instance.ingredients;
        ingredients.Sort((ingredientA, ingrdientB)=>ingredientA.Condition.CompareTo(ingrdientB.Condition));
        for(int i = 0; i < ingredients.Count; i++)
        {
            archives[i].InitArchive(ingredients[i].IngredientName, PlayerDataContainer.Instance.IngredientsIcon[ingredients[i].IngredientName], ingredients[i].Condition, ingredients[i].Cost);
           
        }
    }
    public void SetArchives()
    {
        for(int i=0;i<archives.Count;i++)
        {
            archives[i].SetArchive();
        }
    }
}
