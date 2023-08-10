using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDropFood : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.position=eventData.position;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position=eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector2.zero;
        GetComponent<Image>().raycastTarget=true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
