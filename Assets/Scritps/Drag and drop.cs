using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class DragAndDrop : MonoBehaviour,  IDragHandler , IDropHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 posinicial;
    private bool isOverDropZone  = false;
    private string type;
    public bool playmade = false;
    private GameObject Player1;
    private GameObject Player2;
    private GameObject board;
    private Animator animator;
    public Deck deck;
    void Awake()
    {
        Player1 = GameObject.Find("Player 1");
        Player2 = GameObject.Find("Player 2");
        board = GameObject.Find("Board");
        animator = board.GetComponent<Animator>();
        deck = GetComponentInParent<Deck>();
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        posinicial =  rectTransform.anchoredPosition;
        canvas = GetComponentInParent<Canvas>();
        type = gameObject.tag;   
    }
    void Update()
    {
    Invoke("PlayMade" , 0.5f);
    }

    //Gestiona cuando se está arrastrando el GameObject
    public void OnDrag(PointerEventData eventData)
    { if(Deck.DrawExecuted == true)
        {
        if (!isOverDropZone)
        {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
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
        playmade = true;
        for(int i = 0 ; i < deck.hand.Count ; i++)
        {
            //Borrar la carta de la mano
            if(gameObject == deck.hand[i])
            {
                deck.hand.RemoveAt(i);
            }
        }
        }
    }
    void PlayMade()
    {
        if(playmade && TurnController.haspassed == false)
        {
        GameFunctions.EndTurn(Player1,Player2,animator);
        playmade = false;
        }
    }   
}

