using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanchoDeEscalada : MonoBehaviour
{
    #region Projeção do Raycast
    // Alcance da corda
    public float alcance = 10f;
    #endregion

    #region Conexão
    // Se Ash está conectada a algo usando o Hinge Joint
    public bool jointado;
    // Rigidbody no qual irá se conectar
    public Rigidbody conectadoEm;
    #endregion

    #region Coisas pra ativar e desativar
    // Character controller pra ser ativado e/ou desativado
    CharacterController cc;
    // Rigidbody pro Hinge funcionar e poder adicionar forças
    Rigidbody rb;
    // Hinge joint pra conectar nas coisas
    DistanceJoint3D hj;
    #endregion

    // Update is called once per frame
    void Update()
    {
        // Cada frame ele tenta pegar esses compnentes
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        hj = GetComponent<DistanceJoint3D>();

        // Se apertar a tecla Q...
        if (Input.GetKey(KeyCode.Q))
        {
            // Posição em que a corda é mirada
            Vector3 targetPos;
            //targetPos = Input.mousePosition;


            //Pega a posição do mouse relativa ao ponto mais próximo da câmera
            Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
            //Pega a posição do mouse relativa ao ponto mais distante da câmera
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
            targetPos = Camera.main.ScreenToWorldPoint(mousePosFar);
            targetPos.z = 0;

            // Desenha um raio da Ash até a posição da mira
            Debug.DrawLine(transform.position, targetPos, Color.green);
            RaycastHit hit;
            Physics.Raycast(transform.position, targetPos, out hit, alcance);

            // Se o raio encostou em alguma coisa com um RigidBody...
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody>() != null)
            {
                // E com o raio acertando, se você atirar...
                if (Input.GetButton("Fire1"))
                {
                    // Conecta no Rigidbody acertado
                    conectadoEm = hit.rigidbody;

                    // Ativa o modo jointado
                    jointado = true;
                }
                else
                {
                    // Desativa a conexão
                    conectadoEm = null;

                    // Desativa o modo jointado
                    jointado = false;
                }
            }
        }

        // Os códigos aqui cuida pra que a Ash se conecte a alguma coisa
        // É usado tanto para a corda quanto para o gancho
        #region Conexão
        // Só pra garantir que ainda não tem nenhum HingeJoint nem RigidBody...
        if (jointado && rb == null && hj == null)
        {
            // Desliga o character controller
            cc.enabled = false;
            // Adiciona o DistanceJoint3D
            hj = gameObject.AddComponent<DistanceJoint3D>();
            rb = gameObject.AddComponent<Rigidbody>();
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
        #endregion
    }

}
