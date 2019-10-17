using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Pipilson : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        if (PlayerPrefs.HasKey("posX"))
        {
            player.transform.position = new Vector3(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"), PlayerPrefs.GetFloat("posZ"));
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z)) //Save
        {
            PlayerPrefs.SetFloat("posX", player.transform.position.x);
            PlayerPrefs.SetFloat("posY", player.transform.position.y);
            PlayerPrefs.SetFloat("posZ", player.transform.position.z);
            print("Save");
        }

        if (Input.GetKey(KeyCode.X)) //Load
        {
            player.transform.position = new Vector3(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"), PlayerPrefs.GetFloat("posZ"));
            print("Load");
        }

        if (Input.GetKey(KeyCode.P)) //Delete
        {
            PlayerPrefs.DeleteAll();
            print("Delete");
        }
    }
}