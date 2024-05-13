using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Graveyard : MonoBehaviour
{
public List<GameObject> graveyard = new List<GameObject>();

private RectTransform rectTransform;

void Start(){
    rectTransform = GetComponent<RectTransform>();
}
void Update()
{
    GameFunctions.CheckCardsOnGraveyard();
    GameFunctions.GraveyardPosition(rectTransform , graveyard);
}
}
