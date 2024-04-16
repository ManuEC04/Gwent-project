using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public Animator animator;
    public static bool haspassed;
    private int count = 0;
    void Start()
    {
        Player2.SetActive(false);
    }
    public void Pass()
    {
        if (Deck.DrawExecuted)
        {
            if (haspassed == false)
            {
                GameFunctions.EndTurn(Player1, Player2, animator);
                haspassed = true;
            }
            count++;
            if (count == 2)
            {
                //TerminarRonda
            }
        }
    }

}