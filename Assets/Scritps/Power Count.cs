using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PowerCounter : MonoBehaviour
{
    public MeleeRow meleerow;
    public RangedRow rangedrow;
    public SiegeRow siegerow;
    public Text power;
    public List<CardOutput> cardsinfo = new List<CardOutput>();
    public int powfield;

    void Start()
    {
        meleerow = GetComponentInChildren<MeleeRow>();
        rangedrow = GetComponentInChildren<RangedRow>();
        siegerow = GetComponentInChildren<SiegeRow>();
        power = GetComponentInChildren<Text>();
    }
    void Update()
    {
        power.text = powfield.ToString();
        GetPower();
    }
    public void GetPower()
    {
        int powmelee = 0;
        int powranged = 0;
        int powsiege = 0;
        for (int i = 0; i < meleerow.meleecards.Count; i++)
        {
            powmelee += meleerow.meleecards[i].GetComponent<CardOutput>().powercard;
        }
        for (int i = 0; i < rangedrow.rangedcards.Count; i++)
        {
            powranged += rangedrow.rangedcards[i].GetComponent<CardOutput>().powercard;
        }
        for (int i = 0; i < siegerow.siegecards.Count; i++)
        {
            powsiege += siegerow.siegecards[i].GetComponent<CardOutput>().powercard;
        }
        powfield = powmelee + powranged + powsiege;
    }
}
