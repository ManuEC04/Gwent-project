
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
      public List<GameObject> deck = new List<GameObject>();
    void Start()
    {
        GameFunctions.DeckRandom(deck);
    }
}







