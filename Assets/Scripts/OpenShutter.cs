using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenShutter : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public UnityEvent OnOpenShutter;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 currentPos;
    private float imgHeight;
    private Image img;
    [SerializeField] private float threshold = 300f;
    private void Start()
    {
        img = GetComponent<Image>();
        imgHeight = (Screen.height * 0.4f);
    }
    public void OnDrag(PointerEventData eventData)
    {
        currentPos = eventData.position;
        img.fillAmount = (1 - (currentPos - startPos).y / imgHeight);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Start");
        startPos = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End");
        endPos = eventData.position;
        Debug.Log(endPos - startPos);
        img.fillAmount = 1;
        if (endPos.y - startPos.y >= threshold)
        {
            OnOpenShutter.Invoke();
        }
    }
}
