using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public GameObject afterLoading;
    public TextMeshProUGUI loadingText;
    public IngredientArchivePage ingredientArchivePage;
    // Start is called before the first frame update
    async void Start()
    {
        Image img = GetComponent<Image>();
        await img.DOFade(1.0f, 0.5f);
        await loadingText.DOText("¼ö¹Ú²®Áúµé±úººÀ½", 1f);
        await PlayerDataContainer.Instance.LoadPlayerData();
        await img.DOFade(0f, 0.5f);
        gameObject.SetActive(false);
        afterLoading.SetActive(true);
        ingredientArchivePage.InitArchives();
    }
}
