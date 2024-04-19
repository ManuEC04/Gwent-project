using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncreaseBox : MonoBehaviour
{
   public GameObject Increase;
   public List<GameObject> increasebox = new List<GameObject>();
   public string fieldtag;

   void OnTriggerEnter2D(Collider2D other)
   {
      GameFunctions.IncreaseOnField(Increase , increasebox , fieldtag , other);
   }
}
