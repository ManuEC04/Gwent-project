using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new factionleadercard", menuName = "FactionLeaderCard")]
public class FactionLeaderCard : ScriptableObject
{
   public Sprite picture;
   public string cardname;
   public string description;
   public string faction;
   public string type;

}
