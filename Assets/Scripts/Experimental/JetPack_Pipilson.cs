using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack_Pipilson : MonoBehaviour
{
    ChrCtrl_Pipilson ashCtrl;
    public float combustivel = 1.5f;

    void Start()
    {
        ashCtrl = GetComponent<ChrCtrl_Pipilson>();
        //combustivel = 1.5f;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && (ashCtrl.pulosDados == ashCtrl.puloLimite))
        {
            ashCtrl.moveDirection = Vector3.zero;
            ashCtrl.gravity = 0.8f;
            combustivel--;
        }

        if (combustivel <= 0f)
        {
            ashCtrl.gravity = 20f;
        }             

        if (Input.GetButtonUp("Jump") && (ashCtrl.pulosDados == ashCtrl.puloLimite))
        {
            ashCtrl.gravity = 20f;
        }
    }
}