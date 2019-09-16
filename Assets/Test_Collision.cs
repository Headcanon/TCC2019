using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " Colidiu com " + gameObject.name);

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + " Saiu de " + gameObject.name);
    }
}
