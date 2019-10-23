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
    public float jumpSpeed = 10f;
    bool isJumping;

    public int pulosDados; //Pipilson
    public int puloLimite = 1; //Mudei pra 1, já que é assim que ela deve começar. Precisa ser 2 só quando ela está com o jetpack -Pipilson

    public bool noChao;
    #endregion

    #region Aceleracao
    [Header("Aceleracao")]
    public float speed = 6.0f;
    public float highSpeed = 9.0f;

    float aceleTimer = 0.0f;
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

        // Se estiver sob controle...
        if (sobControle)
        {
            // Pega o botão de movimento
            float horizontal = Input.GetAxis("Horizontal");

            // Se Ash está no chão...
            if (noChao)
            {
                //  Cria a direção de movimento
                moveDirection = transform.right * horizontal;

                // Zera os pulos dados
                pulosDados = 0;

                // Põe a gravidade no padrão
                gravity = 20f;

                jumpTimeCounter = jumpTime;

                if (Input.GetButton("Jump") && jumpTimeCounter > 0)
                {
                    anim.SetTrigger("Jump");
                }

                #region Aceleracao
                    // Se o botão está sendo apertado e já deu o tempo do timer...
                    if (Input.GetButton("Horizontal") && aceleTimer >= 1.5f)
                {
                    // Deixa rápido
                    fast = true;
                }
                // Caso contrário, se mesmo assim tiver apertando o botão...
                else if (Input.GetButton("Horizontal"))
                {
                    // Aumenta o tempo
                    aceleTimer += Time.deltaTime;
                }
                // Caso nenhum dos anteriores seja verdade...
                else
                {
                    // Deixa normal
                    fast = false;
                    // Zera o timer
                    aceleTimer = 0.0f;
                }
                #endregion

            }
            // Se ela está no ar...
            else
            {
                // Altera a movimentação lateral sem alterar o eixo Y
                moveDirection = new Vector3(transform.right.x * Input.GetAxis("Horizontal"), moveDirection.y, 0);                
            }

            // Enquanto o botão de pulo for apertado e ainda não tiver dado o tempo...
            if (Input.GetButton("Jump") && jumpTimeCounter > 0)
            {
                // Adiciona movimento vertical ao vetor de movimento
                moveDirection.y = jumpSpeed;

                jumpTimeCounter -= Time.deltaTime;

                pulosDados = 1;
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position - (transform.forward * 0.1f) + transform.up * 0.3f, Vector3.down, out hit, 1000))
            {
                anim.SetFloat("JumpHeight", hit.distance);
            }

            #region Aceleracao
            if (fast)
            {
                anim.SetBool("Fast", true);
                moveDirection.x *= highSpeed;                
            }
            else
            {
                anim.SetBool("Fast", false);
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

        // Move the controller
        if (characterController.enabled)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }

        
    }
}