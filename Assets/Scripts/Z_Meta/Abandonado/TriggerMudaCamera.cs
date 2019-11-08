using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Muda o angulo de camera quando player passa por esse trigger */
public class TriggerMudaCamera : MonoBehaviour
{
    public Animator cm;
    public string camera;

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Player"))
        {
            cm.SetTrigger(camera);
        }
    }
}
