using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabJoint : MonoBehaviour
{
    public Rigidbody rb;
    public ListaJoints paiDeTodos;
    private ListaGrabJoints listaGrabJoints;

    [FMODUnity.EventRef]
    public string grabSound;

    private Animator anim;

    // Quando a Ashley entra no trigger...
    private void OnTriggerEnter(Collider other)
    {
        // Se nenhum dos dois for nulo...
        if (other.CompareTag("Player") && paiDeTodos.podePegar)
        {
            // Pega o animator
            anim = other.transform.GetChild(0).GetComponent<Animator>();
            // Ativa o ciclo de animações de corda
            anim.SetTrigger("EntraCorda");

            // Ativa o som de enganchar
            FMODUnity.RuntimeManager.PlayOneShot(grabSound);
            paiDeTodos.range.start();


            // Reseta o momento dela
            paiDeTodos.ashCtrl.moveDirection = Vector3.zero;
            // Tira o controle do Player
            paiDeTodos.ashCtrl.sobControle = false;
            // Desativa o CharacterController
            paiDeTodos.ashCC.enabled = false;

            listaGrabJoints = paiDeTodos.GetComponent<ListaGrabJoints>();
            other.transform.position = listaGrabJoints.JointMaisProximo(other.transform).transform.position;

            // Avisa o paiDeTodos pra funcionar
            paiDeTodos.naAtiva = true;
            paiDeTodos.timeLeft = 7;

        }
    }
}
