using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeRow : MonoBehaviour
{
   private List<GameObject> rowposition = new List<GameObject>();
   public List<GameObject> meleecards = new List<GameObject>();
   public GameObject meleerow;
   public string fieldtag;
   public float horizontalpos;
   public float verticalpos;
   void OnTriggerEnter2D(Collider2D other)
   {
      GameFunctions.CardOnField(meleecards, rowposition, fieldtag, other, horizontalpos, verticalpos);
   }
   void Update()
   {
      GameFunctions.ChangeParent(meleecards, meleerow);
   }
}



