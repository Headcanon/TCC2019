using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack_Pipilson : MonoBehaviour
{
    ChrCtrl_Pipilson ashCtrl;
    public float combustivel = 5f;

    void Start()
    {
        ashCtrl = GetComponent<ChrCtrl_Pipilson>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && (ashCtrl.pulosDados == ashCtrl.puloLimite) && (combustivel > 0.0f))
        {
            ashCtrl.moveDirection = Vector3.zero;
            ashCtrl.gravity = 0.8f;
            combustivel--;
        }

        if (combustivel <= 0.0f)
        {
            ashCtrl.gravity = 20f;
            combustivel = 0.0f;
            ashCtrl.puloLimite = 1;
        }
        else
        {
            ashCtrl.puloLimite = 2;
        }

        if (Input.GetButtonUp("Jump") && (ashCtrl.pulosDados == ashCtrl.puloLimite))
        {
            ashCtrl.gravity = 20f;
        }
    }
}