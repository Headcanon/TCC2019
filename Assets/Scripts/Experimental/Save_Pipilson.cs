using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save_Pipilson : MonoBehaviour
{
    private GameObject player;
    private CharacterController charCtrl;
    private GameObject[] paredes;

    private int contagemDeLevels = 0;

    void Awake()
    {        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // Começa e imediatamente joga pra última cena
        // Caso não tenha nenhuma cena salva o default é a cena 1 (Laboratorio)
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastSceneIndex", 1));
    }

    void Update()
    {
        #region Debug
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
        #endregion
    }

    public void CheckPoint()
    {
        // Salva posição de player
        PlayerPrefsX.SetVector3("pos", player.transform.position);

        // Salva o indice da última cena acessada
        PlayerPrefs.SetInt("lastSceneIndex", SceneManager.GetActiveScene().buildIndex);

        // Pra cada objeto da lista...
        for (int i = 0; i < paredes.Length; i++)
        {
            PlayerPrefsX.SetVector3("posParede_" + SceneManager.GetActiveScene().buildIndex + "_" + i, paredes[i].transform.position); //Salva posição dos objetos
        }

        print("Save");
    }

    private void OnLevelWasLoaded(int level)
    {
        // Mais um level carregado nesse playthrough
        contagemDeLevels++;

        // Encontra player e seu character controller
        player = GameObject.FindGameObjectWithTag("Player");
        charCtrl = player.GetComponent<CharacterController>();

        // Acha todas as paredes com a tag
        paredes = GameObject.FindGameObjectsWithTag("Parede");


        // Se já existe um save da posição de player e ainda não carregou dois levels nesse playthrough...
        if (PlayerPrefs.HasKey("pos") && contagemDeLevels < 2)
        {
            charCtrl.enabled = false;
            player.transform.position = PlayerPrefsX.GetVector3("pos"); // Carrega a posição de player
            charCtrl.enabled = true;
        }

        // Carrega tudo o q está salvo
        CarregarObjetos();
    }

    private void CarregarObjetos()
    {
        // Se já existe um save da posição, carregar
        if (PlayerPrefs.HasKey("posParede"))
        {
            // Pra cada objeto da lista...
            for (int i = 0; i < paredes.Length; i++)
            {
                paredes[i].transform.position = PlayerPrefsX.GetVector3("posParede_" + SceneManager.GetActiveScene().buildIndex + "_" + i); // Carrega a posição de objetos
            }
        }
    }

    public void Resetar()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
}