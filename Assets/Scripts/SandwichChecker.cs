using System.Text;
using TMPro;
using UnityEngine;

public class SandwichChecker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI breadText;
    [SerializeField] private TextMeshProUGUI vegetableText;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI cheeseText;
    [SerializeField] private TextMeshProUGUI sauceText;
    [SerializeField] private GameObject ClickGetOrderButton;
    public OrderRecipe orderRecipe;
    /// <summary>
    /// 주어진 오더와 만들어진 샌드위치를 비교해준다.
    /// </summary>
    /// <param name="sandwichResult"></param>
    public void CheckSandwich(Sandwich sandwichResult)
    {
        breadText.text = sandwichResult.bread.IngredientName;
        StringBuilder sb = new StringBuilder();
        foreach (Ingredient i in sandwichResult.vegetables)
        {
            sb.Append(i.IngredientName);
            Debug.Log(i);
            if (i == sandwichResult.vegetables[sandwichResult.vegetables.Count - 1])
                break;
            sb.Append(", ");
        }
        vegetableText.text = sb.ToString();
        sb.Clear();
        mainText.text = sandwichResult.main.IngredientName;
        foreach (Ingredient i in sandwichResult.cheeses)
        {
            sb.Append(i.IngredientName);
            if (i == sandwichResult.cheeses[sandwichResult.cheeses.Count - 1])
                break;
            sb.Append(", ");
        }
        cheeseText.text = sb.ToString();
        sb.Clear();
        foreach (Ingredient i in sandwichResult.sauces)
        {
            sb.Append(i.IngredientName);
            if (i == sandwichResult.sauces[sandwichResult.sauces.Count - 1])
                break;
            sb.Append(", ");
        }
        sauceText.text = sb.ToString();
        sb.Clear();
        float result = 0;
        if (sandwichResult.bread.IngredientName != orderRecipe.order.preferBread)
        {
            result -= 1;
        }
        foreach (Ingredient i in sandwichResult.vegetables)
        {
            if (orderRecipe.order.unlikeVegetables.Contains(i.IngredientName))
            {
                result -= 0.5f;
            }
        }
        if (sandwichResult.main.IngredientName != orderRecipe.order.preferMain)
        {
            result -= 1;
        }
        int cheeseCheck = 2;
        foreach (Ingredient i in sandwichResult.cheeses)
        {
            if (orderRecipe.order.preferCheeses.Contains(i.IngredientName))
                cheeseCheck--;
            else
                cheeseCheck++;
        }
        result -= 0.5f * cheeseCheck;
        float sauceCheck = 0f;
        foreach (Ingredient i in sandwichResult.sauces)
        {
            if (orderRecipe.order.preferParentEmotion.Contains(i.ParentEmotion))
            {
                sauceCheck += 1f;
            }
            else
            {
                sauceCheck -= 0.5f;
            }
        }
        if (sauceCheck == orderRecipe.order.preferParentEmotion.Count)
        {
            sauceCheck += 1f;
        }
        else
        {
            sauceCheck -= 1f;
        }
        result += 0.5f * sauceCheck;
        Debug.Log(result);
        ClickGetOrderButton.SetActive(true);
    }
}
