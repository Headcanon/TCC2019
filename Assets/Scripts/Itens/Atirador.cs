using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirador : MonoBehaviour
{
    // Prefab do projétil que será lançado
    public GameObject municao;


    /* Esse script é uma junção dos scripts Atirador e AtiProjetil do commit número 15 (Coins - L)
    Não foi testado ainda. Se houver algum erro que não puder resolver volte ao commit 15 e recupere esses scripts */

    #region Lógica
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Define os parametros para o projétil seguir
            Rigidbody rb = municao.GetComponent<Rigidbody>();
            InimigoBoss target = FindObjectOfType<InimigoBoss>();
            Vector3 moveDirection = (target.transform.position - transform.position).normalized * 7f;

            // Instancia o prefab do projétil na cena
            Instantiate(municao, transform.position, Quaternion.identity);

            // Define a velocidade do projetil
            rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

            // Manda o projetil ignorar colisão consigo mesmo e com o Atirador para não haver autodestruição
            Physics.IgnoreCollision(municao.GetComponent<Collider>(), GetComponent<Collider>());

            Destroy(gameObject, 6f);
        }
    }
    #endregion
}
