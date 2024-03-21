using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour,  IDragHandler , IDropHandler
{
    private RectTransform rectTransform;
    public RectTransform row;
    private Canvas canvas;
    private Vector2 posinicial;


    private void Start()
    {
       
        rectTransform = GetComponent<RectTransform>();
        
         posinicial =  rectTransform.anchoredPosition;

        canvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    { 
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    
    }
    public void OnDrop(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = posinicial;
    }
}

