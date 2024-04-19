using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class Card : ScriptableObject
{
    public Sprite picture;
    public string cardname;
    public string description;
    public string faction;
    public string type;
    public bool haseffect = true;

}



