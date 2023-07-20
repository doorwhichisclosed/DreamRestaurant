using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseIngredientWithJson))]
public class BaseFoodWithJsonEditor : Editor
{
    private BaseIngredientWithJson baseIngredientWithJson;
    private void OnEnable()
    {
        baseIngredientWithJson = target as BaseIngredientWithJson;
        baseIngredientWithJson.FoodsFromJson();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Save Ingredients"))
        {
            baseIngredientWithJson.FoodsToJson();
        }
        if (GUILayout.Button("Load Ingredients"))
        {
            baseIngredientWithJson.FoodsFromJson();
        }
        if (GUI.changed) { EditorUtility.SetDirty(baseIngredientWithJson); }
    }
}
