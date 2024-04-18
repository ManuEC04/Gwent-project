using System.Collections.Generic;
using UnityEngine;
public class GameFunctions : MonoBehaviour
{
    //Robar 10 Cartas al inicio de la primera ronda
    public static void FirstDraw(List<GameObject> deck, List<GameObject> hand, float horizontalpos, float verticalpos, float distance)
    {

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

        if (powercounter1.powfield < powercounter2.powfield)
        {
            CheckLife();
            Debug.Log("El jugador 2 ha gando la ronda");
            ClearField();
            LifeCounter.player1life--;
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
            LifeCounter.player2life--;
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
            LifeCounter.player1life--;
            LifeCounter.player2life--;
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

        for (int i = 0; i < meleerow1.meleecards.Count; i++)
        {
            meleerow1.meleecards[i].transform.SetParent(player1graveyard.transform);
            player1graveyard.graveyard.Add(meleerow1.meleecards[i]);
        }
        meleerow1.meleecards.Clear();
        for (int i = 0; i < siegerow1.siegecards.Count; i++)
        {
            siegerow1.siegecards[i].transform.SetParent(player1graveyard.transform);
            player1graveyard.graveyard.Add(siegerow1.siegecards[i]);
        }
        siegerow1.siegecards.Clear();
        for (int i = 0; i < rangedrow1.rangedcards.Count; i++)
        {
            rangedrow1.rangedcards[i].transform.SetParent(player1graveyard.transform);
            player1graveyard.graveyard.Add(rangedrow1.rangedcards[i]);
        }
        rangedrow1.rangedcards.Clear();
        for (int i = 0; i < meleerow2.meleecards.Count; i++)
        {
            meleerow2.meleecards[i].transform.SetParent(player2graveyard.transform);
            player2graveyard.graveyard.Add(meleerow2.meleecards[i]);
        }
        meleerow2.meleecards.Clear();
        for (int i = 0; i < siegerow2.siegecards.Count; i++)
        {
            siegerow2.siegecards[i].transform.SetParent(player2graveyard.transform);
            player2graveyard.graveyard.Add(siegerow2.siegecards[i]);
        }
        siegerow2.siegecards.Clear();
        for (int i = 0; i < rangedrow2.rangedcards.Count; i++)
        {
            rangedrow2.rangedcards[i].transform.SetParent(player2graveyard.transform);
            player2graveyard.graveyard.Add(rangedrow2.rangedcards[i]);
        }
        rangedrow2.rangedcards.Clear();
    }
    //Chequear los LifePoints del jugador
    public static void CheckLife()
    {
        if (LifeCounter.player1life == 0)
        {
            Debug.Log("El jugador 2 ha ganado la partida");
            return;
        }
        else if (LifeCounter.player2life == 0)
        {
            Debug.Log("El jugador 1 ha ganado la partida");
            return;
        }
        else if (LifeCounter.player1life == 0 && LifeCounter.player2life == 0)
            Debug.Log("La partida ha quedado empatada");
            return;
    }
}
