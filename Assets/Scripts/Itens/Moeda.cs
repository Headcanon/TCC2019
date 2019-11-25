using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{
    public int pontos = 5;
    public Vector3 rotacao;
    private Pontuacao gm;
    private Transform modelo;
    float time, y; //P

    void Start()
    {
        y = transform.position.y; //P
        gm = FindObjectOfType<Pontuacao>();
        modelo = transform.GetChild(0);
    }

    private void Update()
    {
        modelo.Rotate(rotacao);
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        // Magia da matemática que calcula a posição Y
        y = Mathf.Sin(time * 0.5f) * 0.004f;

        transform.Translate(new Vector3(0, y, 0));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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
