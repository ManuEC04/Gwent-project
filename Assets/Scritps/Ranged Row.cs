using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedRow : MonoBehaviour
{
    private List<GameObject> rowposition = new List<GameObject>();
    public List<GameObject> rangedcards = new List<GameObject>();
    public GameObject rangedzone;
    public string fieldtag;
    public float horizontalpos = 842f;
    public float verticalpos = 313f;
    void OnTriggerEnter2D(Collider2D other)
    {
        GameFunctions.CardOnField(rangedcards, rowposition, fieldtag, other, horizontalpos, verticalpos);
    }
    void Update()
    {
        GameFunctions.ChangeParent(rangedcards, rangedzone);
    }
}



