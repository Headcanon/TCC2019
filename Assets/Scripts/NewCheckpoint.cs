using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Quando o player passa por esse GameObject ele se torno seu novo checkpoint */
public class NewCheckpoint : MonoBehaviour
{
    Vida vida;
    Save_Pipilson save;

    private void OnTriggerEnter(Collider other)
    {
        vida = other.GetComponent<Vida>();

        if (vida != null)
        {
            //vida.spawnPoint = gameObject.transform;
            save.CheckPoint();
            print("oi");
        }
    }
}