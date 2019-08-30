﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    followCamera cm;
    public GameObject menuScreen, ui;
    bool activeMenu;
    public ChrCtrl player;

    private void Awake()
    {
        cm = Camera.main.GetComponent<followCamera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ChrCtrl>();
        PauseCamera();

        menuScreen = GameObject.Find("Menu");

        //ui = GameObject.Find("UI");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !activeMenu)
        {
            menuScreen.gameObject.SetActive(true);
            ui.gameObject.SetActive(false);
            PauseCamera();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && activeMenu)
        {
            menuScreen.gameObject.SetActive(false);
            ui.gameObject.SetActive(true);
            PlayCamera();
        }
    }

    public void PauseCamera()
    {
        player.enableMove = false;
        cm.TrocaOffset(new Vector3(0, 2, -4), new Vector3(1, 3, 0));
        //cm.newOffset = new Vector3(0, 2, -4);
        //cm.newLookAtOffset = new Vector3(1, 3, 0);
        activeMenu = true;        
    }

    public void PlayCamera()
    {
        player.enableMove = true;
        cm.TrocaOffset(new Vector3(5, 3, -20), new Vector3(5, -3, 0));
        //cm.newOffset = new Vector3(5, 3, -20);
        //cm.newLookAtOffset = new Vector3(5, -3, 0);
        activeMenu = false;        
    }
}
