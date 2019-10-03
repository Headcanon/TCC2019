using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curvador : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Camera.main.transform.parent = other.transform;
        other.transform.Rotate(new Vector3(0, 50, 0));
    }
}