using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OtherCardOutput : MonoBehaviour
{
    public OtherCard card;
    public CardEffects effect;
    public Image picture;
    public Text nametext;
    public Text description;
    public Image effecticon;
    public bool effectexecuted = false;
    public bool isonthefield = false;
    void Start()
    {
        picture.sprite = card.picture;
        nametext.text = card.cardname;
        description.text = card.description;
        effecticon.sprite = card.effecticon;
    }
}
