using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new unitcard", menuName = "UnitCard")]
public class UnitCard : ScriptableObject
{
    public Sprite picture;
    public string cardname;
    public string description;
    public int power;
    public string faction;
    public string type;
    public Sprite typeimage;
    public string rank;
    public Sprite rankicon;
}
