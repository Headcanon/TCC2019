using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : MonoBehaviour
{
    public void QuitToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuInicial");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("POC");
    }
}
