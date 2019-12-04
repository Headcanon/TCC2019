using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public GameObject menuScreen, ui;

    [FMODUnity.EventRef]
    public string clickSound;

    private ChrCtrl player;
    private Animator cameraAnim;
    private bool activeMenu;
    private int contagemDeLevels = 0;

    private void OnLevelWasLoaded(int level)
    {
        // Mais um level carregado nesse playthrough
        contagemDeLevels++;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ChrCtrl>();
        cameraAnim = GameObject.Find("CM vcam1").GetComponent<Animator>();

        // Se ainda não carregou dois levels nesse playthrough...
        if (contagemDeLevels == 1)
        {
            PauseCamera();
        }

    }           
    
    private void Update()
    {
        if(Input.GetButtonDown("Start") && !activeMenu)
        {           
            PauseCamera();
        }
        else if (Input.GetButtonDown("Start") && activeMenu)
        {
            PlayCamera();
        }
    }

    public void PauseCamera()
    {
        menuScreen.gameObject.SetActive(true);
        ui.gameObject.SetActive(false);

        player.moveDirection = Vector3.zero;
        player.gravidadeSecundaria = true;
        player.sobControle = false;

        activeMenu = true;        
    }

    public void PlayCamera()
    {

        menuScreen.gameObject.SetActive(false);
        ui.gameObject.SetActive(true);

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
