using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    Player eixos;
    followCamera cm;
    public Vector3 novoOffset;
    public Vector3 novoLookAtOffset;

    private void OnTriggerEnter(Collider target)
    {
        eixos = target.GetComponent<Player>();
        cm = Camera.main.GetComponent<followCamera>();       
        
        cm.newOffset = novoOffset;
        cm.newLookAtOffset = novoLookAtOffset;
    }
}
