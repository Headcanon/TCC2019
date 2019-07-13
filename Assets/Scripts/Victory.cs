using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public GameObject completeLevelUI;

    void OnTriggerEnter(Collider other)
    {
        completeLevelUI.SetActive(true);
        Debug.Log("You Win!");
    }
}
