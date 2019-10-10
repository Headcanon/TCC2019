using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChrCtrl_Pipilson : MonoBehaviour
{
    CharacterController characterController;  
    
    #region Pulo
    [Header("Pulo")]
    public float gravity = 20.0f;

    public float jumpTime = 1.0f;
    float jumpTimeCounter;
    public float jumpSpeed = 10.0f;
    public float jumpHighSpeed = 15f;
    bool isJumping;

    public int pulosDados; //Pipilson
    public int puloLimite = 1; //Mudei pra 1, já que é assim que ela deve começar. Precisa ser 2 só quando ela está com o jetpack -Pipilson

    public bool noChao;
    #endregion

    #region Aceleracao
    [Header("Aceleracao")]
    public float speed = 6.0f;
    public float highSpeed = 9.0f;

    float timer = 0.0f;
    bool fast;
    #endregion

    public Vector3 moveDirection = Vector3.zero; //Deixei public -Pipilson

    public GameObject ashModel;
    Animator anim;

    public bool sobControle = true;

    void Start()
    {        
        characterController = GetComponent<CharacterController>();

        anim = ashModel.GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        

        // A bool noChao é igual ao idGrounded do CharacterController
        // Isso é só pra que eu possa acessar o noChao em outros scripts
        noChao = characterController.isGrounded;

        if (sobControle)
        {
            float horizontal = Input.GetAxis("Horizontal");

            // Se Ash está no chão...
            if (noChao)
            {
                // We are grounded, so recalculate
                // move direction directly from axes
                moveDirection = transform.right * horizontal;
                pulosDados = 0;
                gravity = 20f;

                #region Aceleracao
                if (Input.GetButton("Horizontal") && timer >= 1.5f)
                {
                    fast = true;
                }
                else if (Input.GetButton("Horizontal"))
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    fast = false;
                    timer = 0.0f;
                }
                #endregion
                anim.SetBool("Pulano", false);
            }
            else
            {
                moveDirection = new Vector3(transform.right.x * Input.GetAxis("Horizontal"), moveDirection.y, 0);
                
            }

            // Se ainda não for atingido o limite de pulos permitir pular denovo
            if (Input.GetButtonDown("Jump")) //Alterei esse if -Pipilson
            {
                anim.SetBool("Pulano", true);
                if (pulosDados < puloLimite)
                {
                    moveDirection.y = jumpHighSpeed;
                    pulosDados++;
                }
            }

            #region Pulo Adaptável
            if (Input.GetButton("Jump"))
            {
                if (isJumping && pulosDados < 1)
                {
                    moveDirection.y = jumpHighSpeed;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
            #endregion

            #region Aceleracao
            if (fast)
            {
                moveDirection.x *= highSpeed;
            }
            else
            {
                moveDirection.x *= speed;
            }
            #endregion

            // Corrige o eixo Z
            if (transform.position.z != 0)
            {
                moveDirection.z = (0 - transform.position.z) * 0.5f;
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            ashModel.transform.rotation = Quaternion.Euler(0, 180 - 90 * horizontal, 0);
            if (anim != null)
            {
                anim.SetFloat("Vel", Mathf.Abs(horizontal));
            }
        }

        //// Pra travar o movimento enquanto tá mirando
        //if (Input.GetButton("Mira"))
        //{
        //    sobControle = false;
        //}
        //else
        //{
        //    sobControle = true;
        //}

        // Move the controller
        if (characterController.enabled)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }

        
    }
}