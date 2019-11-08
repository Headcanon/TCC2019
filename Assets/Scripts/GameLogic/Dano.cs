using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script colocado em todos os itens que dão dano quando entram em contato com player*/
public class Dano : MonoBehaviour
{
    // Quantidade de dano tirado da vida
    public float dano;
    // Script da vida
    private Vida move;

    // Quando um objeto entrar no trigger...
    private void OnTriggerEnter(Collider other)
    {
        // Tenta pegar o script de vida desse objeto
        move = other.GetComponent<Vida>();
        // Se esse não for nulo significa que é o player
        if(move != null)
        {
            // Aciona o método de dano na vida com o dano indicado
            move.Damage(dano);
        }
    }
}
