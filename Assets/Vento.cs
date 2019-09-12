using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vento : MonoBehaviour
{
    public Vector3 ventoDirecao;
    private void OnTriggerEnter(Collider other)
    {
        ChrCtrl_Pipilson chrctrl = other.GetComponent<ChrCtrl_Pipilson>();

        if (chrctrl != null)
        {
            chrctrl.sobControle = false;
            chrctrl.moveDirection = Vector3.zero;
            chrctrl.moveDirection = ventoDirecao;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ChrCtrl_Pipilson chrctrl = other.GetComponent<ChrCtrl_Pipilson>();

        if (chrctrl != null)
        {
            chrctrl.sobControle = true;

        }
    }
}
