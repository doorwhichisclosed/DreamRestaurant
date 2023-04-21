using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Answer
{
    public ParentEmotion parentEmotion;
    public List<string> childEmotion;
}
public class Test : MonoBehaviour
{
    public BaseEmotionWithJson baseEmotionWithJson;
    public BaseIngredientWithJson baseFoodWithJson;
    public TextMeshProUGUI quizText;
    public Answer curAnswer;
    public Button pButton;
    public Transform buttonParentTransform;
    void Start()
    {
        foreach (BaseIngredient food in baseFoodWithJson.ingredients)
        {
            Button button = Instantiate(pButton);
            button.onClick.AddListener(() => AnswerTheQuestion(food));
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = food.IngredientName;
            button.transform.SetParent(buttonParentTransform);
        }
        RandomQuiz();
    }

    public void RandomQuiz()
    {
        int parentNum = Random.Range(0, 8);
        int childNum = Random.Range(0, baseEmotionWithJson.emotions[parentNum].childEmotion.Count);
        curAnswer = new Answer();
        curAnswer.parentEmotion = baseEmotionWithJson.emotions[parentNum].parentEmotion;
        curAnswer.childEmotion = new List<string> { baseEmotionWithJson.emotions[parentNum].childEmotion[childNum] };
        quizText.text = baseEmotionWithJson.emotions[parentNum].childEmotion[childNum];
    }

    public void AnswerTheQuestion(BaseIngredient food)
    {
        int result = 0;
        if (food.ParentEmotion == curAnswer.parentEmotion)
        {
            result += 1;
        }
        Debug.Log(result);
        RandomQuiz();
    }


}
