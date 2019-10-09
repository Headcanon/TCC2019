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
    // Rigidbody pro Hinge funcionar e poder adicionar forças
    Rigidbody rb;
    // Hinge joint pra conectar nas coisas
    DistanceJoint3D hj;
    #endregion

    public GameObject ganchoPrefab;
    //public GameObject seta;
    private GameObject ganchoAtual;
    private bool ganchoAtivo = false;
    Quaternion rotacao = Quaternion.identity;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        ganchoPrefab = Resources.Load<GameObject>("CordaLonga");
    }

    // Update is called once per frame
    void Update()
    {
        // Os códigos aqui cuida pra que a Ash se conecte a alguma coisa
        // É usado tanto para a corda quanto para o gancho
        // Só pra garantir que ainda não tem nenhum Joint nem RigidBody...
        if (jointado && rb == null && hj == null)
        {
            // Desliga o character controller
            cc.enabled = false;
            // Adiciona o DistanceJoint3D
            rb = gameObject.AddComponent<Rigidbody>();
            hj = gameObject.AddComponent<DistanceJoint3D>();
            // Conecta o Hinjejoint ao Rigidbody identificado
            hj.penduradoEm = conectadoEm.transform;
        }
        else if (!jointado && rb != null && hj != null)
        {
            // Destrói os dois componentes
            Destroy(hj);
            Destroy(rb);

            // Reativa o Character Controller
            cc.enabled = true;
        }


        //if (!ganchoAtivo && Input.GetButton("Mira"))
        //{
        //    rotacao = AnguloAxis();
        //    seta.gameObject.SetActive(true);
        //    seta.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        //    seta.transform.rotation = rotacao;
        //}
        //else
        //{
        //    seta.gameObject.SetActive(false);
        //}

        if(!ganchoAtivo &&  ganchoAtual == null && Input.GetButtonDown("Fire1"))
        {
            ganchoAtual = Instantiate(ganchoPrefab, transform.position, rotacao, transform);
            ganchoAtivo = true;
        }
        else if(ganchoAtivo && ganchoAtual != null && Input.GetButtonDown("Jump"))
        {
            ganchoAtivo = false;
            jointado = false;
            Destroy(ganchoAtual);
        }
    }
    
    float angulo = 0;
    private Quaternion AnguloAxis()
    {
        angulo -= Input.GetAxis("Horizontal");
        Quaternion rotacao = Quaternion.Euler(0, 0, angulo);

        return rotacao;
    }
}
