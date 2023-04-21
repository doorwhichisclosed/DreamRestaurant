using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BaseIngredientWithJson : MonoBehaviour
{/// <summary>
/// json 작성
/// </summary>
    public List<BaseIngredient> ingredients;

    public void FoodsToJson()
    {
        string jsonFile = JsonUtility.ToJson(new UtilClasses.SerializationList<BaseIngredient>(ingredients));
        File.WriteAllText(Application.persistentDataPath + "/" + "BaseIngredients", jsonFile);
    }

    public void FoodsFromJson()
    {
        if (File.Exists(Application.persistentDataPath + "/" + "BaseIngredients"))
        {
            string jsonFile = File.ReadAllText(Application.persistentDataPath + "/" + "BaseIngredients");
            ingredients = JsonUtility.FromJson<UtilClasses.SerializationList<BaseIngredient>>(jsonFile).ToList();
        }
    }
}
