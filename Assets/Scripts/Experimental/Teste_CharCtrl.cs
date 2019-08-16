using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste_ChangeAxis : MonoBehaviour
{
    public Transform[] todosOsPontos;
    List<Transform> pontosAtivos;

    #region CharCtrl
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();


        pontosAtivos.Add(todosOsPontos[0]);
        pontosAtivos.Add(todosOsPontos[1]);
        pontosAtivos.Add(todosOsPontos[2]);
    }
    #endregion

    void Update()
    {
        #region CharCtrl
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = transform.right * Input.GetAxis("Horizontal");

            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        #endregion


        if (Vector3.Distance(pontosAtivos[0].position, transform.position) < Vector3.Distance(pontosAtivos[1].position, transform.position))
        {
            transform.parent = pontosAtivos[0];
        }
        else if (Vector3.Distance(pontosAtivos[2].position, transform.position) < Vector3.Distance(pontosAtivos[1].position, transform.position))
        {
            transform.parent = pontosAtivos[2];
        }




        for (int i =0; i< pontosAtivos.Count; i++)
        {

        }
    }
}
