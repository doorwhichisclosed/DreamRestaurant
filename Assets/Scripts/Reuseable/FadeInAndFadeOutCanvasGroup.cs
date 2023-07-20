using DG.Tweening;
using UnityEngine;

public class FadeInAndFadeOutCanvasGroup : MonoBehaviour
{
    public float fadeOutTime;
    private CanvasGroup target;
    private Sequence fadeOutFadeInSequence;
    /// <summary>
    /// 글씨UI 페이드인페이드 아웃
    /// </summary>
    private void Start()
    {
        target = GetComponent<CanvasGroup>();
        fadeOutFadeInSequence = DOTween.Sequence();
        fadeOutFadeInSequence.Append(target.DOFade(0, fadeOutTime));
        fadeOutFadeInSequence.Append(target.DOFade(1, fadeOutTime));
        fadeOutFadeInSequence.SetLoops(-1);
        fadeOutFadeInSequence.Play();
    }
}
