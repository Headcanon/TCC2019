using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Pipilson : MonoBehaviour
{
    public GameObject player;
    public CharacterController charCtrl;
    public Vida hp;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("pos"))
        {
            charCtrl.enabled = false;
            player.transform.position = PlayerPrefsX.GetVector3("pos");
            charCtrl.enabled = true;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z)) //Save
        {
            PlayerPrefsX.SetVector3("pos", player.transform.position);
            print("Save");
        }

        if (Input.GetKey(KeyCode.X)) //Load
        {
            charCtrl.enabled = false;
            player.transform.position = PlayerPrefsX.GetVector3("pos");
            charCtrl.enabled = true;
            print("Load");
        }

        if (Input.GetKey(KeyCode.P)) //Delete
        {
            PlayerPrefs.DeleteAll();
            print("Delete");
        }
    }

    public void CheckPoint()
    {
        //save posicao
    }
}