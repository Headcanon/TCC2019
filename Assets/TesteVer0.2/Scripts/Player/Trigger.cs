using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    Player eixos;
    followCamera camera;
    public Vector3 novoOffset;
    public Vector3 novoLookAtOffset;

    private void OnTriggerEnter(Collider target)
    {
        eixos = target.GetComponent<Player>();
        camera = Camera.main.GetComponent<followCamera>();       
        
        camera.newOffset = novoOffset;
        camera.newLookAtOffset = novoLookAtOffset;
    }
}
