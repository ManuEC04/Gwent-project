using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CardEffects : MonoBehaviour
{
    //Efecto de la carta lider

    public void Drawcard()
    {
        Turn playerturn = GetComponentInParent<Turn>();
        OtherCardOutput FactionLeader = new OtherCardOutput();
        Deck playerdeck = new Deck();
        Hand playerhand = new Hand();
        Graveyard graveyard = new Graveyard();
        if (playerturn.tag == "Player1")
        {
            FactionLeader = GameObject.Find("FactionLeader Player1").GetComponent<OtherCardOutput>();
            playerdeck = GameObject.Find("Player1Deck").GetComponent<Deck>();
            playerhand = GameObject.Find("Player1Hand").GetComponent<Hand>();
            graveyard = GameObject.Find("Player1 Graveyard").GetComponent<Graveyard>();
        }
        else if (playerturn.tag == "Player2")
        {
            FactionLeader = GameObject.Find("FactionLeader Player2").GetComponent<OtherCardOutput>();
            playerdeck = GameObject.Find("Player2Deck").GetComponent<Deck>();
            playerhand = GameObject.Find("Player2Hand").GetComponent<Hand>();
            graveyard = GameObject.Find("Player2 Graveyard").GetComponent<Graveyard>();
        }
        if (playerturn.ismyturn == true && playerturn.DrawExecuted == true && FactionLeader.effectexecuted == false)
        {
            int i = Random.Range(0, playerdeck.deck.Count);
            if (playerhand.hand.Count == 10)
            {
                graveyard.graveyard.Add(playerdeck.deck[i]);
                playerdeck.deck.RemoveAt(i);
            }
            else if (playerhand.hand.Count < 10)
            {
                playerhand.hand.Add(playerdeck.deck[i]);
                playerdeck.deck.RemoveAt(i);
                for (int k = 0; k < playerhand.hand.Count; k++)
                {
                    playerhand.hand[k].transform.position = new Vector3(playerhand.horizontalpos + playerhand.distance, playerhand.verticalpos, 0f);
                    playerhand.distance += 85;
                }

            }
            Debug.Log("Se ha activado el efecto del lider");
            FactionLeader.effectexecuted = true;
        }
    }
    //Buscar cual es el efecto de la carta de unidad

    public static void CheckCardEffect(CardOutput cardoutput)
    {
        if (cardoutput.card.haseffect == true && cardoutput.isonthefield == true && cardoutput.effectexecuted == false)
        {
            if (cardoutput.card.cardname == "Thor")
            {
                //Eliminar la carta con mas poder del rival
            }
            else if (cardoutput.card.cardname == "Loki")
            {
                //Eliminar la carta con menos poder del rival
            }
            else if (cardoutput.card.cardname == "Fenrir")
            {
                //Multiplicar por n su ataque siendo n la cantidad de cartas igual a el en el campo
                FenrirEffect(cardoutput);
            }
            else if (cardoutput.card.cardname == "Berserker")
            {
                //Limpia la fila del campo del rival con menos unidades
            }
            else if (cardoutput.card.cardname == "Freya")
            {
                //Poner un aumento
            }
            else if (cardoutput.card.cardname == "Drakkar")
            {
                //Calcular el promedio de las cartas del rival. Luego igualar el poder de todas las cartas a ese promedio
            }
            else if (cardoutput.card.cardname == "Baldur")
            {
                //Poner un clima
            }
        }
        else
        {
            return;
        }
    }
    //Buscar cual es el efecto de la carta de clima
    public static void CheckWeatherEffect(OtherCardOutput card)
    {
        if (card.isonthefield == true)
        {
            if (card.card.cardname == "Supertormenta")
            {
                //Disminuye en 3 el poder de las filas de Melee
                SupertormentaEffect();
            }
           else if (card.card.cardname == "Frio de Nilfheim")
            {
                //Disminuye en 2 el poder de las filas de Asedio
                NilfheimEffect();
            }
            else if (card.card.cardname == "Destello de Alfheim")
            {
                //Disminuye en 4 el poder de las filas de Rango
                AlfheimEffect();
            }
        }
    }
    //Chequear los efectos de los incrementos
    public static void CheckIncreaseEffect(OtherCardOutput card)
    {
        if (card.isonthefield == true)
        {
            if (card.card.cardname == "Mjolnir")
            {
                //Aumenta en 3 el poder de las filas de melee
                MjolnirEffect(card);
            }
           else if (card.card.cardname == "Gungnir")
            {
                //Aumenta en 4 el poder de las filas de Rango
                GungnirEffect(card);
               
            }
            else if (card.card.cardname == "Gjallarhorn")
            {
                //Aumenta en 2 el poder de las filas de Asedio
                GjallarhornEffect(card);
            }
        }
    }
    public static void MjolnirEffect(OtherCardOutput card)
    {
        MeleeRow meleerow = null;
        if(card.gameObject.tag == "MeleeIncrease")
        {
            meleerow = GameObject.Find("Player1 Melee Row").GetComponent<MeleeRow>();
        }
        else if (card.gameObject.tag == "MeleeIncrease2")
        {
            meleerow = GameObject.Find("Player2 Melee Row").GetComponent<MeleeRow>();
        }
        for(int i = 0 ; i < meleerow.meleecards.Count ; i++)
            {
                if(!CheckRankCard(meleerow.meleecards[i]))
                {
                    if(meleerow.meleecards[i].GetComponent<CardOutput>().affectedbyeffect == false)
                    {
                    meleerow.meleecards[i].GetComponent<CardOutput>().powercard += 3;
                    meleerow.meleecards[i].GetComponent<CardOutput>().affectedbyeffect = true;
                    }
                    else{continue;}
                }
                else{continue;}
            }
    }
    //Efecto de Gungnir
    public static void GungnirEffect(OtherCardOutput card)
    {
        RangedRow rangedrow = null;
        if(card.gameObject.tag == "RangedIncrease")
        {
            rangedrow = GameObject.Find("Player1 Ranged Row").GetComponent<RangedRow>();
        }
        else if (card.gameObject.tag == "RangedIncrease2")
        {
            rangedrow = GameObject.Find("Player2 Ranged Row").GetComponent<RangedRow>();
        }
        for(int i = 0 ; i < rangedrow.rangedcards.Count ; i++)
            {
                if(!CheckRankCard(rangedrow.rangedcards[i]))
                {
                    if(rangedrow.rangedcards[i].GetComponent<CardOutput>().affectedbyeffect == false)
                    {
                    rangedrow.rangedcards[i].GetComponent<CardOutput>().powercard += 4;
                    rangedrow.rangedcards[i].GetComponent<CardOutput>().affectedbyeffect = true;
                    }
                    else{continue;}
                }
                else{continue;}
            }
    }
    public static void GjallarhornEffect(OtherCardOutput card)
    {
        SiegeRow siegerow = null;
        if(card.gameObject.tag == "SiegeIncrease")
        {
            siegerow = GameObject.Find("Player1 Siege Row").GetComponent<SiegeRow>();
        }
        else if (card.gameObject.tag == "SiegeIncrease2")
        {
            siegerow = GameObject.Find("Player2 Siege Row").GetComponent<SiegeRow>();
        }
        for(int i = 0 ; i < siegerow.siegecards.Count ; i++)
            {
                if(!CheckRankCard(siegerow.siegecards[i]))
                {
                    if(siegerow.siegecards[i].GetComponent<CardOutput>().affectedbyeffect == false)
                    {
                    siegerow.siegecards[i].GetComponent<CardOutput>().powercard += 2;
                    siegerow.siegecards[i].GetComponent<CardOutput>().affectedbyeffect = true;
                    }
                    else{continue;}
                }
                else{continue;}
            }
    }

    //Verificar si la carta es de tipo oro
    public static bool CheckRankCard(GameObject gameObject)
    {
        CardOutput card = gameObject.GetComponent<CardOutput>();

        if (card.card.rank == "Gold")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Effecto de la carta Fenrir
    public static void FenrirEffect(CardOutput fenrir)
    {
        fenrir.affectedbyeffect = true;
        int n = 0;
        GameObject Player1Field = GameObject.Find("Player1 Rows");
        RangedRow rangedrow1 = Player1Field.GetComponentInChildren<RangedRow>();
        GameObject Player2Field = GameObject.Find("Player2 Rows");
        RangedRow rangedrow2 = Player2Field.GetComponentInChildren<RangedRow>();

        for (int i = 0; i < rangedrow1.rangedcards.Count; i++)
        {
            if (rangedrow1.rangedcards[i].GetComponent<CardOutput>().card.cardname == "Fenrir")
            {
                n++;
            }
        }
        for (int i = 0; i < rangedrow2.rangedcards.Count; i++)
        {
            if (rangedrow2.rangedcards[i].GetComponent<CardOutput>().card.cardname == "Fenrir")
            {
                n++;
            }
        }
        fenrir.powercard = fenrir.powercard * n;
        fenrir.effectexecuted = true;
    }
    //Efecto del clima Supertormenta
    public static void SupertormentaEffect()
    {
        GameObject Player1Field = GameObject.Find("Player1 Rows");
        MeleeRow meleerow1 = Player1Field.GetComponentInChildren<MeleeRow>();
        GameObject Player2Field = GameObject.Find("Player2 Rows");
        MeleeRow meleerow2 = Player2Field.GetComponentInChildren<MeleeRow>();

        for(int i = 0 ; i < meleerow1.meleecards.Count ; i++)
        {
            if(meleerow1.meleecards[i].GetComponent<CardOutput>().affectedbyweather == false)
            {
                if(!CheckRankCard(meleerow1.meleecards[i]))
                {
                meleerow1.meleecards[i].GetComponent<CardOutput>().affectedbyweather = true;
                meleerow1.meleecards[i].GetComponent<CardOutput>().powercard -=3;
                }
                else{continue;}
            }
        }
        for(int i = 0 ; i < meleerow2.meleecards.Count ; i++)
        {
            if(meleerow2.meleecards[i].GetComponent<CardOutput>().affectedbyweather == false)
            {   if(!CheckRankCard(meleerow2.meleecards[i]))
            {
                meleerow2.meleecards[i].GetComponent<CardOutput>().affectedbyweather = true;
                meleerow2.meleecards[i].GetComponent<CardOutput>().powercard -=3;
                }
                else{continue;}
            }
        }
    }
    //Efecto del clima Frio de Nilfheim
    public static void NilfheimEffect()
    {
        GameObject Player1Field = GameObject.Find("Player1 Rows");
        SiegeRow siegerow1 = Player1Field.GetComponentInChildren<SiegeRow>();
        GameObject Player2Field = GameObject.Find("Player2 Rows");
        SiegeRow siegerow2 = Player2Field.GetComponentInChildren<SiegeRow>();

        for(int i = 0 ; i < siegerow1.siegecards.Count ; i++)
        {
            if(siegerow1.siegecards[i].GetComponent<CardOutput>().affectedbyweather == false)
            {
                if(!CheckRankCard(siegerow1.siegecards[i])){
                siegerow1.siegecards[i].GetComponent<CardOutput>().affectedbyweather = true;
                siegerow1.siegecards[i].GetComponent<CardOutput>().powercard -=4;
                }
                else{continue;}
            }
        }
        for(int i = 0 ; i < siegerow2.siegecards.Count ; i++)
        {
            if(siegerow2.siegecards[i].GetComponent<CardOutput>().affectedbyweather == false)
            {
                if(!CheckRankCard(siegerow2.siegecards[i])){
                siegerow2.siegecards[i].GetComponent<CardOutput>().affectedbyweather = true;
                siegerow2.siegecards[i].GetComponent<CardOutput>().powercard -=4;
                }
                else{continue;}
            }
        }
    }
    //Efecto del clima Fuego de Hell
    public static void AlfheimEffect()
    {
        GameObject Player1Field = GameObject.Find("Player1 Rows");
        RangedRow rangedrow1 = Player1Field.GetComponentInChildren<RangedRow>();
        GameObject Player2Field = GameObject.Find("Player2 Rows");
        RangedRow rangedrow2 = Player2Field.GetComponentInChildren<RangedRow>();

        for(int i = 0 ; i < rangedrow1.rangedcards.Count ; i++)
        {
            if(rangedrow1.rangedcards[i].GetComponent<CardOutput>().affectedbyweather == false)
            {
                if(!CheckRankCard(rangedrow1.rangedcards[i])){
                rangedrow1.rangedcards[i].GetComponent<CardOutput>().affectedbyweather = true;
                rangedrow1.rangedcards[i].GetComponent<CardOutput>().powercard -=2;
                }
                else{continue;}
            }
        }
        for(int i = 0 ; i < rangedrow2.rangedcards.Count ; i++)
        {
            if(rangedrow2.rangedcards[i].GetComponent<CardOutput>().affectedbyweather == false)
            {
                if(!CheckRankCard(rangedrow2.rangedcards[i])){
                rangedrow2.rangedcards[i].GetComponent<CardOutput>().affectedbyweather = true;
                rangedrow2.rangedcards[i].GetComponent<CardOutput>().powercard -=2;
                }
                else{continue;}
            }
        }
    }

}



