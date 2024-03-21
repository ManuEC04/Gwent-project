using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new card", menuName = "Card")]
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
