using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Quando o player passa por esse GameObject ele se torno seu novo checkpoint */
public class NewCheckpoint : MonoBehaviour
{
    Vida pl;

    private void OnTriggerEnter(Collider other)
    {
        pl = other.GetComponent<Vida>();
        if(pl != null)
        {
            pl.spawnPoint = gameObject.transform;
        }
    }
}