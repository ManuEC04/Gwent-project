using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public bool FirstDrawExecuted = false;
    public bool RedrawExecuted = false;
    public bool DrawExecuted = false;
    public bool StartRoundDraw = false;
    public bool ismyturn = false;
    public bool passed = false;
    public bool playmade = false;
    public void Pass()
    {
      if(FirstDrawExecuted && DrawExecuted)
      {
        passed = true;
        ismyturn = false;
        GameFunctions.CheckTurn();
      }
    }

}
