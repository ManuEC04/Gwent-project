using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeRow : MonoBehaviour
{
private List<GameObject> rowposition = new List<GameObject>();
public List<GameObject> meleecards = new List<GameObject>();
float distance = 0;
void OnTriggerEnter2D(Collider2D other)
    {
        //Comparar si el tipo de la carta coincide con el de la fila
        if (other.gameObject.CompareTag("Melee") || other.gameObject.CompareTag("RangedMelee"))
        {
         rowposition.Add(other.gameObject);
         meleecards.Add(other.gameObject);

         //Ajustar las posiciones de las cartas en la fila
         for(int i = 0; i < rowposition.Count; i++)
        {
            rowposition[i].transform.position = new Vector3(842f + distance,467f , 0);
            rowposition.RemoveAt(i);
            distance+=85;
        }
        Debug.Log("Ocurrio la colision");
        }
    }
 }



