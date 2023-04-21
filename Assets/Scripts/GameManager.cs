using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Stamina stamina;
    [SerializeField] private GameObject mainGameObject;
    [SerializeField] private GameObject endOfGameObject;
    public float gameTime;
    /// <summary>
    /// 게임 매니저로 씬 변경없이 UI변경으로 게임 시작
    /// </summary>
    public void StartGame()
    {
        stamina.UseStamina();
        mainGameObject.SetActive(true);
        mainGameObject.GetComponent<IngredientManager>().OpenStore();
        EndGameTimer().Forget();
    }

    private async UniTaskVoid EndGameTimer()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(gameTime));
        endOfGameObject.SetActive(true);
    }
}
