using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Options ()
    {
        SceneManager.LoadScene("Options");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
