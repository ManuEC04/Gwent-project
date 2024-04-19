using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Graveyard : MonoBehaviour
{
public int horizontalpos;
public int verticalpos;
public List<GameObject> graveyard = new List<GameObject>();

private RectTransform rectTransform;

void Start(){
    rectTransform = GetComponent<RectTransform>();
}
void Update()
{
    GameFunctions.GraveyardPosition(rectTransform , graveyard);
    GameFunctions.CheckCardsOnGraveyard();
}
}
