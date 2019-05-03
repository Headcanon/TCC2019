using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInicial : MonoBehaviour
{

    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("POC");
    }
}
