using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDropFood : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    private Ingredient ingredient;
    public RectTransform parentObject;
    public Ingredient Ingredient { get { return ingredient; } }
    public void SetIngredient(Ingredient _ingredient)
    {
        ingredient = _ingredient;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentObject, eventData.position, Camera.main, out mPosition);
        transform.localPosition = mPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector2.zero;
        GetComponent<Image>().raycastTarget=true;
    }
}
