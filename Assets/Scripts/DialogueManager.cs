using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private List<Dictionary<string, object>> scriptAsset;
    private int currentIdx;
    [SerializeField] private GameObject dialogueGameObject;
    [SerializeField] private Image playerImage;
    [SerializeField] private Image leftImage;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI leftNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image nextButton;
    private IEnumerator typingCoroutine;
    private bool isTyping = false;
    public void LoadScript(string scriptName)
    {
        scriptAsset = new List<Dictionary<string, object>>();
        scriptAsset = UtilClasses.CSVReader.Read(scriptName);
        StartScript();
    }
    private void StartScript()
    {
        dialogueGameObject.SetActive(true);
        currentIdx = 0;
        dialogueText.text = string.Empty;
        string curScript = (string)scriptAsset[currentIdx]["Script"];
        playerNameText.text = "me";
        leftNameText.text = (string)scriptAsset[currentIdx]["Left"];
        HighlightImageAndNameText((string)scriptAsset[currentIdx]["Highlight"]);
        typingCoroutine = TypeScript(curScript);
        isTyping = true;
        nextButton.gameObject.SetActive(false);
        StartCoroutine(typingCoroutine);
    }
    private void NextScript()
    {
        currentIdx++;
        dialogueText.text = string.Empty;
        if (currentIdx >= scriptAsset.Count)
        {
            dialogueGameObject.SetActive(false);
            return;
        }
        string curScript = (string)scriptAsset[currentIdx]["Script"];
        leftNameText.text = (string)scriptAsset[currentIdx]["Left"];
        HighlightImageAndNameText((string)scriptAsset[currentIdx]["Highlight"]);
        typingCoroutine = TypeScript(curScript);
        isTyping = true;
        nextButton.gameObject.SetActive(false);
        StartCoroutine(typingCoroutine);
    }

    public void GetTouch()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
            dialogueText.text = (string)scriptAsset[currentIdx]["Script"];
            isTyping = false;
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            NextScript();
        }
    }

    private IEnumerator TypeScript(string s)
    {
        isTyping = true;
        foreach (char c in s)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        nextButton.gameObject.SetActive(true);
        isTyping = false;
    }
    private void HighlightImageAndNameText(string side)
    {
        if (side.Equals("L"))
        {
            playerImage.DOFade(1, 0);
            leftImage.DOFade(0.5f, 0);
        }
        else if (side.Equals("R"))
        {
            playerImage.DOFade(0.5f, 0);
            leftImage.DOFade(1, 0);
        }
        else
        {
            playerImage.DOFade(1, 0);
            leftImage.DOFade(1, 0);
        }
    }
}
