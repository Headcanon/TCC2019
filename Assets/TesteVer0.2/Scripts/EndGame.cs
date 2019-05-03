using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    //public void PlayerDeath()
    //{
    //    if (Destroy(other.gameObject))
    //    {
    //        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    //    }
    //}

    private void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}