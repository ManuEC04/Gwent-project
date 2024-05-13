using System.Collections.Generic;
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
                //Eliminar la carta con mayor poder en el campo del rival
                ThorEffect(cardoutput);
            }
            else if (cardoutput.card.cardname == "Loki")
            {   //Eliminar la carta con menor poder en el campo del rival
                LokiEffect(cardoutput);
            }
            else if (cardoutput.card.cardname == "Fenrir")
            {
                //Multiplicar por n su ataque siendo n la cantidad de cartas igual a el en el campo
                FenrirEffect(cardoutput);
            }
            else if (cardoutput.card.cardname == "Berserker")
            {
                //Limpia la fila del campo del rival con menos unidades
                BerserkerEffect(cardoutput);
            }
            else if (cardoutput.card.cardname == "Freya")
            {
                //Poner un aumento
                FreyaEffect(cardoutput);
            }
            else if (cardoutput.card.cardname == "Drakkar")
            {
                //Calcular el promedio de las cartas del rival. Luego igualar el poder de todas las cartas a ese promedio
                DrakkarEffect(cardoutput);
            }
            else if (cardoutput.card.cardname == "Baldur")
            {
                //Poner un clima
                BaldurEffect(cardoutput);
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
            else if (card.card.cardname == "Bendición de Baldur")
            {
                //Carta de despeje
                BaldurBlessing();
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

    //Chequear si hay un señuelo en el campo
    public static void CheckLureEffect(CardOutput card, Lure lure)
    {
        if (card.isonthefield == true && card.effectexecuted == false)
        {
            if (card.card.type == "Lure")
            {
                LureEffect(card, lure);
            }
        }
    }
    //Efecto de la carta señuelo
    static void LureEffect(CardOutput card, Lure lure)
    {
        if (card.gameObject.tag == "Melee")
        {
            GameObject Player1Field = GameObject.Find("Player1 Rows");
            MeleeRow meleerow1 = Player1Field.GetComponentInChildren<MeleeRow>();
            RangedRow rangedrow1 = Player1Field.GetComponentInChildren<RangedRow>();
            SiegeRow siegerow1 = Player1Field.GetComponentInChildren<SiegeRow>();
            Hand player1hand;
            if (GameFunctions.CheckFieldNoNull(meleerow1.meleecards, rangedrow1.rangedcards, siegerow1.siegecards) == true)
            {
                if (lure.lurechange != null)
                {
                    player1hand = GameObject.Find("Player1Hand").GetComponent<Hand>();
                    player1hand.hand.Add(lure.lurechange);
                    lure.lurechange.transform.SetParent(player1hand.transform);

                    if (meleerow1.meleecards.Contains(lure.lurechange))
                    {
                        for (int i = 0; i < meleerow1.meleecards.Count; i++)
                        {

                            if (meleerow1.meleecards[i] == lure.lurechange)
                            {
                                meleerow1.meleecards.RemoveAt(i);

                            }
                            else { continue; }
                        }
                    }
                    else if (rangedrow1.rangedcards.Contains(lure.lurechange))
                    {
                        for (int i = 0; i < rangedrow1.rangedcards.Count; i++)
                        {
                            if (rangedrow1.rangedcards[i] == lure.lurechange)
                            {
                                rangedrow1.rangedcards.RemoveAt(i);

                            }
                            else { continue; }
                        }
                    }
                    else if (siegerow1.siegecards.Contains(lure.lurechange))
                    {
                        for (int i = 0; i < siegerow1.siegecards.Count; i++)
                        {
                            if (siegerow1.siegecards[i] == lure.lurechange)
                            {
                                siegerow1.siegecards.RemoveAt(i);

                            }
                            else { continue; }
                        }
                    }
                    GameFunctions.CheckHandPosition(player1hand.hand, player1hand.horizontalpos, player1hand.verticalpos);
                    card.effectexecuted = true;
                    lure.lurechange.GetComponent<CardManager>().isOverDropZone = false;
                    lure.lurechange = null;
                    if (!card.GetComponent<CardManager>().opponentturn.playmade)
                    {
                        card.GetComponent<CardManager>().playerturn.playmade = true;
                    }
                    GameFunctions.CheckTurn();
                }
            }
            else
            {
                card.effectexecuted = true;
                card.GetComponent<CardManager>().playerturn.playmade = true;
                GameFunctions.CheckTurn();
            }
        }
        else if (card.gameObject.tag == "Melee2")
        {
            GameObject Player2Field = GameObject.Find("Player2 Rows");
            MeleeRow meleerow1 = Player2Field.GetComponentInChildren<MeleeRow>();
            RangedRow rangedrow1 = Player2Field.GetComponentInChildren<RangedRow>();
            SiegeRow siegerow1 = Player2Field.GetComponentInChildren<SiegeRow>();
            Hand player1hand;
            if (GameFunctions.CheckFieldNoNull(meleerow1.meleecards, rangedrow1.rangedcards, siegerow1.siegecards) == true)
            {
                if (lure.lurechange != null)
                {
                    player1hand = GameObject.Find("Player2Hand").GetComponent<Hand>();
                    player1hand.hand.Add(lure.lurechange);
                    lure.lurechange.transform.SetParent(player1hand.transform);

                    if (meleerow1.meleecards.Contains(lure.lurechange))
                    {
                        for (int i = 0; i < meleerow1.meleecards.Count; i++)
                        {

                            if (meleerow1.meleecards[i] == lure.lurechange)
                            {
                                meleerow1.meleecards.RemoveAt(i);

                            }
                            else { continue; }
                        }
                    }
                    else if (rangedrow1.rangedcards.Contains(lure.lurechange))
                    {
                        for (int i = 0; i < rangedrow1.rangedcards.Count; i++)
                        {
                            if (rangedrow1.rangedcards[i] == lure.lurechange)
                            {
                                rangedrow1.rangedcards.RemoveAt(i);

                            }
                            else { continue; }
                        }
                    }
                    else if (siegerow1.siegecards.Contains(lure.lurechange))
                    {
                        for (int i = 0; i < siegerow1.siegecards.Count; i++)
                        {
                            if (siegerow1.siegecards[i] == lure.lurechange)
                            {
                                siegerow1.siegecards.RemoveAt(i);
                            }
                            else { continue; }
                        }
                    }
                    GameFunctions.CheckHandPosition(player1hand.hand, player1hand.horizontalpos, player1hand.verticalpos);
                    card.effectexecuted = true;
                    lure.lurechange.GetComponent<CardManager>().isOverDropZone = false;
                    lure.lurechange = null;
                    card.GetComponent<CardManager>().playerturn.playmade = true;
                    GameFunctions.CheckTurn();
                }
            }
            else
            {
                card.effectexecuted = true;
                card.GetComponent<CardManager>().playerturn.playmade = true;
                GameFunctions.CheckTurn();
            }
        }
    }

    // Incrementar el poder de una fila
    static void IncreaseFunction(List<GameObject> player, int n)
    {
        for (int i = 0; i < player.Count; i++)
        {
            if (!CheckRankCard(player[i]))
            {
                if (player[i].GetComponent<CardOutput>().buffed == false)
                {
                    player[i].GetComponent<CardOutput>().powercard += n;
                    player[i].GetComponent<CardOutput>().buffed = true;
                }
                else { continue; }
            }
            else { continue; }
        }
    }
    //Efecto de Mjolnir
    static void MjolnirEffect(OtherCardOutput card)
    {
        MeleeRow meleerow = null;
        if (card.gameObject.tag == "MeleeIncrease")
        {
            meleerow = GameObject.Find("Player1 Melee Row").GetComponent<MeleeRow>();
        }
        else if (card.gameObject.tag == "MeleeIncrease2")
        {
            meleerow = GameObject.Find("Player2 Melee Row").GetComponent<MeleeRow>();
        }
        IncreaseFunction(meleerow.meleecards, 3);
    }
    //Efecto de Gungnir
    static void GungnirEffect(OtherCardOutput card)
    {
        RangedRow rangedrow = null;
        if (card.gameObject.tag == "RangedIncrease")
        {
            rangedrow = GameObject.Find("Player1 Ranged Row").GetComponent<RangedRow>();
        }
        else if (card.gameObject.tag == "RangedIncrease2")
        {
            rangedrow = GameObject.Find("Player2 Ranged Row").GetComponent<RangedRow>();
        }
        IncreaseFunction(rangedrow.rangedcards, 4);
    }
    //Efecto de Gjallahorn
    static void GjallarhornEffect(OtherCardOutput card)
    {
        SiegeRow siegerow = null;
        if (card.gameObject.tag == "SiegeIncrease")
        {
            siegerow = GameObject.Find("Player1 Siege Row").GetComponent<SiegeRow>();
        }
        else if (card.gameObject.tag == "SiegeIncrease2")
        {
            siegerow = GameObject.Find("Player2 Siege Row").GetComponent<SiegeRow>();
        }
        IncreaseFunction(siegerow.siegecards, 2);
    }
    //Verificar si la carta es de tipo oro
    public static bool CheckRankCard(GameObject gameObject)
    {
        CardOutput card = gameObject.GetComponent<CardOutput>();

        if (card.card.rank == "Gold" || card.card.type == "Lure")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Effecto de la carta Fenrir
    static void FenrirEffect(CardOutput fenrir)
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
    //Funcion para disminuir el poder de una fila
    static void WeatherFunction(List<GameObject> player, int n)
    {
        for (int i = 0; i < player.Count; i++)
        {
            if (player[i].GetComponent<CardOutput>().affectedbyweather == false)
            {
                if (!CheckRankCard(player[i]))
                {
                    player[i].GetComponent<CardOutput>().affectedbyweather = true;
                    player[i].GetComponent<CardOutput>().powercard -= n;
                }
                else { continue; }
            }
        }
    }
    //Efecto del clima Supertormenta
    static void SupertormentaEffect()
    {
        GameObject Player1Field = GameObject.Find("Player1 Rows");
        MeleeRow meleerow1 = Player1Field.GetComponentInChildren<MeleeRow>();
        GameObject Player2Field = GameObject.Find("Player2 Rows");
        MeleeRow meleerow2 = Player2Field.GetComponentInChildren<MeleeRow>();
        WeatherFunction(meleerow1.meleecards, 3);
        WeatherFunction(meleerow2.meleecards, 3);
    }
    //Efecto del clima Frio de Nilfheim
    static void NilfheimEffect()
    {
        GameObject Player1Field = GameObject.Find("Player1 Rows");
        SiegeRow siegerow1 = Player1Field.GetComponentInChildren<SiegeRow>();
        GameObject Player2Field = GameObject.Find("Player2 Rows");
        SiegeRow siegerow2 = Player2Field.GetComponentInChildren<SiegeRow>();
        WeatherFunction(siegerow1.siegecards, 4);
        WeatherFunction(siegerow2.siegecards, 4);
    }
    //Efecto del clima Fuego de Hell
    static void AlfheimEffect()
    {
        GameObject Player1Field = GameObject.Find("Player1 Rows");
        RangedRow rangedrow1 = Player1Field.GetComponentInChildren<RangedRow>();
        GameObject Player2Field = GameObject.Find("Player2 Rows");
        RangedRow rangedrow2 = Player2Field.GetComponentInChildren<RangedRow>();
        WeatherFunction(rangedrow1.rangedcards, 2);
        WeatherFunction(rangedrow2.rangedcards, 2);
    }
    //Efecto de Bendicion de Baldur
    static void BaldurBlessing()
    {

        WeatherRow weatherRow = GameObject.Find("WeatherRow").GetComponent<WeatherRow>();
        MeleeRow player1meleerow = GameObject.Find("Player1 Melee Row").GetComponent<MeleeRow>();
        RangedRow player1rangedrow = GameObject.Find("Player1 Ranged Row").GetComponent<RangedRow>();
        SiegeRow player1siegerow = GameObject.Find("Player1 Siege Row").GetComponent<SiegeRow>();
        MeleeRow player2meleerow = GameObject.Find("Player2 Melee Row").GetComponent<MeleeRow>();
        RangedRow player2rangedrow = GameObject.Find("Player2 Ranged Row").GetComponent<RangedRow>();
        SiegeRow player2siegerow = GameObject.Find("Player2 Siege Row").GetComponent<SiegeRow>();
        Graveyard player1graveyard = GameObject.Find("Player1 Graveyard").GetComponent<Graveyard>();

        for (int i = 0; i < weatherRow.weathercards.Count; i++)
        {
            player1graveyard.graveyard.Add(weatherRow.weathercards[i]);
            weatherRow.weathercards[i].GetComponent<OtherCardOutput>().isonthefield = false;
            weatherRow.weathercards[i].transform.SetParent(player1graveyard.transform);
        }
        weatherRow.weathercards.Clear();

        BaldurBlessingRestore(player1meleerow.meleecards, 3);
        BaldurBlessingRestore(player2meleerow.meleecards, 3);
        BaldurBlessingRestore(player1rangedrow.rangedcards, 2);
        BaldurBlessingRestore(player2rangedrow.rangedcards, 2);
        BaldurBlessingRestore(player1siegerow.siegecards, 4);
        BaldurBlessingRestore(player2siegerow.siegecards, 4);

    }
    //Funcion para restaurar el poder cuando una carta esta afectada por clima
    static void BaldurBlessingRestore(List<GameObject> list, int power)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].GetComponent<CardOutput>().affectedbyweather == true)
            {
                list[i].GetComponent<CardOutput>().powercard += power;
                list[i].GetComponent<CardOutput>().affectedbyweather = false;
            }
            else
            {
                continue;
            }
        }

    }
    //Efecto de la carta Thor
    static void ThorEffect(CardOutput thor)
    {
        if (thor.effectexecuted == false)
        {
            CardOutput destroycard = new CardOutput();
            bool isonmeleerow = false;
            bool isonrangedrow = false;
            bool isonsiegerow = false;

            if (thor.tag == "Melee2")
            {
                MeleeRow player1meleerow = GameObject.Find("Player1 Melee Row").GetComponent<MeleeRow>();
                RangedRow player1rangedrow = GameObject.Find("Player1 Ranged Row").GetComponent<RangedRow>();
                SiegeRow player1siegerow = GameObject.Find("Player1 Siege Row").GetComponent<SiegeRow>();
                Graveyard player1graveyard = GameObject.Find("Player1 Graveyard").GetComponent<Graveyard>();
                ThorEffectFunction(player1meleerow, player1rangedrow, player1siegerow, player1graveyard, destroycard, thor, isonmeleerow, isonsiegerow, isonrangedrow);
            }
            else if (thor.tag == "Melee")
            {
                MeleeRow player2meleerow = GameObject.Find("Player2 Melee Row").GetComponent<MeleeRow>();
                RangedRow player2rangedrow = GameObject.Find("Player2 Ranged Row").GetComponent<RangedRow>();
                SiegeRow player2siegerow = GameObject.Find("Player2 Siege Row").GetComponent<SiegeRow>();
                Graveyard player2graveyard = GameObject.Find("Player2 Graveyard").GetComponent<Graveyard>();
                ThorEffectFunction(player2meleerow, player2rangedrow, player2siegerow, player2graveyard, destroycard, thor, isonmeleerow, isonsiegerow, isonrangedrow);
            }
        }

    }
    //Funcion para buscar la carta con mayor poder y destruirla
    static void ThorEffectFunction(MeleeRow player1meleerow, RangedRow player1rangedrow, SiegeRow player1siegerow, Graveyard player1graveyard, CardOutput destroycard, CardOutput thor, bool isonmeleerow, bool isonsiegerow, bool isonrangedrow)
    {

        for (int i = 0; i < player1meleerow.meleecards.Count; i++)
        {
            if (player1meleerow.meleecards != null && player1meleerow.meleecards[i].GetComponent<CardOutput>().powercard > destroycard.powercard || destroycard == null)
            {
                destroycard = player1meleerow.meleecards[i].GetComponent<CardOutput>();
                isonmeleerow = true;
                isonrangedrow = false;
                isonsiegerow = false;
            }
            else { continue; }
        }
        for (int i = 0; i < player1rangedrow.rangedcards.Count; i++)
        {

            if (player1rangedrow.rangedcards != null && player1rangedrow.rangedcards[i].GetComponent<CardOutput>().powercard > destroycard.powercard || destroycard == null)
            {
                destroycard = player1rangedrow.rangedcards[i].GetComponent<CardOutput>();
                isonmeleerow = false;
                isonrangedrow = true;
                isonsiegerow = false;
            }
            else { continue; }

        }
        for (int i = 0; i < player1siegerow.siegecards.Count; i++)
        {
            if (player1siegerow.siegecards != null && player1siegerow.siegecards[i].GetComponent<CardOutput>().powercard > destroycard.powercard || destroycard == null)
            {
                destroycard = player1siegerow.siegecards[i].GetComponent<CardOutput>();
                isonmeleerow = false;
                isonrangedrow = false;
                isonsiegerow = true;
            }
            else { continue; }
        }

        if (isonmeleerow == true)
        {
            for (int i = 0; i < player1meleerow.meleecards.Count; i++)
            {
                if (player1meleerow.meleecards[i].GetComponent<CardOutput>() == destroycard)
                {
                    player1graveyard.graveyard.Add(player1meleerow.meleecards[i]);
                    player1meleerow.meleecards[i].transform.SetParent(player1graveyard.transform);
                    player1meleerow.meleecards.RemoveAt(i);
                }
                else { continue; }
            }
        }
        else if (isonrangedrow == true)
        {
            for (int i = 0; i < player1rangedrow.rangedcards.Count; i++)
            {
                if (player1rangedrow.rangedcards[i].GetComponent<CardOutput>() == destroycard)
                {
                    player1graveyard.graveyard.Add(player1rangedrow.rangedcards[i]);
                    player1rangedrow.rangedcards[i].transform.SetParent(player1graveyard.transform);
                    player1rangedrow.rangedcards.RemoveAt(i);
                }
                else { continue; }
            }
        }
        else if (isonsiegerow == true)
        {
            for (int i = 0; i < player1siegerow.siegecards.Count; i++)
            {
                if (player1siegerow.siegecards[i].GetComponent<CardOutput>() == destroycard)
                {
                    player1graveyard.graveyard.Add(player1siegerow.siegecards[i]);
                    player1siegerow.siegecards[i].transform.SetParent(player1graveyard.transform);
                    player1siegerow.siegecards.RemoveAt(i);
                }
                else { continue; }
            }
        }
        thor.effectexecuted = true;
    }
    //Efecto de la carta Loki
    static void LokiEffect(CardOutput loki)
    {
        if (loki.effectexecuted == false)
        {
            CardOutput destroycard = new CardOutput();
            bool isonmeleerow = false;
            bool isonrangedrow = false;
            bool isonsiegerow = false;

            if (loki.tag == "Ranged2")
            {
                MeleeRow player1meleerow = GameObject.Find("Player1 Melee Row").GetComponent<MeleeRow>();
                RangedRow player1rangedrow = GameObject.Find("Player1 Ranged Row").GetComponent<RangedRow>();
                SiegeRow player1siegerow = GameObject.Find("Player1 Siege Row").GetComponent<SiegeRow>();
                Graveyard player1graveyard = GameObject.Find("Player1 Graveyard").GetComponent<Graveyard>();
                LokiEffectFunction(player1meleerow, player1rangedrow, player1siegerow, player1graveyard, destroycard, loki, isonmeleerow, isonsiegerow, isonrangedrow);
            }
            else if (loki.tag == "Ranged")
            {
                MeleeRow player2meleerow = GameObject.Find("Player2 Melee Row").GetComponent<MeleeRow>();
                RangedRow player2rangedrow = GameObject.Find("Player2 Ranged Row").GetComponent<RangedRow>();
                SiegeRow player2siegerow = GameObject.Find("Player2 Siege Row").GetComponent<SiegeRow>();
                Graveyard player2graveyard = GameObject.Find("Player2 Graveyard").GetComponent<Graveyard>();
                LokiEffectFunction(player2meleerow, player2rangedrow, player2siegerow, player2graveyard, destroycard, loki, isonmeleerow, isonsiegerow, isonrangedrow);
            }
        }

    }
    //Funcion para buscar la carta con menor poder y destruirla
    static void LokiEffectFunction(MeleeRow player1meleerow, RangedRow player1rangedrow, SiegeRow player1siegerow, Graveyard player1graveyard, CardOutput destroycard, CardOutput loki, bool isonmeleerow, bool isonsiegerow, bool isonrangedrow)
    {

        for (int i = 0; i < player1meleerow.meleecards.Count; i++)
        {
            if (player1meleerow.meleecards != null && player1meleerow.meleecards[i].GetComponent<CardOutput>().powercard < destroycard.powercard || destroycard == null)
            {
                destroycard = player1meleerow.meleecards[i].GetComponent<CardOutput>();
                isonmeleerow = true;
                isonrangedrow = false;
                isonsiegerow = false;
            }
            else { continue; }
        }
        for (int i = 0; i < player1rangedrow.rangedcards.Count; i++)
        {

            if (player1rangedrow.rangedcards != null && player1rangedrow.rangedcards[i].GetComponent<CardOutput>().powercard < destroycard.powercard || destroycard == null)
            {
                destroycard = player1rangedrow.rangedcards[i].GetComponent<CardOutput>();
                isonmeleerow = false;
                isonrangedrow = true;
                isonsiegerow = false;
            }
            else { continue; }

        }
        for (int i = 0; i < player1siegerow.siegecards.Count; i++)
        {
            if (player1siegerow.siegecards != null && player1siegerow.siegecards[i].GetComponent<CardOutput>().powercard < destroycard.powercard || destroycard == null)
            {
                destroycard = player1siegerow.siegecards[i].GetComponent<CardOutput>();
                isonmeleerow = false;
                isonrangedrow = false;
                isonsiegerow = true;
            }
            else { continue; }
        }

        if (isonmeleerow == true)
        {
            for (int i = 0; i < player1meleerow.meleecards.Count; i++)
            {
                if (player1meleerow.meleecards[i].GetComponent<CardOutput>() == destroycard)
                {
                    player1graveyard.graveyard.Add(player1meleerow.meleecards[i]);
                    player1meleerow.meleecards[i].transform.SetParent(player1graveyard.transform);
                    player1meleerow.meleecards.RemoveAt(i);
                }
                else { continue; }
            }
        }
        else if (isonrangedrow == true)
        {
            for (int i = 0; i < player1rangedrow.rangedcards.Count; i++)
            {
                if (player1rangedrow.rangedcards[i].GetComponent<CardOutput>() == destroycard)
                {
                    player1graveyard.graveyard.Add(player1rangedrow.rangedcards[i]);
                    player1rangedrow.rangedcards[i].transform.SetParent(player1graveyard.transform);
                    player1rangedrow.rangedcards.RemoveAt(i);
                }
                else { continue; }
            }
        }
        else if (isonsiegerow == true)
        {
            for (int i = 0; i < player1siegerow.siegecards.Count; i++)
            {
                if (player1siegerow.siegecards[i].GetComponent<CardOutput>() == destroycard)
                {
                    player1graveyard.graveyard.Add(player1siegerow.siegecards[i]);
                    player1siegerow.siegecards[i].transform.SetParent(player1graveyard.transform);
                    player1siegerow.siegecards.RemoveAt(i);
                }
                else { continue; }
            }
        }
        loki.effectexecuted = true;
    }
    //Efecto de la carta Berserker
    static void BerserkerEffect(CardOutput berserker)
    {
        if (berserker.effectexecuted == false)
        {
            if (berserker.tag == "Siege2")
            {
                MeleeRow player1meleerow = GameObject.Find("Player1 Melee Row").GetComponent<MeleeRow>();
                RangedRow player1rangedrow = GameObject.Find("Player1 Ranged Row").GetComponent<RangedRow>();
                SiegeRow player1siegerow = GameObject.Find("Player1 Siege Row").GetComponent<SiegeRow>();
                Graveyard player1graveyard = GameObject.Find("Player1 Graveyard").GetComponent<Graveyard>();
                BerserkerEffectFunction(berserker, player1meleerow, player1rangedrow, player1siegerow, player1graveyard);
            }
            else if (berserker.tag == "Siege")
            {
                MeleeRow player2meleerow = GameObject.Find("Player2 Melee Row").GetComponent<MeleeRow>();
                RangedRow player2rangedrow = GameObject.Find("Player2 Ranged Row").GetComponent<RangedRow>();
                SiegeRow player2siegerow = GameObject.Find("Player2 Siege Row").GetComponent<SiegeRow>();
                Graveyard player2graveyard = GameObject.Find("Player2 Graveyard").GetComponent<Graveyard>();
                BerserkerEffectFunction(berserker, player2meleerow, player2rangedrow, player2siegerow, player2graveyard);
            }
        }
    }
    //Funcion para buscar la fila con menor numero de cartas y limpiarla
    static void BerserkerEffectFunction(CardOutput berserker, MeleeRow player1meleerow, RangedRow player1rangedrow, SiegeRow player1siegerow, Graveyard player1graveyard)
    {
        int meleecount = 0;
        int rangedcount = 0;
        int siegecount = 0;

        foreach (GameObject i in player1meleerow.meleecards)
        {
            meleecount++;
        }
        foreach (GameObject i in player1rangedrow.rangedcards)
        {
            rangedcount++;
        }
        foreach (GameObject i in player1siegerow.siegecards)
        {
            siegecount++;
        }
        if (meleecount != 0 && rangedcount != 0 && siegecount != 0)
        {
            if (meleecount < rangedcount && meleecount < siegecount)
            {
                for (int i = 0; i < player1meleerow.meleecards.Count; i++)
                {
                    player1graveyard.graveyard.Add(player1meleerow.meleecards[i]);
                    player1meleerow.meleecards[i].transform.SetParent(player1graveyard.transform);
                }
                player1meleerow.meleecards.Clear();
            }
            else if (rangedcount < meleecount && rangedcount < siegecount)
            {
                for (int i = 0; i < player1rangedrow.rangedcards.Count; i++)
                {
                    player1graveyard.graveyard.Add(player1rangedrow.rangedcards[i]);
                    player1rangedrow.rangedcards[i].transform.SetParent(player1graveyard.transform);
                }
                player1rangedrow.rangedcards.Clear();
            }
            else if (siegecount < meleecount && siegecount < rangedcount)
            {
                for (int i = 0; i < player1siegerow.siegecards.Count; i++)
                {
                    player1graveyard.graveyard.Add(player1siegerow.siegecards[i]);
                    player1siegerow.siegecards[i].transform.SetParent(player1graveyard.transform);
                }
                player1siegerow.siegecards.Clear();
            }
            else
            {
                int k = Random.Range(0, 2);
                if (k == 0)
                {
                    for (int i = 0; i < player1meleerow.meleecards.Count; i++)
                    {
                        player1graveyard.graveyard.Add(player1meleerow.meleecards[i]);
                        player1meleerow.meleecards[i].transform.SetParent(player1graveyard.transform);

                    }
                    player1meleerow.meleecards.Clear();
                }
                else if (k == 1)
                {
                    for (int i = 0; i < player1rangedrow.rangedcards.Count; i++)
                    {
                        player1graveyard.graveyard.Add(player1rangedrow.rangedcards[i]);
                        player1rangedrow.rangedcards[i].transform.SetParent(player1graveyard.transform);
                    }
                    player1rangedrow.rangedcards.Clear();
                }
                else
                {
                    for (int i = 0; i < player1siegerow.siegecards.Count; i++)
                    {
                        player1graveyard.graveyard.Add(player1siegerow.siegecards[i]);
                        player1siegerow.siegecards[i].transform.SetParent(player1graveyard.transform);
                    }
                    player1siegerow.siegecards.Clear();
                }
            }
        }
        else if (meleecount == 0 && rangedcount != 0 && siegecount != 0)
        {
            if (rangedcount < meleecount)
            {
                for (int i = 0; i < player1rangedrow.rangedcards.Count; i++)
                {
                    player1graveyard.graveyard.Add(player1rangedrow.rangedcards[i]);
                    player1rangedrow.rangedcards[i].transform.SetParent(player1graveyard.transform);
                }
                player1rangedrow.rangedcards.Clear();
            }
            else
            {
                for (int i = 0; i < player1siegerow.siegecards.Count; i++)
                {
                    player1graveyard.graveyard.Add(player1siegerow.siegecards[i]);
                    player1siegerow.siegecards[i].transform.SetParent(player1graveyard.transform);
                }
                player1siegerow.siegecards.Clear();
            }
        }
        else if (meleecount != 0 && rangedcount == 0 && siegecount != 0)
        {
            if (meleecount < siegecount)
            {
                for (int i = 0; i < player1meleerow.meleecards.Count; i++)
                {
                    player1graveyard.graveyard.Add(player1meleerow.meleecards[i]);
                    player1meleerow.meleecards[i].transform.SetParent(player1graveyard.transform);
                }
                player1meleerow.meleecards.Clear();
            }
            else
            {
                for (int i = 0; i < player1siegerow.siegecards.Count; i++)
                {
                    player1graveyard.graveyard.Add(player1siegerow.siegecards[i]);
                    player1siegerow.siegecards[i].transform.SetParent(player1graveyard.transform);
                }
                player1siegerow.siegecards.Clear();
            }
        }
        else if (meleecount != 0 && rangedcount != 0 && siegecount == 0)
        {
            if (meleecount < rangedcount)
            {
                for (int i = 0; i < player1meleerow.meleecards.Count; i++)
                {
                    player1graveyard.graveyard.Add(player1meleerow.meleecards[i]);
                    player1meleerow.meleecards[i].transform.SetParent(player1graveyard.transform);
                }
                player1meleerow.meleecards.Clear();
            }
            else
            {
                for (int i = 0; i < player1rangedrow.rangedcards.Count; i++)
                {
                    player1graveyard.graveyard.Add(player1rangedrow.rangedcards[i]);
                    player1rangedrow.rangedcards[i].transform.SetParent(player1graveyard.transform);
                }
                player1rangedrow.rangedcards.Clear();
            }
        }
        else if (meleecount != 0 && rangedcount == 0 && siegecount == 0)
        {
            for (int i = 0; i < player1meleerow.meleecards.Count; i++)
            {
                player1graveyard.graveyard.Add(player1meleerow.meleecards[i]);
                player1meleerow.meleecards[i].transform.SetParent(player1graveyard.transform);
            }
            player1meleerow.meleecards.Clear();
        }
        else if (meleecount == 0 && rangedcount != 0 && siegecount == 0)
        {
            for (int i = 0; i < player1rangedrow.rangedcards.Count; i++)
            {
                player1graveyard.graveyard.Add(player1rangedrow.rangedcards[i]);
                player1rangedrow.rangedcards[i].transform.SetParent(player1graveyard.transform);
            }
            player1rangedrow.rangedcards.Clear();
        }
        else if (meleecount == 0 && rangedcount == 0 && siegecount != 0)
        {
            for (int i = 0; i < player1siegerow.siegecards.Count; i++)
            {

                player1graveyard.graveyard.Add(player1siegerow.siegecards[i]);
                player1siegerow.siegecards[i].transform.SetParent(player1graveyard.transform);
            }
            player1siegerow.siegecards.Clear();
        }
        berserker.effectexecuted = true;
    }
    //Efecto de la carta Freya
    static void FreyaEffect(CardOutput freya)
    {
        if (freya.effectexecuted == false)
        {
            if (freya.tag == "Ranged2")
            {
                Deck player2deck = GameObject.Find("Player2Deck").GetComponent<Deck>();
                GameObject meleeincrease = GameObject.Find("Player2 MeleeIncrease");
                GameObject rangedincrease = GameObject.Find("Player2 RangedIncrease");
                GameObject siegeincrease = GameObject.Find("Player2 SiegeIncrease");
                FreyaEffectFunction(freya, player2deck, meleeincrease, rangedincrease, siegeincrease);
                freya.effectexecuted = true;
            }
            else if (freya.tag == "Ranged")
            {
                Deck player1deck = GameObject.Find("Player1Deck").GetComponent<Deck>();
                GameObject meleeincrease = GameObject.Find("Player1 MeleeIncrease");
                GameObject rangedincrease = GameObject.Find("Player1 RangedIncrease");
                GameObject siegeincrease = GameObject.Find("Player1 SiegeIncrease");
                FreyaEffectFunction(freya, player1deck, meleeincrease, rangedincrease, siegeincrease);
                freya.effectexecuted = true;
            }
        }
    }
    //Funcion para buscar un Incremento en el mazo y llevarlo al campo
    static void FreyaEffectFunction(CardOutput freya, Deck player2deck, GameObject meleeincrease, GameObject rangedincrease, GameObject siegeincrease)
    {
        for (int i = 0; i < player2deck.deck.Count; i++)
        {
            if (player2deck.deck[i] != null && player2deck.deck[i].GetComponent<OtherCardOutput>() != null)
            {
                if (player2deck.deck[i].GetComponent<OtherCardOutput>().card.cardname == "Mjolnir")
                {
                    meleeincrease.GetComponent<IncreaseBox>().increasebox.Add(player2deck.deck[i]);
                    player2deck.deck[i].transform.SetParent(meleeincrease.transform);
                    player2deck.deck[i].transform.position = meleeincrease.transform.position;
                    player2deck.deck[i].GetComponent<OtherCardOutput>().isonthefield = true;
                    player2deck.deck.RemoveAt(i);
                    return;
                }
                else if (freya.effectexecuted == false && player2deck.deck[i].GetComponent<OtherCardOutput>().card.cardname == "Gungnir")
                {
                    rangedincrease.GetComponent<IncreaseBox>().increasebox.Add(player2deck.deck[i]);
                    player2deck.deck[i].transform.SetParent(rangedincrease.transform);
                    player2deck.deck[i].transform.position = rangedincrease.transform.position;
                    player2deck.deck[i].GetComponent<OtherCardOutput>().isonthefield = true;
                    player2deck.deck.RemoveAt(i);
                    return;
                }
                else if (freya.effectexecuted == false && player2deck.deck[i].GetComponent<OtherCardOutput>().card.cardname == "Gjallarhorn")
                {
                    siegeincrease.GetComponent<IncreaseBox>().increasebox.Add(player2deck.deck[i]);
                    player2deck.deck[i].transform.SetParent(siegeincrease.transform);
                    player2deck.deck[i].transform.position = siegeincrease.transform.position;
                    player2deck.deck[i].GetComponent<OtherCardOutput>().isonthefield = true;
                    player2deck.deck.RemoveAt(i);
                    return;
                }
            }
            else { continue; }
        }
    }
    //Efecto de la carta Baldur
    static void BaldurEffect(CardOutput Baldur)
    {
        if (Baldur.effectexecuted == false)
        {
            if (Baldur.tag == "Melee")
            {
                Deck player1deck = GameObject.Find("Player1Deck").GetComponent<Deck>();
                GameObject weatherrow = GameObject.Find("WeatherRow");
                for (int i = 0; i < player1deck.deck.Count; i++)
                {
                    if (player1deck.deck[i] != null && player1deck.deck[i].GetComponent<OtherCardOutput>() != null)
                    {
                        if (player1deck.deck[i].GetComponent<OtherCardOutput>().card.type == "Weather" && player1deck.deck[i].GetComponent<OtherCardOutput>().card.cardname != "Bendición de Baldur")
                        {
                            weatherrow.GetComponent<WeatherRow>().weathercards.Add(player1deck.deck[i]);
                            player1deck.deck[i].transform.SetParent(weatherrow.transform);
                            player1deck.deck[i].transform.position = weatherrow.transform.position;
                            player1deck.deck[i].GetComponent<OtherCardOutput>().isonthefield = true;
                            player1deck.deck.RemoveAt(i);
                            Baldur.effectexecuted = true;
                            return;
                        }
                    }
                    else { continue; }
                }
            }
            else if (Baldur.tag == "Melee2")
            {
                Deck player2deck = GameObject.Find("Player2Deck").GetComponent<Deck>();
                GameObject weatherrow = GameObject.Find("WeatherRow");
                for (int i = 0; i < player2deck.deck.Count; i++)
                {
                    if (player2deck.deck[i] != null && player2deck.deck[i].GetComponent<OtherCardOutput>() != null)
                    {
                        if (player2deck.deck[i].GetComponent<OtherCardOutput>().card.type == "Weather" && player2deck.deck[i].GetComponent<OtherCardOutput>().card.cardname != "Bendición de Baldur")
                        {
                            weatherrow.GetComponent<WeatherRow>().weathercards.Add(player2deck.deck[i]);
                            player2deck.deck[i].transform.SetParent(weatherrow.transform);
                            player2deck.deck[i].transform.position = weatherrow.transform.position;
                            player2deck.deck[i].GetComponent<OtherCardOutput>().isonthefield = true;
                            player2deck.deck.RemoveAt(i);
                            Baldur.effectexecuted = true;
                            return;
                        }
                    }
                    else { continue; }
                }
            }

        }
    }
    //Efecto de la carta Drakkar
    static void DrakkarEffect(CardOutput drakkar)
    {
        if (drakkar.effectexecuted == false)
        {
            MeleeRow player1meleerow = GameObject.Find("Player1 Melee Row").GetComponent<MeleeRow>();
            RangedRow player1rangedrow = GameObject.Find("Player1 Ranged Row").GetComponent<RangedRow>();
            SiegeRow player1siegerow = GameObject.Find("Player1 Siege Row").GetComponent<SiegeRow>();
            MeleeRow player2meleerow = GameObject.Find("Player2 Melee Row").GetComponent<MeleeRow>();
            RangedRow player2rangedrow = GameObject.Find("Player2 Ranged Row").GetComponent<RangedRow>();
            SiegeRow player2siegerow = GameObject.Find("Player2 Siege Row").GetComponent<SiegeRow>();
            int powertotal;
            int count;

            if (drakkar.tag == "Siege")
            {
                powertotal = 0;
                count = 0;

                foreach (GameObject i in player2meleerow.meleecards)
                {
                    powertotal += i.GetComponent<CardOutput>().powercard;
                    count++;
                }
                foreach (GameObject i in player2rangedrow.rangedcards)
                {
                    powertotal += i.GetComponent<CardOutput>().powercard;
                    count++;
                }
                foreach (GameObject i in player2siegerow.siegecards)
                {
                    powertotal += i.GetComponent<CardOutput>().powercard;
                    count++;
                }
                if (count != 0)
                {
                    powertotal = powertotal / count;
                }
                else { powertotal = 0; }
                foreach (GameObject i in player1meleerow.meleecards)
                {
                    i.GetComponent<CardOutput>().powercard = powertotal;
                }
                foreach (GameObject i in player1rangedrow.rangedcards)
                {
                    i.GetComponent<CardOutput>().powercard = powertotal;
                }
                foreach (GameObject i in player1siegerow.siegecards)
                {
                    i.GetComponent<CardOutput>().powercard = powertotal;
                }
            }
            else if (drakkar.tag == "Siege2")
            {
                powertotal = 0;
                count = 0;

                foreach (GameObject i in player1meleerow.meleecards)
                {
                    powertotal += i.GetComponent<CardOutput>().powercard;
                    count++;
                }
                foreach (GameObject i in player1rangedrow.rangedcards)
                {
                    powertotal += i.GetComponent<CardOutput>().powercard;
                    count++;
                }
                foreach (GameObject i in player1siegerow.siegecards)
                {
                    powertotal += i.GetComponent<CardOutput>().powercard;
                    count++;
                }
                if (count != 0)
                {
                    powertotal = powertotal / count;
                }
                else { powertotal = 0; }
                foreach (GameObject i in player2meleerow.meleecards)
                {
                    i.GetComponent<CardOutput>().powercard = powertotal;
                }
                foreach (GameObject i in player2rangedrow.rangedcards)
                {
                    i.GetComponent<CardOutput>().powercard = powertotal;
                }
                foreach (GameObject i in player2siegerow.siegecards)
                {
                    i.GetComponent<CardOutput>().powercard = powertotal;
                }
            }
            drakkar.effectexecuted = true;
        }
    }
}


