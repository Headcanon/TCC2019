using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour {

    ChrCtrl chrctrl;

    private void OnTriggerEnter(Collider other)
    {
        chrctrl = other.GetComponent<ChrCtrl>();
        if(chrctrl != null)
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
