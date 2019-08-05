using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{
    public int pontos = 5;
    Pontuacao gm;

    void Start()
    {
        gm = FindObjectOfType<Pontuacao>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SetPontuacao(pontos);
            Destroy(gameObject);
        }
    }

    void SetPontuacao(int pts)
    {
        gm.AlteraPontos(pts);
    }
}
