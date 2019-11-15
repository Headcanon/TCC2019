using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public GameObject menuScreen, ui;

    [FMODUnity.EventRef]
    public string clickSound;

    private ChrCtrl player;
    private bool activeMenu;
    private int contagemDeLevels = 0;

    private void OnLevelWasLoaded(int level)
    {
        // Mais um level carregado nesse playthrough
        contagemDeLevels++;

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

        activeMenu = true;        
    }

    public void PlayCamera()
    {
        player.moveDirection = Vector3.zero;
        player.gravidadeSecundaria = false;
        player.sobControle = true;

        activeMenu = false;        
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Click()
    {
        // Som de clique
        FMODUnity.RuntimeManager.PlayOneShot(clickSound);
    }
}
