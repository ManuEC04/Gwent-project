using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public GameObject Player1Win;
    public GameObject Player2Win;
    public GameObject Empate;

    void Start()
    {
     GameObject Invisible = GameObject.Find("Invisible");
     Player1Win.transform.SetParent(Invisible.transform);
     Player2Win.transform.SetParent(Invisible.transform);
     Empate.transform.SetParent(Invisible.transform);
    }
}

