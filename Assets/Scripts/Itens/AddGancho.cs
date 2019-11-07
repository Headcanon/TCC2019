using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGancho : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.AddComponent<GanchoDeEscalada>();
            Destroy(gameObject);
        }
    }
}
