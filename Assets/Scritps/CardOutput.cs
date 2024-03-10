using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardOutput : MonoBehaviour
{
    // Start is called before the first frame update
public Card card;
public Image picture;
public Text nametext;
public Text description;
public Text power;
public Image type;
//public Text texttype;
public Image effect;
//public Text texteffect;


    void Start()
    {
        picture.sprite = card.picture;
        nametext.text = card.cardname;
        description.text = card.description;
        power.text = card.power.ToString();
        //texttype.text = card.type;
        type.sprite = card.typeimage;
        //texteffect.text = card.effect;
        effect.sprite = card.effectimage;
        //texteffect.text = card.effect;
        
    }

}
