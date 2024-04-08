using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class DragAndDrop : MonoBehaviour,  IDragHandler , IDropHandler 
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 posinicial;
    public bool isOverDropZone  = false;
    public GameObject meleeZone;
    private string type;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
        posinicial =  rectTransform.anchoredPosition;

        canvas = GetComponentInParent<Canvas>();

        type = gameObject.tag;
    }

    //Gestiona cuando se está arrastrando el GameObject
    public void OnDrag(PointerEventData eventData)
    { 
        if (!isOverDropZone)
        {
           rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        else
        {
        return;
        }
    }
    //Gestiona cuando se suelta el GameObject
    public void OnDrop(PointerEventData eventData)
    {
        if (!isOverDropZone)
        {
           rectTransform.anchoredPosition = posinicial;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
   if (collider.gameObject.CompareTag(type))
   {
   isOverDropZone = true;
   }

    }

}

