using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // Lista para almacenar las cartas
    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> hand = new List<GameObject>();
    private float distance = 1;
     public bool FirstDrawExecuted = false;
    public void FirstDraw(){        
    //Comprueba si la función ha sido ejecutada
    if (!FirstDrawExecuted){
        for (int i = 0; i <= 10; i++)
        {
        int k = 0;
           hand.Add(deck[k]);
           deck.RemoveAt(k);
        }
        for(int i = 0 ; i<hand.Count; i++)
        {
            hand[i].transform.position = new Vector3(120f + distance,370f , 0f);
            distance+=70;
        }
        //Indica que la función se ejecutó
            FirstDrawExecuted = true;
           
    }
}
}



    




