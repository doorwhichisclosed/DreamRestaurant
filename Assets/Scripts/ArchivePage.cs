using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArchivePage : MonoBehaviour
{
    public List<Archive> archives;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        foreach (var archive in archives)
        {
            archive.GetComponent<Button>().onClick.RemoveAllListeners();
            if (archive.Level <= PlayerDataContainer.Instance.playerData.curStoryNum)
            {
                archive.lockImage.SetActive(false);
            }
            else
            {
                archive.lockImage.SetActive(true);
                if (archive.Level == PlayerDataContainer.Instance.playerData.curStoryNum + 1)
                {
                    archive.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        PlayerDataContainer.Instance.playerData.curStoryNum++;
                        Init();
                    });
                }
            }
        }
    }
}
