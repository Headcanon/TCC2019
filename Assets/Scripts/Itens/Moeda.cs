using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{
    public int pontos = 5;
    public Vector3 rotacao;

    private Pontuacao gm;

    void Start()
    {
        gm = FindObjectOfType<Pontuacao>();
    }

    private void Update()
    {
        transform.Rotate(rotacao);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SetPontuacao(pontos);
            gameObject.SetActive(false);
        }
    }

    void SetPontuacao(int pts)
    {
        gm.AlteraPontos(pts);
    }
}
