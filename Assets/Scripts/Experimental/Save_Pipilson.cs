using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Pipilson : MonoBehaviour
{
    public GameObject player;
    public CharacterController charCtrl;
    GameObject[] parede;

    void Awake()
    {
        parede = GameObject.FindGameObjectsWithTag("Parade");
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("pos"))
        {
            charCtrl.enabled = false;
            player.transform.position = PlayerPrefsX.GetVector3("pos");
            charCtrl.enabled = true;

            for (int i = 0; i < parede.Length; i++)
            {
                parede[i].transform.position = PlayerPrefsX.GetVector3("posParede");
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) //Save
        {
            PlayerPrefsX.SetVector3("pos", player.transform.position);
            print("Save");
        }

        if (Input.GetKeyDown(KeyCode.X)) //Load
        {
            charCtrl.enabled = false;
            player.transform.position = PlayerPrefsX.GetVector3("pos");
            charCtrl.enabled = true;
            print("Load");
        }

        if (Input.GetKeyDown(KeyCode.P)) //Delete
        {
            PlayerPrefs.DeleteAll();
            print("Delete");
        }
    }

    public void CheckPoint()
    {
        PlayerPrefsX.SetVector3("pos", player.transform.position);

        for (int i = 0; i < parede.Length; i++)
        {
            PlayerPrefsX.SetVector3("posParede", parede[i].transform.position);
        }

        print("Save");
    }
}