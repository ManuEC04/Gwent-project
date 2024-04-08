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
public Image type;
public Image rank;


    void Start()
    {
        picture.sprite = card.picture;
        nametext.text = card.cardname;
        description.text = card.description;
        power.text = card.power.ToString();
        type.sprite = card.typeimage;
        rank.sprite = card.rankicon;
    }

}
