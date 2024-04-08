using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new weathercard", menuName = "WeatherCard")]
public class WeatherCard : ScriptableObject
{
    public Sprite picture;
    public string cardname;
    public string description;
    public Sprite effecticon;
    public string faction;
    public string type;

}