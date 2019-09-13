using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabJoint : MonoBehaviour
{
    ChrCtrl_Pipilson ash;
    AshleyController ac;

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ash = other.GetComponent<ChrCtrl_Pipilson>();
        ac = other.GetComponent<AshleyController>();

        if (ash !=null && ac != null)
        {
            ash.moveDirection = Vector3.zero;
            ash.sobControle = false;
            ac.conectadoEm = GetComponent<Rigidbody>();
            ac.joint = true;
            ash.transform.parent = transform;
        }
    }
}
