
using System.Collections.Generic;
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
    public void FirstDraw()
    {        
    //Comprueba si la función ha sido ejecutada
    if (!FirstDrawExecuted)
    {
        for (int i = 0; i <= 10; i++)
        {
        int k = 0;
           hand.Add(deck[k]);
           deck.RemoveAt(k);
        }
        for(int i = 0 ; i < hand.Count; i++)
        {
            //int k = UnityEngine.Random.Range(0, 24);
            hand[i].transform.position = new Vector3(horizontalpos + distance,verticalpos , 0f);
            distance+=85;
        }
        //Indica que la función se ejecutó
            FirstDrawExecuted = true;
    }
   }
}



    




