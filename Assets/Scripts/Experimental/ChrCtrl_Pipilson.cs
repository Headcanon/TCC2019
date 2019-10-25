using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChrCtrl_Pipilson : MonoBehaviour
{
    #region Movimento basico
    [Header("Movimento basico")]
    // Componente que controla o movimento da Ash
    private CharacterController characterController;  

    // Vetor de movimento da Ashley
    public Vector3 moveDirection = Vector3.zero;

    // Modelo da Ashley
    private GameObject ashModel;
    // Animato pra controlar as animações
    private Animator anim;

    // Bool que define se player controla o movimento da Ash
    public bool sobControle = true;
    #endregion
    
    #region Pulo
    [Header("Pulo")]
    // Força da gravidade que puxa a Ash pra baixo
    public float gravity = 20.0f;

    // Tempo que pode segurar o botão de pulo
    public float jumpTime = 1.0f;
    // Contador do tempo decorrido segurando o botão de pulo
    private float jumpTimeCounter;

    // Velocidade vertical do pulo
    public float jumpSpeed = 15f;

    // Quantidade de pulos dados
    public int pulosDados;
    // Limite de pulos
    public int puloLimite = 1;

    // Bool que indica se a personagem está no chão
    public bool noChao;
    #endregion

    #region Aceleracao
    [Header("Aceleracao")]
    // Velocidade padrão
    public float speed = 6.0f;
    // Velocidade acelerada
    public float highSpeed = 9.0f;

    // Tempo que até começar a acelerar
    public float aceleTime = 1.5f;
    // Contador do tempo decorrido antes de acelerar
    private float aceleTimeCounter;

    // Bool que define se o movimento já está acelerado
    private bool fast;
    #endregion

    void Start()
    {        
        characterController = GetComponent<CharacterController>();
        ashModel = transform.GetChild(0).GetChild(0).gameObject;
        anim = ashModel.GetComponent<Animator>();
    }    

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

                #region Pulo
                // Zera os pulos dados
                pulosDados = 0;

                // Põe a gravidade no padrão
                gravity = 20f;

                // Zera o timer de segurar o botão de pulo
                jumpTimeCounter = jumpTime;

                // Ativa a animação de pulo
                if (Input.GetButton("Jump") && jumpTimeCounter > 0)
                {
                    anim.SetTrigger("Jump");
                }
                #endregion

                #region Aceleracao
                // Se o botão está sendo apertado e já deu o tempo do timer...
                if (Input.GetButton("Horizontal") && aceleTimeCounter > 0)
                {
                    // Deixa rápido
                    fast = true;
                }
                // Caso contrário, se mesmo assim tiver apertando o botão...
                else if (Input.GetButton("Horizontal"))
                {
                    // Aumenta o tempo
                    aceleTimeCounter -= Time.deltaTime;
                }
                // Caso nenhum dos anteriores seja verdade...
                else
                {
                    // Deixa normal
                    fast = false;
                    // Zera o timer
                    aceleTimeCounter = aceleTime;
                }
                #endregion

            }
            // Se ela está no ar...
            else
            {
                // Altera a movimentação lateral sem alterar o eixo Y
                moveDirection = new Vector3(transform.right.x * Input.GetAxis("Horizontal"), moveDirection.y, 0);                
            }

            #region Pulo
            // Enquanto o botão de pulo for apertado e ainda não tiver dado o tempo...
            if (Input.GetButton("Jump") && jumpTimeCounter > 0)
            {
                // Adiciona movimento vertical ao vetor de movimento
                moveDirection.y = jumpSpeed;

                // Roda o timer
                jumpTimeCounter -= Time.deltaTime;

                // Coloca os pulos dados como 1
                pulosDados = 1;
            }

            // Lança um raycast pra baixo
            RaycastHit hit;
            if (Physics.Raycast(transform.position - (transform.forward * 0.1f) + transform.up * 0.3f, Vector3.down, out hit, 1000))
            {
                // Retorna a distância entre a Ash e o chão
                anim.SetFloat("JumpHeight", hit.distance);
            }
            #endregion

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

            // Rotaciona o modelo da Ash de acordo com o botão apertado
            ashModel.transform.rotation = Quaternion.Euler(0, 180 - 90 * horizontal, 0);

            // Retorna o aperto do botão de movimento pro animator
            anim.SetFloat("Vel", Mathf.Abs(horizontal));
            
        }

        // Move the controller, mesmo se player não estiver sob controle
        if (characterController.enabled)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }

        
    }
}