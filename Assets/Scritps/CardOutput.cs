using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class CardOutput : MonoBehaviour
{
  public UnitCard card;
  public Image picture;
  public Text nametext;
  public Text description;
  public Text power;
  public int powercard;
  public Image type;
  public Image rank;
  public bool effectexecuted;
  public bool isonthefield = false;
  public bool isonthehand = false;
  public bool affectedbyweather = false;
  public bool affectedbyeffect;
  public bool buffed = false;
  void Start()
  {
    picture.sprite = card.picture;
    nametext.text = card.cardname;
    description.text = card.description;
    powercard = card.power;
      type.sprite = card.typeimage;
      rank.sprite = card.rankicon;
  }
  void Update()
  {
    power.text = powercard.ToString();
  }

}
