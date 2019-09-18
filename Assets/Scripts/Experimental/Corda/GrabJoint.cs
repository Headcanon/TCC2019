using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabJoint : MonoBehaviour
{
    public Rigidbody rb;
    public ListaJoints paiDeTodos;  

    // Quando a Ashley entra no trigger...
    private void OnTriggerEnter(Collider other)
    {
        // Se nenhum dos dois for nulo...
        if (other.CompareTag("Player") && paiDeTodos.podePegar)
        {
            // Reseta o momento dela
            paiDeTodos.ashCtrl.moveDirection = Vector3.zero;
            // Tira o controle do Player
            paiDeTodos.ashCtrl.sobControle = false;
            // Desativa o CharacterController
            paiDeTodos.ashCC.enabled = false;

            //ash.transform.position = transform.position;

            // Avisa o paiDeTodos pra funcionar
            paiDeTodos.naAtiva = true;
            paiDeTodos.timeLeft = 7;

            Physics.IgnoreLayerCollision(0, 10, true);
        }
    }
    /*
     * O que é que eu estou tentando fazer
     * Era alguma coisa com a corda
     * Tinha a ver com o teletransporte
     * Eu tenho que resolver duas coisas diferente
     * Uma é os colliders que etão pequenos demais
     * Outra é o pulo que precisa pra soltar
     * Acho que eu vou resolver primeiro o pulo
     * Eu só preciso botar um timer em algum lugar
     */

    //public void Abortar()
    //{
    //    // Reseta o momento dela
    //    paiDeTodos.ashCtrl.moveDirection = Vector3.zero;
    //    // Devolvo o controle ao Player
    //    paiDeTodos.ashCtrl.sobControle = true;
    //    // Reativa o CharacterController
    //    paiDeTodos.ashCC.enabled = true;

    //    // Avisa o paiDeTodos pra parar de funcionar
    //    paiDeTodos.naAtiva = false;

    //    paiDeTodos.podePegar = false;
    //    paiDeTodos.timeLeft = 10;
    //}
}
