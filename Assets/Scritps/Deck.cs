
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // Lista para almacenar las cartas
    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> hand = new List<GameObject>();
    public float horizontalpos;
    public float verticalpos;
    private float distance = 0;
    public bool FirstDrawExecuted = false;
    public static bool DrawExecuted = false;
    public void FirstDraw()
    {
        //Comprueba si la función ha sido ejecutada
        if (FirstDrawExecuted == false)
        {
            for (int i = 0; i <= 9; i++)
            {
                int k = 0;
                hand.Add(deck[k]);
                deck.RemoveAt(k);
            }
            for (int i = 0; i < hand.Count; i++)
            {
                //int k = UnityEngine.Random.Range(0, 24);
                hand[i].transform.position = new Vector3(horizontalpos + distance, verticalpos, 0f);
                distance += 85;
            }
            //Indica que la función se ejecutó
            DrawExecuted = true;
            FirstDrawExecuted = true;
        }
        else if (FirstDrawExecuted && DrawExecuted == false && deck.Count > 2)
        {
            distance = 0;
            {
                for (int i = 0; i < 2; i++)
                {
                    hand.Add(deck[i]);
                    deck.RemoveAt(i);
                }
                DrawExecuted = true;
            }
            for (int i = 0; i < hand.Count; i++)
            {
                //int k = UnityEngine.Random.Range(0, 24);
                hand[i].transform.position = new Vector2(horizontalpos + distance, verticalpos);
                horizontalpos -= 5;
                distance += 85;
            }
        }
        else
        {
            Debug.Log("Se ha quedado sin cartas");
        }
    }
}








