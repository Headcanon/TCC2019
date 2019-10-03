using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Muda o angulo de camera quando player passa por esse trigger */
public class Trigger : MonoBehaviour
{
    followCamera cm;
    public Vector3 novoOffset;
    public Vector3 novoLookAtOffset;

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Player"))
        {
            cm = Camera.main.GetComponent<followCamera>();

            cm.TrocaOffset(novoOffset, novoLookAtOffset);
            //cm.newOffset = novoOffset;
            //cm.newLookAtOffset = novoLookAtOffset;
        }
    }
}