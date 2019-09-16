using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabJoint_Deactivate : MonoBehaviour
{
    Rigidbody rb;

    ChrCtrl_Pipilson ash;
    CharacterController cc;

    public Transform pai;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Se tiver uma Ash e um Gancho...
        if (ash != null)
        {
            // Adiciona força de acordo com o eixo horizontal
            float horizontal = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector3(horizontal * 2, 0, 0));

            float vertical = Input.GetAxis("Vertical") * Time.fixedDeltaTime;
            ash.transform.position = Vector3.MoveTowards(ash.transform.position, pai.position, vertical);
        }
    }

    // Quando a Ashley entra no trigger...
    private void OnTriggerEnter(Collider other)
    {
        // Pega o controlador e o Gancho
        ash = other.GetComponent<ChrCtrl_Pipilson>();
        cc = other.GetComponent<CharacterController>();

        // Se nenhum dos dois for nulo...
        if (ash !=null)
        {
            // Reseta o momento dela
            ash.moveDirection = Vector3.zero;
            // Tira o controle do Player
            ash.sobControle = false;
            cc.enabled = false;
            cc.transform.parent = transform;
        }
    }
}
