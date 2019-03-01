using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano : MonoBehaviour
{
    Movimento move;

    private void OnTriggerEnter(Collider other)
    {
        move = other.GetComponent<Movimento>();
        if(move != null)
        {
            move.Respawn();
        }
    }
}
