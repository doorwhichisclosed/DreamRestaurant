using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
public class Stamina : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI timerText;
    public int maxStamina = 25;
    public int currentStamina;
    private int restoreDuration = 100;
    private DateTime nextStaminaTime;
    private DateTime lastStaminaTime;
    private bool isRestoring = false;
    private DateTime cachedTime;

    async void Start()
    {
        await RenewCachedTime();
        TimerInternal().Forget();
        if (!ES3.KeyExists("CurrentStamina"))
        {
            ES3.Save("CurrentStamina", 25);
            Load().Forget();
            StartCoroutine(RestoreStamina());
        }
        else
        {
            Load().Forget();
            StartCoroutine(RestoreStamina());
        }
    }
    private async UniTask TimerInternal()
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1), delayTiming: PlayerLoopTiming.PreLateUpdate);
            cachedTime = cachedTime.AddSeconds(1);
            Debug.Log(cachedTime);
        }
    }
    /// <summary>
    /// 스태미나 사용 관련
    /// </summary>
    public void UseStamina()
    {
        if (currentStamina >= 1)
        {
            currentStamina--;
            UpdateStamina();
            if (!isRestoring)
            {
                if (currentStamina + 1 == maxStamina)
                {
                    nextStaminaTime = AddDuration(cachedTime, restoreDuration);
                }
                StartCoroutine(RestoreStamina());
            }
        }

        else
        {
            Debug.Log("Insufficient Energy!!");
        }
    }
    /// <summary>
    /// 스태미나 회복 관련
    /// 추후에 고쳐야함
    /// </summary>
    /// <returns></returns>
    private IEnumerator RestoreStamina()
    {
        UpdateStaminaTimer();
        isRestoring = true;
        while (currentStamina < maxStamina)
        {
            DateTime currentDateTime = cachedTime;
            DateTime nextDateTime = nextStaminaTime;
            bool isStaminaAdding = false;
            while (currentDateTime > nextDateTime)
            {
                if (currentStamina < maxStamina)
                {
                    isStaminaAdding = true;
                    currentStamina++;
                    UpdateStamina();
                    DateTime timeToAdd = lastStaminaTime > nextDateTime ? lastStaminaTime : nextDateTime;
                    nextDateTime = AddDuration(timeToAdd, restoreDuration);
                }
                else
                    break;
            }
            if (isStaminaAdding)
            {
                lastStaminaTime = cachedTime;
                nextStaminaTime = nextDateTime;
            }
            UpdateStaminaTimer();
            UpdateStamina();
            Save();
            yield return null;
        }
        isRestoring = false;
    }
    /// <summary>
    /// 시간 추가
    /// </summary>
    /// <param name="datetime"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private DateTime AddDuration(DateTime datetime, int duration)
    {
        return datetime.AddSeconds(duration);
    }
    private void UpdateStaminaTimer()
    {
        if (currentStamina >= maxStamina)
        {
            staminaText.text = "Full";
            timerText.text = null;
            return;
        }
        TimeSpan time = nextStaminaTime - cachedTime;
        string timeValue = string.Format("{0:D3}:{1:D1}", time.Minutes, time.Seconds);
        timerText.text = timeValue;

    }
    private void UpdateStamina()
    {
        staminaText.text = currentStamina.ToString() + "/" + maxStamina.ToString();
    }
    async UniTask<DateTime> StringToDate(string datetime)
    {
        if (String.IsNullOrEmpty(datetime))
        {
            return await WebTime();
        }
        else
        {
            return DateTime.Parse(datetime);
        }
    }
    /// <summary>
    /// 저장 관련(추후에 보안 추가)
    /// </summary>
    private void Save()
    {
        ES3.Save("CurrentStamina", currentStamina);
        ES3.Save("NextStaminaTime", nextStaminaTime.ToString());
        ES3.Save("LastStaminaTime", lastStaminaTime.ToString());
    }

    private async UniTask Load()
    {
        currentStamina = (int)ES3.Load("CurrentStamina");
        nextStaminaTime = await StringToDate(ES3.Load("NextStaminaTime").ToString());
        lastStaminaTime = await StringToDate(ES3.Load("LastStaminaTime").ToString());
    }
    /// <summary>
    /// 무리하게 웹에 접속하는 경우 제한
    /// </summary>
    /// <returns></returns>
    private async UniTask<DateTime> WebTime()
    {
        Debug.Log("server on");
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get("www.naver.com"))
        {
            await request.SendWebRequest();
            if (!String.IsNullOrEmpty(request.error))
            {
                Debug.Log(request.error);
                return DateTime.UtcNow;
            }
            else
            {
                string date = request.GetResponseHeader("date");
                DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
                return dateTime;
            }
        }
    }
    /// <summary>
    /// 필요할 때만 갱신
    /// </summary>
    /// <returns></returns>
    public async UniTask RenewCachedTime()
    {
        cachedTime = await WebTime();
    }
}
