using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Todos os métodos dos menus do jogo*/
public class Menus : MonoBehaviour
{
    // Cria um novo save
    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lago");
    }

    // Volta para o menu inicial
    public void QuitToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuInicial");
    }

    // Sai do jogo
    public void QuitGame()
    {
        Application.Quit();
    }

    // Reinicia a fase
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lago");
    }
}