using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public GameObject PlayerLife1;
    public GameObject PlayerLife2;
  
public int playerlife = 2;

void Start()
{

if(gameObject.tag == "Player1")
{
  PlayerLife1 = GameObject.Find("Player1Life1");
  PlayerLife2 = GameObject.Find("Player1Life2");
}
else if(gameObject.tag == "Player2")
{
  PlayerLife1 = GameObject.Find("Player2Life1");
  PlayerLife2 = GameObject.Find("Player2Life2");
}
}
void Update()
{
  if(playerlife == 1)
  {
    Destroy(PlayerLife1);
  }
  else if (playerlife == 0)
  {
    Destroy(PlayerLife2);
  }
}
}

