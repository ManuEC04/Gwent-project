using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<GameObject> hand = new List<GameObject>();
    public Deck playerdeck;
    public Turn playerturn;
    public Graveyard graveyard;
    public string handtag;
    public float horizontalpos;
    public float verticalpos;
    public float distance = 0;
    public GameObject redraw;
    private GameObject playerhand;
    void Awake()
    {
        handtag = gameObject.tag;
        if (handtag == "Player1")
        {
            playerturn = GameObject.Find("Player1Turn").GetComponent<Turn>();
            playerdeck = GameObject.Find("Player1Deck").GetComponent<Deck>();
            playerhand = GameObject.Find("Player1Hand");
            graveyard = GameObject.Find("Player1 Graveyard").GetComponent<Graveyard>();
        }
        else if (handtag == "Player2")
        {
            playerturn = GameObject.Find("Player2Turn").GetComponent<Turn>();
            playerdeck = GameObject.Find("Player2Deck").GetComponent<Deck>();
            playerhand = GameObject.Find("Player2Hand");
            graveyard = GameObject.Find("Player2 Graveyard").GetComponent<Graveyard>();
        }
    }
    public void Draw()
    {
        if (playerturn.FirstDrawExecuted == false && !redraw.activeSelf)
        {
            GameFunctions.FirstDraw(playerdeck.deck, hand, horizontalpos, verticalpos, distance);
            GameFunctions.ChangeParent(hand, playerhand);
            redraw.SetActive(true);
            if (playerturn.RedrawExecuted == false)
            {
                playerturn.RedrawExecuted = true;
            }
        }
        else if (playerturn.FirstDrawExecuted == true && playerturn.RedrawExecuted == true && playerturn.DrawExecuted == false)
        {
            GameFunctions.StartRoundDraw(playerdeck.deck, hand, horizontalpos, verticalpos, distance);
            GameFunctions.ChangeParent(hand, playerhand);
            playerturn.DrawExecuted = true;
            playerturn.StartRoundDraw = true;
        }
    }
    void Update()
    {
        GameFunctions.CheckHandCount(hand, graveyard);
    }

}
