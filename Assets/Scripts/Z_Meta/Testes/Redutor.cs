using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redutor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Pontuacao pontuacao = GameObject.FindObjectOfType<Pontuacao>();
        Debug.Log(pontuacao.name);
        pontuacao.Reduzir(3);
    }
}
