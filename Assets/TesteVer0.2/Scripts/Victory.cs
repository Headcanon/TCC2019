using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public GameObject completeLevelUI;

    void OnTriggerEnter(Collider other)
    {
        completeLevelUI.SetActive(true);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameWon");
        Debug.Log("You Win!");
    }
}
