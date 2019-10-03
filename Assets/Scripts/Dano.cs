using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script colocado em todos os itens que dão dano quando entram em contato com player*/
public class Dano : MonoBehaviour
{
    Vida move;
    public float dano;

    private void OnTriggerEnter(Collider other)
    {
        move = other.GetComponent<Vida>();
        if(move != null)
        {
            move.Damage(dano);
        }
    }
}