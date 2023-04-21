using DG.Tweening;
using TMPro;
using UnityEngine;

public class FadeInAndFadeOutTextmeshUI : MonoBehaviour
{
    public float fadeOutTime;
    private TextMeshProUGUI target;
    private Sequence fadeOutFadeInSequence;
    /// <summary>
    /// 글씨UI 페이드인페이드 아웃
    /// </summary>
    private void Start()
    {
        target = GetComponent<TextMeshProUGUI>();
        fadeOutFadeInSequence = DOTween.Sequence();
        fadeOutFadeInSequence.Append(target.DOFade(0, fadeOutTime));
        fadeOutFadeInSequence.Append(target.DOFade(1, fadeOutTime));
        fadeOutFadeInSequence.SetLoops(-1);
        fadeOutFadeInSequence.Play();
    }
}
