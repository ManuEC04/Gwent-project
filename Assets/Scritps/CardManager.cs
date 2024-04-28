using System;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class CardManager : MonoBehaviour, IDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    public UnityEngine.Vector3 posinicial;
    public bool received = false;
    public bool received2 = false;
    public bool isOverDropZone = false;
    private string type;
    public bool playmade = false;
    private GameObject Player1Visual;
    private GameObject Player2Visual;
    private GameObject Player1;
    private GameObject Player2;
    private CardOutput card;
    private int power;
    private OtherCardOutput othercard;
    private GameObject Cardinfo;
    private GameObject OtherCardinfo;
    private GameObject Canvas;
    private GameObject Board;
    private Deck deck;
    private Hand hand;
    public Turn playerturn;
    private Turn opponentturn;
    Lure lurecard;

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
        OtherCardinfo = GameObject.Find("OtherCardInfo");
        Cardinfo = GameObject.Find("CardInfo");
        Board = GameObject.Find("Fondo");
        Canvas = GameObject.Find("Invisible");
        OtherCardinfo.transform.SetParent(Canvas.transform);
        Cardinfo.transform.SetParent(Canvas.transform);
        rectTransform = GetComponent<RectTransform>();
        posinicial = rectTransform.anchoredPosition;
        canvas = GetComponentInParent<Canvas>();
        type = gameObject.tag;
        lurecard = GameObject.Find("Lure").GetComponent<Lure>();

        if (deck.gameObject.tag == "Player1")
        {
            hand = Player1Visual.GetComponentInChildren<Hand>();
            playerturn = Player1.GetComponentInChildren<Turn>();
            opponentturn = Player2.GetComponentInChildren<Turn>();

            if (gameObject.tag == "Melee" || gameObject.tag == "Ranged" || gameObject.tag == "Siege" || gameObject.tag == "Lure")
            {
                card = GetComponent<CardOutput>();
                power = card.powercard;
            }
            else if (gameObject.tag == "Weather" || gameObject.tag == "MeleeIncrease" || gameObject.tag == "RangedIncrease" || gameObject.tag == "SiegeIncrease")
            {
                othercard = GetComponent<OtherCardOutput>();
            }
        }
        else if (deck.gameObject.tag == "Player2")
        {
            hand = Player2Visual.GetComponentInChildren<Hand>();
            playerturn = Player2.GetComponentInChildren<Turn>();
            opponentturn = Player1.GetComponentInChildren<Turn>();
            if (gameObject.tag == "Melee2" || gameObject.tag == "Ranged2" || gameObject.tag == "Siege2" || gameObject.tag == "Lure")
            {
                card = GetComponent<CardOutput>();
                power = card.powercard;
            }
            else if (gameObject.tag == "Weather" || gameObject.tag == "MeleeIncrease2" || gameObject.tag == "RangedIncrease2" || gameObject.tag == "SiegeIncrease2")
            {
                othercard = GetComponent<OtherCardOutput>();
            }
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
        if (card != null)
        {
            CardEffects.CheckCardEffect(card);
            CardEffects.CheckLureEffect(card, lurecard);

            if (card.isonthefield == false)
            {
                card.powercard = power;
            }
        }
        else if (othercard != null)
        {
            CardEffects.CheckWeatherEffect(othercard);
            CardEffects.CheckIncreaseEffect(othercard);

        }
        if (opponentturn.passed == true)
        {
            playerturn.ismyturn = true;
        }

    }
    //Gestiona cuando se está arrastrando el GameObject
    public void OnDrag(PointerEventData eventData)
    {
        if (playerturn.DrawExecuted == true && playerturn.playmade == false)
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
            if (card != null)
            {
                card.isonthefield = true;
                if (card != null && card.card.type == "Lure")
                {
                    if (card.effectexecuted == true && opponentturn.passed == false)
                    {
                        playerturn.playmade = true;
                        playerturn.ismyturn = false;
                        GameFunctions.CheckTurn();
                    }
                    else if (card.effectexecuted == false && opponentturn.passed == false)
                    {
                        playerturn.ismyturn = true;
                        playerturn.playmade = true;
                    }
                }
                else if (card != null && card.card.type != "Lure" && opponentturn.passed == false)
                {
                    playerturn.playmade = true;
                    playerturn.ismyturn = false;
                    GameFunctions.CheckTurn();
                }

            }
            else if (othercard != null)
            {
                othercard.isonthefield = true;
                playerturn.playmade = true;
                playerturn.ismyturn = false;
                GameFunctions.CheckTurn();
            }

        }
    }
    public void OnClick()
    {
        //Funcion para la carta señuelo
        if (gameObject.tag != "Weather" && gameObject.tag != "MeleeIncrease" && gameObject.tag != "MeleeIncrease2" && gameObject.tag != "RangedIncrease" && gameObject.tag != "RangedIncrease2" && gameObject.tag != "SiegeIncrease" && gameObject.tag != "SiegeIncrease2")
        {
            if (GameObject.Find("Card Lure 1") != null && GameObject.Find("Card Lure 1").GetComponent<CardOutput>().isonthefield == true)
            {
                if (gameObject.GetComponent<CardOutput>().card.cardname == "Señuelo de Loki")
                {
                    return;
                }
                else
                {
                    if (!CardEffects.CheckRankCard(gameObject))
                    {
                        if (gameObject.GetComponent<CardOutput>().isonthefield)
                        {
                            lurecard.lurechange = gameObject;
                        }
                    }
                }
            }
            else if (GameObject.Find("Card Lure 2") != null && GameObject.Find("Card Lure 2").GetComponent<CardOutput>().isonthefield == true)
            {
                if (gameObject.GetComponent<CardOutput>().card.cardname == "Señuelo de Loki")
                {
                    return;
                }
                else
                {
                    if (!CardEffects.CheckRankCard(gameObject))
                    {
                        if (gameObject.GetComponent<CardOutput>().isonthefield)
                        {
                            lurecard.lurechange = gameObject;
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
    void OnMouseEnter()
    {
        if (card != null)
        {
            Cardinfo.transform.SetParent(Board.transform);
            Cardinfo.GetComponent<CardInfo>().card = card.card;
            Cardinfo.GetComponent<CardInfo>().picture.sprite = card.card.picture;
            Cardinfo.GetComponent<CardInfo>().nametext.text = card.card.cardname;
            Cardinfo.GetComponent<CardInfo>().description.text = card.card.description;
            Cardinfo.GetComponent<CardInfo>().power.text = card.card.power.ToString();
            Cardinfo.GetComponent<CardInfo>().type.sprite = card.card.typeimage;
            Cardinfo.GetComponent<CardInfo>().rank.sprite = card.card.rankicon;
        }
        else if (othercard != null)
        {
            OtherCardinfo.transform.SetParent(Board.transform);
            OtherCardinfo.GetComponent<OtherCardInfo>().card = othercard.card;
            OtherCardinfo.GetComponent<OtherCardInfo>().picture.sprite = othercard.card.picture;
            OtherCardinfo.GetComponent<OtherCardInfo>().nametext.text = othercard.card.cardname;
            OtherCardinfo.GetComponent<OtherCardInfo>().description.text = othercard.card.description;
            OtherCardinfo.GetComponent<OtherCardInfo>().effecticon.sprite = othercard.card.effecticon;
        }
    }
    void OnMouseExit()
    {
        if (card != null)
        {
            Cardinfo.transform.SetParent(Canvas.transform);
        }
        else if (othercard != null)
        {
            OtherCardinfo.transform.SetParent(Canvas.transform);
        }
    }
}

