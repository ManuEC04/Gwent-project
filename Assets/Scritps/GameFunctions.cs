using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class GameFunctions : MonoBehaviour
{
    //Terminar el turno
    public static void EndTurn(GameObject Player1 , GameObject Player2 , Animator animator)
    {
    if(Player2.activeSelf == false)
    {
    animator.SetBool("TurnFinished",true);
    Player1.SetActive(false);
    Player2.SetActive(true);
    }
    else if(Player1.activeSelf == false)
    {
    animator.SetBool("TurnFinished",false);
    Player1.SetActive(true);
    Player2.SetActive(false);
    }
    }
    //Posicionar las cartas en el campo
    public static void CardOnField(List<GameObject>meleecards , List<GameObject>rowposition , string tag , Collider2D other , float horizontalpos , float verticalpos)
    {
        int distance = 0;
        //Comparar si el tipo de la carta coincide con el de la fila
        if (other.gameObject.CompareTag(tag))
        {
         rowposition.Add(other.gameObject);
         meleecards.Add(other.gameObject);

         //Ajustar las posiciones de las cartas en la fila
         for(int i = 0; i < rowposition.Count; i++)
        {
            rowposition[i].transform.position = new Vector3(horizontalpos + distance,verticalpos,0f);
            distance+=85;
        }
        Debug.Log("Ocurrio la colision");
        }
    }
    //Cambiar la jerarquia de las cartas que están en juego
    public static void ChangeParent(List<GameObject> mylist , GameObject gameObject)
    {
       for(int i = 0 ; i < mylist.Count ; i++)
        {
        mylist[i].transform.SetParent(gameObject.transform);
        }
    }
}
