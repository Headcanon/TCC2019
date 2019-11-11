using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    followCamera cm;
    public GameObject menuScreen, ui;
    bool activeMenu;
    private ChrCtrl player;
    public Vector3 pauseOffset, pauseLookatOffset;
    public Vector3 playOffset, playLookatOffset;

    private int contagemDeLevels = 0;

    private void OnLevelWasLoaded(int level)
    {
        // Mais um level carregado nesse playthrough
        contagemDeLevels++;

        cm = Camera.main.GetComponent<followCamera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ChrCtrl>();

        // Se ainda não carregou dois levels nesse playthrough...
        if (contagemDeLevels < 2)
        {
            PauseCamera();
        }

    }           
    
    private void Update()
    {
        if(Input.GetButtonDown("Start") && !activeMenu)
        {
            menuScreen.gameObject.SetActive(true);
            ui.gameObject.SetActive(false);
            PauseCamera();
        }
        else if (Input.GetButtonDown("Start") && activeMenu)
        {
            menuScreen.gameObject.SetActive(false);
            ui.gameObject.SetActive(true);
            PlayCamera();
        }
    }

    public void PauseCamera()
    {
        player.moveDirection = Vector3.zero;
        player.gravidadeSecundaria = true;
        player.sobControle = false;
        cm.TrocaOffset(pauseOffset, pauseLookatOffset);
        //cm.newOffset = new Vector3(0, 2, -4);
        //cm.newLookAtOffset = new Vector3(1, 3, 0);
        activeMenu = true;        
    }

    public void PlayCamera()
    {
        player.moveDirection = Vector3.zero;
        player.gravidadeSecundaria = false;
        player.sobControle = true;
        cm.TrocaOffset(playOffset, playLookatOffset);
        //cm.newOffset = new Vector3(5, 3, -20);
        //cm.newLookAtOffset = new Vector3(5, -3, 0);
        activeMenu = false;        
    }

    public void Sair()
    {
        Application.Quit();
    }
}
