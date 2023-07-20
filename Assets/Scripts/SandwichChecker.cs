using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SandwichChecker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI breadText;
    [SerializeField] private TextMeshProUGUI vegetableText;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI cheeseText;
    [SerializeField] private TextMeshProUGUI sauceText;
    [SerializeField] private GameStoreManager storeManager;
    [SerializeField] UnityEvent OnSandwichFinished;
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
        sauceText.text = sandwichResult.sauces.IngredientName;
        sb.Clear();
        float result = 0;
        if (sandwichResult.bread.IngredientName != storeManager.Order.preferBread)
        {
            result -= 1;
        }
        foreach (Ingredient i in sandwichResult.vegetables)
        {
            if (storeManager.Order.preferVegetables.Contains(i.IngredientName))
            {
                result += 0.5f;
            }
        }
        if (sandwichResult.main.IngredientName != storeManager.Order.preferMain)
        {
            result -= 1;
        }
        int cheeseCheck = 2;
        foreach (Ingredient i in sandwichResult.cheeses)
        {
            if (storeManager.Order.preferCheeses.Contains(i.IngredientName))
                cheeseCheck--;
            else
                cheeseCheck++;
        }
        result -= 0.5f * cheeseCheck;
        float sauceCheck = 0f;
        if (sandwichResult.sauces.ParentEmotion == storeManager.Order.preferParentEmotion)
        {
            sauceCheck += 1;
        }
        else
        {
            sauceCheck -= 1;
        }

        result += sauceCheck * 2;
        Debug.Log(result);
        OnSandwichFinished?.Invoke();
    }
}
