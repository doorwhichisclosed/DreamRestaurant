using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Stamina stamina;
    [SerializeField] private GameObject mainGameObject;
    [SerializeField] private GameObject endOfGameObject;
    [SerializeField] private GameObject fireflyParticle;
    [SerializeField] private SwipeUI swipeUI;
    private int enablePerson;
    [SerializeField] private Button customerImage;
    [SerializeField] private SandwichChecker sandwichChecker;
    [SerializeField] private GameStoreManager gameStoreManager;
    private int curPerson = 0;
    /// <summary>
    /// 게임 매니저로 씬 변경없이 UI변경으로 게임 시작
    /// </summary>
    public void StartGame()
    {
        stamina.UseStamina();
        fireflyParticle.SetActive(false);
        mainGameObject.SetActive(true);
        mainGameObject.GetComponent<IngredientManager>().PrepareIngredients();
        swipeUI.enabled = false;
        enablePerson = Mathf.FloorToInt(5f * Mathf.Pow(1.05f,
            PlayerDataContainer.Instance.playerData.playTimeLevel));
        SetCustomer();
    }

    public void EndGame()
    {
        curPerson = 0;
        mainGameObject.SetActive(false);
        swipeUI.enabled = true;
        fireflyParticle?.SetActive(true);
    }
    public void SetCustomer()
    {
        customerImage.onClick.RemoveAllListeners();
        if (curPerson == enablePerson)
        {
            EndGame();
            return;
        }
        customerImage.onClick.AddListener
            (() =>
            {
                curPerson++;
                gameStoreManager.SetOrder();
                customerImage.gameObject.SetActive(false);

            });
        customerImage.gameObject.SetActive(true);
    }
    private void OnApplicationQuit()
    {
        PlayerDataContainer.Instance.SavePlayerData();
    }
}
