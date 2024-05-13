using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redraw : MonoBehaviour
{
public Hand playerhand;
public Deck playerdeck;
public Turn playerturn;
public GameObject redraw;
private float horizontalpos;
private float verticalpos;
private float distance;

void Start()
{
redraw.SetActive(false);
horizontalpos = playerhand.horizontalpos;
verticalpos = playerhand.verticalpos;
distance = playerhand.distance;
}  public void WantRedraw()
     {
            GameFunctions.Redraw(playerhand , playerdeck , horizontalpos , verticalpos , distance);
            playerturn.FirstDrawExecuted = true;
            playerturn.DrawExecuted = true;
            redraw.SetActive(false);
     }
     public void DontWantRedraw()
     {
            redraw.SetActive(false);
            playerturn.FirstDrawExecuted = true;
            playerturn.DrawExecuted = true;
     }
}
