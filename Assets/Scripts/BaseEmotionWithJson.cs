using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class BaseEmotionWithJson : MonoBehaviour
{
    public List<Emotion> emotions;
    /// <summary>
    /// json파일 작성
    /// </summary>
    public void EmotionsToJson()
    {
        string jsonFile = JsonUtility.ToJson(new UtilClasses.SerializationList<Emotion>(emotions));
        File.WriteAllText(Application.persistentDataPath + "/" + "BaseEmotions", jsonFile);
    }

    public void EmotionsFromJson()
    {
        if (File.Exists(Application.persistentDataPath + "/" + "BaseEmotions"))
        {
            string jsonFile = File.ReadAllText(Application.persistentDataPath + "/" + "BaseEmotions");
            emotions = JsonUtility.FromJson<UtilClasses.SerializationList<Emotion>>(jsonFile).ToList();
        }
    }
}
