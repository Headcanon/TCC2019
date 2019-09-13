using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabJoint : MonoBehaviour
{
    ChrCtrl_Pipilson ash;

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ash = other.GetComponent<ChrCtrl_Pipilson>();

        if (ash !=null)
        {
            ash.moveDirection = Vector3.zero;
            ash.sobControle = false;
            ash.transform.parent = transform;
        }
    }
}
