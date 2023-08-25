using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Archive : MonoBehaviour
{
    [SerializeField] Sprite lockImage;
    [SerializeField] Sprite unlockImage;
    [SerializeField] Sprite disableImage;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] private int level;
    [SerializeField] private Image ingredientImage;
    [SerializeField] private IngredientArchivePage ingredientArchivePage;
    public string ingredientName;
    public int ingredientCost;
    public int Level { get { return level; } }
    public void InitArchive(string ingredientName, Sprite ingredientIcon, int ingredientLevel, int _ingredientCost)
    {
        this.ingredientName = ingredientName;
        unlockImage = ingredientIcon;
        level = ingredientLevel;
        ingredientCost = _ingredientCost;
        Debug.Log(PlayerDataContainer.Instance.playerData);
        SetArchive();
    }
    public void SetArchive()
    {
        if (PlayerDataContainer.Instance.playerData.curStoryNum < level)
        {
            if (PlayerDataContainer.Instance.playerData.curStoryNum - level == -1)
            {
                costText.gameObject.SetActive(true);
                costText.text = (level * 500).ToString();
                ingredientImage.color = Color.clear;
                Button b = GetComponentInChildren<Button>();
                b.enabled = true;
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(() =>
                {
                    if (PlayerDataContainer.Instance.playerData.moneyNum - (level * 500) < 0)
                        return;
                    PlayerDataContainer.Instance.ChangeValue("Money", -(level * 500), true);
                    PlayerDataContainer.Instance.ChangeValue("CurStoryNum", 1, true);
                    ingredientArchivePage.SetArchives();
                });
            }
            else
            {
                costText.gameObject.SetActive(false);
                ingredientImage.sprite = disableImage;
                ingredientImage.color = Color.white;
                Button b = GetComponentInChildren<Button>();
                b.enabled = false;
            }
            ingredientImage.preserveAspect = true;
        }
        else
        {
            costText.gameObject.SetActive(false);
            ingredientImage.sprite = unlockImage;
            ingredientImage.color = Color.white;
            Button b = GetComponentInChildren<Button>();
            b.enabled = false;
            ingredientImage.preserveAspect = true;

        }
    }
}
    