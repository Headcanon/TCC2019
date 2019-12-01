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
    private CharacterController cc;
    // Controlador só pra zerar o vetor de movimento
    private ChrCtrl ashCtrl;
    // Rigidbody pro Hinge funcionar e poder adicionar forças
    private Rigidbody rb;
    // Hinge joint pra conectar nas coisas
    private DistanceJoint3D dj3D;
    #endregion

    public GameObject ganchoPrefab;

    //[FMODUnity.EventRef]
    //public string hookSound;

    //public GameObject seta;
    private GameObject ganchoAtual;
    private bool ganchoAtivo = false;
    private Quaternion rotacao = Quaternion.identity;
    private Animator anim;
    private Transform mao;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        ashCtrl = GetComponent<ChrCtrl>();
        ganchoPrefab = Resources.Load<GameObject>("Gancho_Prefab");
        anim = transform.GetChild(0).GetComponent<Animator>();
        mao = GameObject.Find("SpawnGancho").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Os códigos aqui cuida pra que a Ash se conecte a alguma coisa

        // Só pra garantir que ainda não tem nenhum Joint nem RigidBody...
        if (jointado && rb == null && dj3D == null)
        {
            Debug.Log("deu if");
            
            // Ativa a animação de gancho
            anim.SetBool("Enganchado", true);

            // Tira o controle de player
            ashCtrl.sobControle = false;
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

        if (!ganchoAtivo && ganchoAtual == null && Input.GetButtonDown("FaceY"))
        {
            // Ativa o som do gancho
            FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/launching_hook");

            anim.SetTrigger("LancaGancho");
            ganchoAtual = Instantiate(ganchoPrefab, mao.position, rotacao, mao);
            ganchoAtivo = true;
        }
        else if (ganchoAtivo && ganchoAtual != null && Input.GetButtonDown("FaceA"))
        {
            Abortar();
        }
    }

    public void Abortar()
    {
        Invoke("RecobrarColisao", 5);
        // Desativa animação de gancho
        anim.SetBool("Enganchado", false);

        // Retorna o controle de player
        ashCtrl.sobControle = true;
        // Reseta o momento dela
        ashCtrl.moveDirection = Vector3.zero;
        if (jointado)
        {
            // Bota um impulso de pulo reduzido nela
            ashCtrl.moveDirection.y = ashCtrl.jumpSpeed / 1.5f;
        }

        // Desativa o gancho
        ganchoAtivo = false;
        jointado = false;
        Destroy(ganchoAtual);
    }

    private void RecobrarColisao()
    {
        Physics.IgnoreLayerCollision(0, 2, false);
    }

    float angulo = 0;
    private Quaternion AnguloAxis()
    {
        angulo -= Input.GetAxis("LeftHorizontal");
        Quaternion rotacao = Quaternion.Euler(0, 0, angulo);

        return rotacao;
    }
}
