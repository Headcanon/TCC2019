using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save_Pipilson : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string saveSound;

    private GameObject player;
    private CharacterController charCtrl;
    private Vida vida;
    private GameObject[] paredes;
    private GameObject[] items;
    private BotaoPlataforma[] alavancas;
    private Pontuacao pontos;

    private int contagemDeLevels;

    void Awake()
    {        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        pontos = GetComponent<Pontuacao>();
        pontos.pontos = PlayerPrefs.GetInt("pecasTotais", 0);

        // Começa e imediatamente joga pra última cena
        // Caso não tenha nenhuma cena salva o default é a cena 1 (Laboratorio)
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastSceneIndex", 1));
    }

    private void OnLevelWasLoaded(int level)
    {

        // Encontra player e seu character controller
        player = GameObject.FindGameObjectWithTag("Player");
        charCtrl = player.GetComponent<CharacterController>();
        vida = player.GetComponent<Vida>();

        // Mais um level carregado nesse playthrough
        contagemDeLevels++;
        Debug.Log("contou mais um - " + contagemDeLevels);

        // Acha todas as paredes com a tag
        paredes = GameObject.FindGameObjectsWithTag("Parede");
        // Acha todos os itens com a tag
        items = GameObject.FindGameObjectsWithTag("Coletavel");
        // Acha todas as alvancas pelo tipo
        alavancas = FindObjectsOfType<BotaoPlataforma>();

        // Se já existe um save da posição de player e ainda não carregou dois levels nesse playthrough...
        if (PlayerPrefs.HasKey("pos") && contagemDeLevels < 2)
        {
            Debug.Log("carregou player - " + contagemDeLevels);
            charCtrl.enabled = false;
            player.transform.position = PlayerPrefsX.GetVector3("pos"); // Carrega a posição de player
            charCtrl.enabled = true;
        }

        // Pega a vida salva
        vida.vida = PlayerPrefs.GetFloat("vida", 100);

        // Carrega tudo o q está salvo
        CarregarObjetos();
    }

    void Update()
    {
        #region Debug
        if (Input.GetKey(KeyCode.G) && Input.GetKeyDown(KeyCode.Z)) //Save
        {
            PlayerPrefsX.SetVector3("pos", player.transform.position);
            print("Save");
        }

        if (Input.GetKey(KeyCode.G) && Input.GetKeyDown(KeyCode.X)) //Load
        {
            charCtrl.enabled = false;
            player.transform.position = PlayerPrefsX.GetVector3("pos");
            charCtrl.enabled = true;
            print("Load");
        }

        if (Input.GetKey(KeyCode.G) && Input.GetKeyDown(KeyCode.P)) //Delete
        {
            PlayerPrefs.DeleteAll();
            print("Delete");
        }

        if (Input.GetKey(KeyCode.G) && Input.GetKeyDown(KeyCode.R)) //Reset
        {
            Resetar();
            print("Reset");
        }
        #endregion
    }

    public void CheckPoint(Vector3 pos)
    {
        // Som de save
        FMODUnity.RuntimeManager.PlayOneShot(saveSound);

        // Salva posição de player
        PlayerPrefsX.SetVector3("pos", pos);

        // Salva o indice da última cena acessada
        PlayerPrefs.SetInt("lastSceneIndex", SceneManager.GetActiveScene().buildIndex);

        // Salva os pontos totais no momento do checkpoint
        PlayerPrefs.SetInt("pecasTotais", pontos.pontos);

        // Salva a vida total no momento do checkpoint
        PlayerPrefs.SetFloat("vida", vida.vida);

        #region Paredes
        // Pra cada objeto da lista...
        for (int i = 0; i < paredes.Length; i++)
        {
            PlayerPrefsX.SetVector3("posParede_" + SceneManager.GetActiveScene().buildIndex + "_" + i, paredes[i].transform.position); //Salva posição dos objetos
        }
        #endregion

        #region Itens
        for (int i = 0; i < items.Length; i++)
        {
            string key = "item_" + SceneManager.GetActiveScene().buildIndex + "_" + i;

            PlayerPrefsX.SetBool(key, items[i].activeSelf); // Carrega a atividade de objetos
        }
        #endregion

        #region Botões
        for (int i = 0; i < alavancas.Length; i++)
        {
            string key = "alavancaAperto_" + SceneManager.GetActiveScene().buildIndex + "_" + i;
            PlayerPrefsX.SetBool(key, alavancas[i].apertado); // Carrega o aperto de botões

            string key2 = "alavancaTrava_" + SceneManager.GetActiveScene().buildIndex + "_" + i;
            PlayerPrefsX.SetBool(key2, alavancas[i].travado); // Carrega a trava de botões
        }
        #endregion

        print("Save");
    }
    

    private void CarregarObjetos()
    {
        #region Paredes
        // Pra cada objeto da lista...
        for (int i = 0; i < paredes.Length; i++)
        {
            string key = "posParede_" + SceneManager.GetActiveScene().buildIndex + "_" + i;

            // Se já existe um save da posição, carregar
            if (PlayerPrefs.HasKey(key))
            {
                paredes[i].transform.position = PlayerPrefsX.GetVector3(key); // Carrega a posição de objetos
            }
        }
        #endregion

        #region Itens
        // Pra cada objeto da lista...
        for (int i = 0; i < items.Length; i++)
        {
            string key = "item_" + SceneManager.GetActiveScene().buildIndex + "_" + i;

            // Se já existe um save da posição, carregar
            if (PlayerPrefs.HasKey(key))
            {
                items[i].SetActive(PlayerPrefsX.GetBool(key)); // Carrega a atividade de objetos
            }
        }
        #endregion

        #region Botões
        for (int i = 0; i < alavancas.Length; i++)
        {
            string key = "alavancaAperto_" + SceneManager.GetActiveScene().buildIndex + "_" + i;
            if (PlayerPrefs.HasKey(key))
            {
                alavancas[i].apertado = PlayerPrefsX.GetBool(key); // Carrega o aperto de botões
                Debug.Log("Pegou aperto");
            }

            string key2 = "alavancaTrava_" + SceneManager.GetActiveScene().buildIndex + "_" + i;
            if (PlayerPrefs.HasKey(key2))
            {
                alavancas[i].travado = PlayerPrefsX.GetBool(key2); // Carrega a trava de botões
            }
        }
        #endregion

    }

    public void Resetar()
    {
        PlayerPrefs.DeleteAll();
        
        SceneManager.LoadScene(0);

        Destroy(gameObject);
    }
}