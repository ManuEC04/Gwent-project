using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SiegeRow : MonoBehaviour
{
private List<GameObject> rowposition = new List<GameObject>();
public List<GameObject> siegecards = new List<GameObject>();
public GameObject siegezone;
public string fieldtag = "Siege";
public float horizontalpos = 842f;
public float verticalpos = 174f;

void OnTriggerEnter2D(Collider2D other)
    {
     GameFunctions.CardOnField(siegecards,rowposition,fieldtag,other,horizontalpos,verticalpos);
    }
void Update()
    {   
    GameFunctions.ChangeParent(siegecards , siegezone);
    }  
 }