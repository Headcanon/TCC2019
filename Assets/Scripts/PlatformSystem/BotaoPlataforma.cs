using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoPlataforma : MonoBehaviour {

    public GameObject plataforma;
    public Vector3 distancia;
    public float velocidade;
    public bool negativo;
    public bool travado;
    public bool apertado;
    public Animator anim;

    private GameObject alavanca, alavancaTravada;

    private ActivePlatform plataformaAtiva;
    private bool travaExtra = true;

    // Use this for initialization
    void Start ()
    {
        alavanca = transform.GetChild(0).gameObject;
        alavancaTravada = transform.GetChild(1).gameObject;

        plataformaAtiva = plataforma.GetComponent<ActivePlatform>();

        if(travado)
        {
            alavanca.SetActive(false);
            alavancaTravada.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!travado && !apertado)
        {
            alavanca.SetActive(true);
            alavancaTravada.SetActive(false);
        }

        if (!travado && apertado && !travaExtra)
        {
            if (!plataformaAtiva.Foi && !negativo)
            {
                plataformaAtiva.Vai(distancia, velocidade);
            }
            else if (plataformaAtiva.Foi && negativo)
            {
                plataformaAtiva.Volta(distancia, velocidade);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!travado && Input.GetButton("FaceX"))
        {
            travaExtra = false;
            apertado = true;
            anim.SetTrigger("Ativar");
        }
    }
}
