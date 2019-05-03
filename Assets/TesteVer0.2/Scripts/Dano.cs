using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano : MonoBehaviour
{
    public float dano;

    Player move;

    private void OnTriggerEnter(Collider other)
    {
        move = other.GetComponent<Player>();
        if(move != null)
        {
            move.Damage(dano);
        }
    }
}
