using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Plate : MonoBehaviour,IDropHandler
{
    public UnityEvent<Ingredient> OnDropIngredient;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.GetComponent<DragNDropFood>().Ingredient.IngredientName);
        eventData.pointerDrag.GetComponent<DragNDropFood>().OnEndDrag(eventData);
        OnDropIngredient.Invoke(eventData.pointerDrag.GetComponent<DragNDropFood>().Ingredient);
    }
}
