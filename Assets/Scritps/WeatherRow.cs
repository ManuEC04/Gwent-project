using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherRow : MonoBehaviour
{
   private List<GameObject> rowposition = new List<GameObject>();
   public List<GameObject> weathercards = new List<GameObject>();
   public GameObject Weatherrow;
   public RectTransform pos;
   public RectTransform cardpos;
   public string fieldtag = "Weather";
   public float horizontalpos = 374;
   public float verticalpos = 545f;
   void Start()
   {
    pos = GetComponent<RectTransform>();
   }
   void OnTriggerEnter2D(Collider2D other)
   {
        cardpos = other.GetComponent<RectTransform>();
        int distance = 0;
        if (other.gameObject.CompareTag(tag))
        {
            rowposition.Add(other.gameObject);
            weathercards.Add(other.gameObject);
            for (int i = 0; i < rowposition.Count; i++)
            {
                rowposition[i].transform.position = pos.position + new Vector3(distance ,0f ,0f);
                distance += 40;
            }
        }
   }
   void Update()
   {
      GameFunctions.ChangeParent(weathercards, Weatherrow);
   }
}
