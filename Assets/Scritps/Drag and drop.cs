using System;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class DragAndDrop : MonoBehaviour, IDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private UnityEngine.Vector3 posinicial;
    private bool received = false;
    private bool received2 = false;
    private bool isOverDropZone = false;
    private string type;
    public bool playmade = false;
    private GameObject Player1Visual;
    private GameObject Player2Visual;
    private GameObject Player1;
    private GameObject Player2;
    private Deck deck;
    private Hand hand;
    private Turn playerturn;
    private Turn opponentturn;
    void Awake()
    {
        Player1Visual = GameObject.Find("Player 1 Visual");
        Player2Visual = GameObject.Find("Player 2 Visual");
        Player1 = GameObject.Find("Player 1");
        Player2 = GameObject.Find("Player 2");
        deck = GetComponentInParent<Deck>();
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        posinicial = rectTransform.anchoredPosition;
        canvas = GetComponentInParent<Canvas>();
        type = gameObject.tag;
        if (deck.gameObject.tag == "Player1")
        {
            hand = Player1Visual.GetComponentInChildren<Hand>();
            playerturn = Player1.GetComponentInChildren<Turn>();
            opponentturn = Player2.GetComponentInChildren<Turn>();
        }
        else if (deck.gameObject.tag == "Player2")
        {
            hand = Player2Visual.GetComponentInChildren<Hand>();
            playerturn = Player2.GetComponentInChildren<Turn>();
            opponentturn = Player1.GetComponentInChildren<Turn>();

        }
    }
    void Update()
    {
        //Obtener la posicion de la carta despues de ser robada
        if (!received && playerturn.DrawExecuted == true)
        {
            posinicial = GetComponent<RectTransform>().anchoredPosition;
            received = true;
        }
          if (!received2 && playerturn.StartRoundDraw == true)
        {
            posinicial = GetComponent<RectTransform>().anchoredPosition;
            received2 = true;
        }
    }

    //Gestiona cuando se está arrastrando el GameObject
    public void OnDrag(PointerEventData eventData)
    {
        if (playerturn.DrawExecuted == true)
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
        if (isOverDropZone == false)
        {
            rectTransform.anchoredPosition = posinicial;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(type))
        {
            isOverDropZone = true;
            for (int i = 0; i < hand.hand.Count; i++)
            {
                //Borrar la carta de la mano
                if (gameObject == hand.hand[i])
                {
                    hand.hand.RemoveAt(i);
                }
            }
            if (opponentturn.passed == false)
            {
                playerturn.playmade = true;
                playerturn.ismyturn = false;
                GameFunctions.CheckTurn();
            }
        }
    }
}

