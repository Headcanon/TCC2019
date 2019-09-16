using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabJoint : MonoBehaviour
{
    public Rigidbody rb;
    public ListaJoints paiDeTodos;

    ChrCtrl_Pipilson ash;
    CharacterController cc;    

    // Quando a Ashley entra no trigger...
    private void OnTriggerEnter(Collider other)
    {
        // Pega o controlador e o Gancho
        ash = other.GetComponent<ChrCtrl_Pipilson>();
        cc = other.GetComponent<CharacterController>();

        // Se nenhum dos dois for nulo...
        if (ash !=null && cc != null)
        {
            // Reseta o momento dela
            ash.moveDirection = Vector3.zero;
            // Tira o controle do Player
            ash.sobControle = false;
            // Desativa o CharacterController
            cc.enabled = false;

            // Avisa o paiDeTodos pra funcionar
            paiDeTodos.naAtiva = true;
        }
    }

    public void Abortar()
    {
        // Se nenhum dos dois for nulo...
        if (ash != null && cc != null)
        {
            // Reseta o momento dela
            ash.moveDirection = Vector3.zero;
            // Tira o controle do Player
            ash.sobControle = true;
            // Desativa o CharacterController
            cc.enabled = true;

            // Avisa o paiDeTodos pra funcionar
            paiDeTodos.naAtiva = false;

            ash = null;
            cc = null;
        }
    }
}
