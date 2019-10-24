using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanchoDeEscalada : MonoBehaviour
{
    #region Conexão
    // Se Ash está conectada a algo usando o Hinge Joint
    public bool jointado;
    // Rigidbody no qual irá se conectar
    public Transform conectadoEm;
    #endregion

    #region Coisas pra ativar e desativar
    // Character controller pra ser ativado e/ou desativado
    CharacterController cc;
    // Controlador só pra zerar o vetor de movimento
    ChrCtrl_Pipilson chcrt;
    // Rigidbody pro Hinge funcionar e poder adicionar forças
    Rigidbody rb;
    // Hinge joint pra conectar nas coisas
    DistanceJoint3D dj3D;
    #endregion

    public GameObject ganchoPrefab;
    //public GameObject seta;
    private GameObject ganchoAtual;
    private bool ganchoAtivo = false;
    private Quaternion rotacao = Quaternion.identity;
    private Animator anim;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        chcrt = GetComponent<ChrCtrl_Pipilson>();
        ganchoPrefab = Resources.Load<GameObject>("CordaLonga");
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Os códigos aqui cuida pra que a Ash se conecte a alguma coisa

        // Só pra garantir que ainda não tem nenhum Joint nem RigidBody...
        if (jointado && rb == null && dj3D == null)
        {
            anim.SetBool("Enganchado", true);
            // Desliga o character controller
            cc.enabled = false;
            // Adiciona o DistanceJoint3D
            rb = gameObject.AddComponent<Rigidbody>();
            dj3D = gameObject.AddComponent<DistanceJoint3D>();
            // Conecta o Hinjejoint ao Rigidbody identificado
            dj3D.penduradoEm = conectadoEm.transform;
        }
        else if (!jointado && rb != null && dj3D != null)
        {
            // Destrói os dois componentes
            Destroy(dj3D);
            Destroy(rb);

            // Reativa o Character Controller
            cc.enabled = true;
        }

        if(!ganchoAtivo &&  ganchoAtual == null && Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("LancaGancho");
            ganchoAtual = Instantiate(ganchoPrefab, transform.position, rotacao, transform);
            ganchoAtivo = true;
        }
        else if(ganchoAtivo && ganchoAtual != null && Input.GetButtonDown("Jump"))
        {            
            Abortar();
        }
    }
    
    public void Abortar()
    {
        anim.SetBool("Enganchado", false);
        // Reseta o momento dela
        chcrt.moveDirection = Vector3.zero;
        ganchoAtivo = false;
        jointado = false;
        Destroy(ganchoAtual);
    }

    float angulo = 0;
    private Quaternion AnguloAxis()
    {
        angulo -= Input.GetAxis("Horizontal");
        Quaternion rotacao = Quaternion.Euler(0, 0, angulo);

        return rotacao;
    }
}
