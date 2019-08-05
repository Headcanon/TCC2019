using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Quando o player passa por esse GameObject ele se torno seu novo checkpoint */
public class NewCheckpoint : MonoBehaviour
{
    Player pl;

    private void OnTriggerEnter(Collider other)
    {
        pl = other.GetComponent<Player>();
        if(pl != null)
        {
            pl.spawnPoint = gameObject.transform;
        }
    }
}
