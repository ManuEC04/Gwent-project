using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    public void OptionMenuBack()
    {
        Debug.Log("Funciona el boton");
        SceneManager.LoadScene("Menu");
        
    }
}
