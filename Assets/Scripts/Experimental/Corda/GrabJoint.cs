using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabJoint : MonoBehaviour
{
    Rigidbody rb;

    ChrCtrl_Pipilson ash;
    GanchoDeEscalada ge;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Se tiver uma Ash e um Gancho...
        if (ash != null && ge != null)
        {
            // Adiciona força de acordo com o eixo horizontal
            float horizontal = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector3(horizontal * 2, 0, 0));

            float vertical = Input.GetAxis("Vertical");
            ash.moveDirection = new Vector3(0, transform.up.y * vertical, 0);
        }
    }

    // Quando a Ashley entra no trigger...
    private void OnTriggerEnter(Collider other)
    {
        // Pega o controlador e o Gancho
        ash = other.GetComponent<ChrCtrl_Pipilson>();
        ge = other.GetComponent<GanchoDeEscalada>();

        // Se nenhum dos dois for nulo...
        if (ash !=null && ge != null)
        {
            // Reseta o momento dela
            ash.moveDirection = Vector3.zero;
            // Tira o controle do Player
            ash.sobControle = false;
            // Conecta o Gancho nesse Rigidbody
            ge.conectadoEm = rb;
            // Avisa o Gancho que ele está jointado
            // O que faz ele criar o Hinjejoint no lugar correspondente
            ge.jointado = true;            
        }
    }
}
