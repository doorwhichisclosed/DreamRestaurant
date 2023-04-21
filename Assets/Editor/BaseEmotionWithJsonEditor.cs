using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseEmotionWithJson))]
public class BaseEmotionWithJsonEditor : Editor
{
    private BaseEmotionWithJson baseEmotionWithJson;
    private void OnEnable()
    {
        baseEmotionWithJson = target as BaseEmotionWithJson;
        baseEmotionWithJson.EmotionsFromJson();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("SaveEmotions"))
        {
            baseEmotionWithJson.EmotionsToJson();
        }
        if (GUILayout.Button("LoadEmotions"))
        {
            baseEmotionWithJson.EmotionsFromJson();
        }
        if (GUI.changed) { EditorUtility.SetDirty(baseEmotionWithJson); }
    }
}
