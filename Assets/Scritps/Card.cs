using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new card" , menuName = "Card")]
public class Card : ScriptableObject
{
public Sprite picture;
public string cardname;
public string description;
public int power;
public string effect;
public Sprite effectimage;

public string type;
public Sprite typeimage;

}
