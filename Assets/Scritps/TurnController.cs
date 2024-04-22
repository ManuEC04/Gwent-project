using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public Turn player1turn;
    public Turn player2turn;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player1Visual;
    public GameObject Player2Visual;

    public static bool Round1 = false;
    public static bool Round2 = false;
    public static bool Round3 = false;
    void Start()
    {
        player1turn = GameObject.Find("Player1Turn").GetComponent<Turn>();
        player2turn = GameObject.Find("Player2Turn").GetComponent<Turn>();
        Player2Visual.SetActive(false);
        player2turn.ismyturn = false;
        player1turn.ismyturn = true;
    }
    void Update()
    {
        GameFunctions.CheckVisualTurn(Player1, Player2, Player1Visual, Player2Visual);
    }


}