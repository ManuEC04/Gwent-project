using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameFunctions : MonoBehaviour
{

    //Barajear el mazo
    public static void DeckRandom(List<GameObject> deck)
    {
        int k = 0;
        List<int> index = new List<int>();
        for (int i = 0; i < deck.Count; i++)
        {
            k = Random.Range(0, deck.Count);
            if (!index.Contains(k))
            {
                index.Add(k);
            }
            else
            {
                i--;
            }
        }
        for (int i = 0; i < deck.Count; i++)
        {
            GameObject temp = deck[i];
            deck[i] = deck[index[i]];
            deck[index[i]] = temp;
        }
    }
    //Robar 10 Cartas al inicio de la primera ronda
    public static void FirstDraw(List<GameObject> deck, List<GameObject> hand, float horizontalpos, float verticalpos, float distance)
    {
        //DeckRandom(deck);
        for (int i = 0; i <= 9; i++)
        {
            int k = 0;
            hand.Add(deck[k]);
            deck.RemoveAt(k);
        }
        for (int i = 0; i < hand.Count; i++)
        {
            //int k = UnityEngine.Random.Range(0, 24);
            hand[i].transform.position = new Vector3(horizontalpos + distance, verticalpos, 0f);
            distance += 85;
        }

    }
    //Robar 2 cartas al inicio e cada ronda
    public static void StartRoundDraw(List<GameObject> deck, List<GameObject> hand, float horizontalpos, float verticalpos, float distance)
    {

        distance = 0;
        {
            for (int i = 0; i < 2; i++)
            {
                hand.Add(deck[i]);
                deck.RemoveAt(i);
            }
        }
        for (int i = 0; i < hand.Count; i++)
        {
            //int k = UnityEngine.Random.Range(0, 24);
            hand[i].transform.position = new Vector2(horizontalpos + distance, verticalpos);
            horizontalpos -= 5;
            distance += 85;
        }
    }
    //Verificar que jugador esta activado 
    public static void CheckVisualTurn(GameObject Player1, GameObject Player2, GameObject Player1Visual, GameObject Player2Visual)
    {
        Animator animator = GameObject.Find("Board").GetComponent<Animator>();
        Turn player1turn = Player1.GetComponentInChildren<Turn>();
        Turn player2turn = Player2.GetComponentInChildren<Turn>();
        if (player1turn.ismyturn == false)
        {
            animator.SetBool("TurnFinished", true);
            Player1Visual.SetActive(false);
            Player2Visual.SetActive(true);
        }
        else if (player2turn.ismyturn == false)
        {
            animator.SetBool("TurnFinished", false);
            Player1Visual.SetActive(true);
            Player2Visual.SetActive(false);
        }
    }
    public static void CheckTurn()
    {
        TurnController turnController = GameObject.Find("Turn Controller").GetComponent<TurnController>();
        if (turnController.player1turn.playmade == true)
        {
            turnController.player1turn.ismyturn = false;
            turnController.player2turn.ismyturn = true;
            turnController.player1turn.playmade = false;
            Debug.Log("Le toca al jugador 2");
        }
        else if (turnController.player1turn.passed == true && turnController.player2turn.passed == true)
        {
            FinishRound();
        }
        else if (turnController.player1turn.passed == true && turnController.player1turn.playmade == false)
        {
            turnController.player1turn.ismyturn = false;
            turnController.player2turn.ismyturn = true;
            turnController.player1turn.playmade = false;
            Debug.Log("El jugador 1 ha pasado");
        }
        else if (turnController.player2turn.playmade == true)
        {
            turnController.player2turn.ismyturn = false;
            turnController.player1turn.ismyturn = true;
            turnController.player2turn.playmade = false;
            Debug.Log("Le toca al jugador 1");
        }
        else if (turnController.player2turn.passed == true && turnController.player2turn.playmade == false)
        {
            turnController.player2turn.ismyturn = false;
            turnController.player1turn.ismyturn = true;
            turnController.player2turn.playmade = false;
            Debug.Log("El jugador 2 ha pasado");
        }

    }

    //Posicionar las cartas en el campo
    public static void CardOnField(List<GameObject> meleecards, List<GameObject> rowposition, string tag, Collider2D other, float horizontalpos, float verticalpos)
    {
        int distance = 0;
        if (other.gameObject.CompareTag(tag))
        {
            rowposition.Add(other.gameObject);
            meleecards.Add(other.gameObject);
            for (int i = 0; i < rowposition.Count; i++)
            {
                rowposition[i].transform.position = new Vector3(horizontalpos + distance, verticalpos, 0f);
                distance += 85;
            }
        }
    }
    //Cambiar la jerarquia de las cartas que están en juego
    public static void ChangeParent(List<GameObject> mylist, GameObject gameObject)
    {
        for (int i = 0; i < mylist.Count; i++)
        {
            mylist[i].transform.SetParent(gameObject.transform);
        }
    }
    public static void FinishRound()
    {
        Turn player1turn = GameObject.Find("Player1Turn").GetComponent<Turn>();
        Turn player2turn = GameObject.Find("Player2Turn").GetComponent<Turn>();
        GameObject Player1powcounter = GameObject.Find("Player 1 Power Counter");
        GameObject Player2powcounter = GameObject.Find("Player 2 Power Counter");
        PowerCounter powercounter1 = Player1powcounter.GetComponent<PowerCounter>();
        PowerCounter powercounter2 = Player2powcounter.GetComponent<PowerCounter>();
        LifeCounter lifecounter1 =  GameObject.Find("Player 1 Life Counter").GetComponent<LifeCounter>();
        LifeCounter lifecounter2 =  GameObject.Find("Player 2 Life Counter").GetComponent<LifeCounter>();

        if (powercounter1.powfield < powercounter2.powfield)
        {
            CheckLife();
            Debug.Log("El jugador 2 ha gando la ronda");
            ClearField();
            lifecounter1.playerlife --;
            CheckLife();
            player2turn.ismyturn = true;
            player2turn.passed = false;
            player2turn.DrawExecuted = false;
            player1turn.ismyturn = false;
            player1turn.DrawExecuted = false;
            player1turn.passed = false;
            player1turn.playmade = false;
        }
        else if (powercounter1.powfield > powercounter2.powfield)
        {
            Debug.Log("El jugador 1 ha gando la ronda");
            ClearField();
            lifecounter2.playerlife--;
            CheckLife();
            player2turn.ismyturn = false;
            player2turn.passed = false;
            player2turn.DrawExecuted = false;
            player1turn.ismyturn = true;
            player1turn.DrawExecuted = false;
            player1turn.passed = false;
            player1turn.playmade = false;
        }
        else if (powercounter1.powfield == powercounter2.powfield)
        {
            Debug.Log("La ronda ha quedado empatada");
            ClearField();
            lifecounter1.playerlife--;
            lifecounter2.playerlife--;
            CheckLife();
            player2turn.ismyturn = true;
            player2turn.passed = false;
            player2turn.DrawExecuted = false;
            player1turn.ismyturn = false;
            player1turn.DrawExecuted = false;
            player1turn.passed = false;
            player1turn.playmade = false;
        }
    }
    //Cartas del campo al cementerio
    public static void ClearField()
    {
        GameObject Player1Field = GameObject.Find("Player1 Rows");
        Graveyard player1graveyard = GameObject.Find("Player1 Graveyard").GetComponent<Graveyard>();
        MeleeRow meleerow1 = Player1Field.GetComponentInChildren<MeleeRow>();
        RangedRow rangedrow1 = Player1Field.GetComponentInChildren<RangedRow>();
        SiegeRow siegerow1 = Player1Field.GetComponentInChildren<SiegeRow>();
        GameObject Player2Field = GameObject.Find("Player2 Rows");
        Graveyard player2graveyard = GameObject.Find("Player2 Graveyard").GetComponent<Graveyard>();
        MeleeRow meleerow2 = Player2Field.GetComponentInChildren<MeleeRow>();
        RangedRow rangedrow2 = Player2Field.GetComponentInChildren<RangedRow>();
        SiegeRow siegerow2 = Player2Field.GetComponentInChildren<SiegeRow>();
        WeatherRow weatherrow = GameObject.Find("WeatherRow").GetComponent<WeatherRow>();
        IncreaseBox meleeincrease = GameObject.Find("Player1 MeleeIncrease").GetComponent<IncreaseBox>();
        IncreaseBox rangedincrease = GameObject.Find("Player1 RangedIncrease").GetComponent<IncreaseBox>();
        IncreaseBox siegeincrease = GameObject.Find("Player1 SiegeIncrease").GetComponent<IncreaseBox>();
        IncreaseBox meleeincrease2 = GameObject.Find("Player2 MeleeIncrease").GetComponent<IncreaseBox>();
        IncreaseBox rangedincrease2 = GameObject.Find("Player2 RangedIncrease").GetComponent<IncreaseBox>();
        IncreaseBox siegeincrease2 = GameObject.Find("Player2 SiegeIncrease").GetComponent<IncreaseBox>();
        for (int i = 0; i < meleerow1.meleecards.Count; i++)
        {
            meleerow1.meleecards[i].transform.SetParent(player1graveyard.transform);
            meleerow1.meleecards[i].GetComponent<CardOutput>().isonthefield = false;
            player1graveyard.graveyard.Add(meleerow1.meleecards[i]);
        }
        meleerow1.meleecards.Clear();
        for (int i = 0; i < siegerow1.siegecards.Count; i++)
        {
            siegerow1.siegecards[i].transform.SetParent(player1graveyard.transform);
            siegerow1.siegecards[i].GetComponent<CardOutput>().isonthefield = false;
            player1graveyard.graveyard.Add(siegerow1.siegecards[i]);
        }
        siegerow1.siegecards.Clear();
        for (int i = 0; i < rangedrow1.rangedcards.Count; i++)
        {
            rangedrow1.rangedcards[i].transform.SetParent(player1graveyard.transform);
            rangedrow1.rangedcards[i].GetComponent<CardOutput>().isonthefield = false;
            player1graveyard.graveyard.Add(rangedrow1.rangedcards[i]);
        }
        rangedrow1.rangedcards.Clear();
        for (int i = 0; i < meleerow2.meleecards.Count; i++)
        {
            meleerow2.meleecards[i].transform.SetParent(player2graveyard.transform);
            meleerow2.meleecards[i].GetComponent<CardOutput>().isonthefield = false;
            player2graveyard.graveyard.Add(meleerow2.meleecards[i]);
        }
        meleerow2.meleecards.Clear();
        for (int i = 0; i < siegerow2.siegecards.Count; i++)
        {
            siegerow2.siegecards[i].transform.SetParent(player2graveyard.transform);
            siegerow2.siegecards[i].GetComponent<CardOutput>().isonthefield = false;
            player2graveyard.graveyard.Add(siegerow2.siegecards[i]);
        }
        siegerow2.siegecards.Clear();
        for (int i = 0; i < rangedrow2.rangedcards.Count; i++)
        {
            rangedrow2.rangedcards[i].transform.SetParent(player2graveyard.transform);
            rangedrow2.rangedcards[i].GetComponent<CardOutput>().isonthefield = false;
            player2graveyard.graveyard.Add(rangedrow2.rangedcards[i]);
        }
        rangedrow2.rangedcards.Clear();
        for (int i = 0; i < weatherrow.weathercards.Count; i++)
        {
            weatherrow.weathercards[i].transform.SetParent(player1graveyard.transform);
            weatherrow.weathercards[i].GetComponent<OtherCardOutput>().isonthefield = false;
            player1graveyard.graveyard.Add(weatherrow.weathercards[i]);
        }
        weatherrow.weathercards.Clear();
         
         if(meleeincrease.increasebox.Count == 1)
         {
         meleeincrease.increasebox[0].transform.SetParent(player1graveyard.transform);
         player1graveyard.graveyard.Add(meleeincrease.increasebox[0]);
         meleeincrease.increasebox.RemoveAt(0);
         }
         if(rangedincrease.increasebox.Count == 1)
         {
         rangedincrease.increasebox[0].transform.SetParent(player1graveyard.transform);
         player1graveyard.graveyard.Add(rangedincrease.increasebox[0]);
         rangedincrease.increasebox.RemoveAt(0);
         }
         if(siegeincrease.increasebox.Count == 1)
         {
         siegeincrease.increasebox[0].transform.SetParent(player1graveyard.transform);
         player1graveyard.graveyard.Add(siegeincrease.increasebox[0]);
         siegeincrease.increasebox.RemoveAt(0);
         }
         if(meleeincrease2.increasebox.Count == 1)
         {
         meleeincrease2.increasebox[0].transform.SetParent(player2graveyard.transform);
         player2graveyard.graveyard.Add(meleeincrease2.increasebox[0]);
         meleeincrease2.increasebox.RemoveAt(0);
         }
         if(rangedincrease2.increasebox.Count == 1)
         {
         rangedincrease2.increasebox[0].transform.SetParent(player2graveyard.transform);
         player2graveyard.graveyard.Add(rangedincrease2.increasebox[0]);
         rangedincrease2.increasebox.RemoveAt(0);
         }
         if(siegeincrease2.increasebox.Count == 1)
         {
         siegeincrease2.increasebox[0].transform.SetParent(player2graveyard.transform);
         player2graveyard.graveyard.Add(siegeincrease2.increasebox[0]);
         siegeincrease2.increasebox.RemoveAt(0);
         }



    }
    //Posicionar las cartas del cementerio
    public static void GraveyardPosition(RectTransform cardposition, List<GameObject> graveyard)
    {
        for (int i = 0; i < graveyard.Count; i++)
        {
            graveyard[i].transform.position = cardposition.position;
            graveyard[i].transform.SetParent(graveyard[i].transform);
        }
    }
    //Si la carta esta en el cementerio reiniciar su estado
    public static void CheckCardsOnGraveyard()
    {
        Graveyard player1graveyard = GameObject.Find("Player1 Graveyard").GetComponent<Graveyard>();
        Graveyard player2graveyard = GameObject.Find("Player2 Graveyard").GetComponent<Graveyard>();
        for (int i = 0; i < player1graveyard.graveyard.Count; i++)
        {
            if (player1graveyard.graveyard[i].GetComponent<CardOutput>() != null)
            {
                player1graveyard.graveyard[i].GetComponent<CardOutput>().affectedbyweather = false;
                player1graveyard.graveyard[i].GetComponent<CardOutput>().affectedbyeffect = false;
                player1graveyard.graveyard[i].GetComponent<CardOutput>().isonthefield = false;
            }
            else if (player1graveyard.graveyard[i].GetComponent<OtherCardOutput>() != null)
            {
                player1graveyard.graveyard[i].GetComponent<OtherCardOutput>().isonthefield = false;
            }
        }
        for (int i = 0; i < player2graveyard.graveyard.Count; i++)
        {
            if (player2graveyard.graveyard[i].GetComponent<CardOutput>() != null)
            {
                player2graveyard.graveyard[i].GetComponent<CardOutput>().affectedbyweather = false;
                player2graveyard.graveyard[i].GetComponent<CardOutput>().affectedbyeffect = false;
                player2graveyard.graveyard[i].GetComponent<CardOutput>().isonthefield = false;
            }
            else if (player2graveyard.graveyard[i].GetComponent<OtherCardOutput>() != null)
            {
                player2graveyard.graveyard[i].GetComponent<OtherCardOutput>().isonthefield = false;
            }
        }
    }
    //Chequear los LifePoints del jugador
    public static void CheckLife()
    {
        LifeCounter lifecounter1 = GameObject.Find("Player 1 Life Counter").GetComponent<LifeCounter>();
        LifeCounter lifecounter2 = GameObject.Find("Player 2 Life Counter").GetComponent<LifeCounter>();
        if (lifecounter1.playerlife == 0)
        {
            Debug.Log("El jugador 2 ha ganado la partida");
            SceneManager.LoadScene("Jugador2 Ha Ganado");
            return;
        }
        else if (lifecounter2.playerlife == 0)
        {
            Debug.Log("El jugador 1 ha ganado la partida");
            SceneManager.LoadScene("Jugador1 Ha Ganado");
            return;
        }
        else if (lifecounter1.playerlife == 0 && lifecounter2.playerlife == 0)
        {
            Debug.Log("La partida ha quedado empatada");
            SceneManager.LoadScene("Empate");
            return;
        }
    }
    //Asegurar que la mano siempre tenga 10 cartas
    public static void CheckHandCount(List<GameObject> hand, Graveyard graveyard)
    {
        if (hand.Count > 10)
        {
            while (hand.Count > 10)
            {
                graveyard.graveyard.Add(hand[hand.Count - 1]);
                hand.RemoveAt(hand.Count - 1);
            }
        }
    }
    //Posicionar las cartas de la casilla de Aumento

    public static void IncreaseOnField(GameObject Increase, List<GameObject>box , string tag, Collider2D other)
    {
       if(box.Count < 1){
       if (other.gameObject.CompareTag(tag))
       {
       RectTransform boxposition = Increase.GetComponent<RectTransform>();
       box.Add(other.gameObject);
       box[0].transform.position = boxposition.position;
       box[0].transform.SetParent(Increase.transform);
       }
       }
    }
    //Limpiar las posiciones de la fila
    public static void ClearPositions(List<GameObject>cards , List<GameObject>rowposition)
    {
         if(cards.Count == 0)
      {
         rowposition.Clear();
      }
    }
}
