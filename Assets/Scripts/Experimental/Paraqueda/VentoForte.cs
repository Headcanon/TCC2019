using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentoForte : MonoBehaviour
{
    public Vector3 ventoDirecao;
    Vector3 oldDrag;
    private void OnTriggerEnter(Collider other)
    {
        Paraqueda pqd = other.GetComponent<Paraqueda>();

        if (pqd != null)
        {
            oldDrag = pqd.drag;
            pqd.newMoveDirection = Vector3.zero;
            pqd.drag = ventoDirecao;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Paraqueda pqd = other.GetComponent<Paraqueda>();

        if (pqd != null)
        {
            pqd.drag = oldDrag;
        }
    }
}