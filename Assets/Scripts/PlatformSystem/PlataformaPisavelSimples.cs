using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Inspirado no sistema de plataformas do LookAway, qlq coisa olha lá como ele fez*/
public class PlataformaPisavelSimples : MonoBehaviour
{
    // Se a plataforma se move em um eixo específico
    public bool goX, goY, goZ;
    // Se a plataforma se move at all
    public bool goAtAll;

    // X, Y e Z do movimento desejado
    private float x, y, z;

    // Distancia percorrida e velocidade do percurso
    public float distance;
    public float velocity;

    // FixedUpdate é o que permite que o Character Controller tenha tempo de reconhecer o movimento da plataforma
    void FixedUpdate()
    {        
        if (goX)
        {
            // Magia da matemática que calcula a posição X
            x = Mathf.Sin(Time.time * velocity) * distance;
        }
        if (goY)
        {
            // Magia da matemática que calcula a posição Y
            y = Mathf.Sin(Time.time * velocity) * distance;
        }
        if (goZ)
        {
            // Magia da matemática que calcula a posição Z
            z = Mathf.Sin(Time.time * velocity) * distance;
        }


        if (goAtAll)
        {
            // Move a plataforma
            transform.Translate(new Vector3(x, y, z));
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.transform.parent = null;
        }
    }
}