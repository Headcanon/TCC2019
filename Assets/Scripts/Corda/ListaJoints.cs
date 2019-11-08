using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaJoints : MonoBehaviour
{
    #region Parametros
    public float forcaBalango = 2;

    // Lista de todos os joints na corda
    private List<Joint> listaJoints;
    // Transform de player
    private GameObject ash;
    public ChrCtrl ashCtrl;
    public CharacterController ashCC;

    // bool que define se a corda está ativada
    public bool naAtiva = false;

    // Velocidade de escalada da corda
    public float velocidade;

    private Animator anim;
    #endregion

    [System.Serializable]
    private struct Joint
    {
        public Transform t_Joint;
        public GrabJoint gj_Joint;

        public Joint(Transform t, GrabJoint gj)
        {
            t_Joint = t;
            gj_Joint = gj;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Cria a lista
        listaJoints = new List<Joint>();

        // Acha a coisas da Ashley
        ash = GameObject.FindGameObjectWithTag("Player");
        ashCtrl = ash.GetComponent<ChrCtrl>();
        ashCC = ash.GetComponent<CharacterController>();
        anim = ash.transform.GetChild(0).GetComponent<Animator>();
        // Pega o filho desse objeto
        Transform child = transform.GetChild(0);

        // Enquanto o filho não for nulo, ou seja, child tiver outro child...
        for (int i = 0;  child.childCount == 1; i ++)
        {
            // Adiciona o componente GrabJoint a ele
            GrabJoint gj = child.gameObject.AddComponent<GrabJoint>();
            // Define o parametro rb como o Rigidbody dele mesmo
            gj.rb = child.gameObject.GetComponent<Rigidbody>();
            gj.rb.centerOfMass = new Vector3(0, 0, 0);
            gj.rb.inertiaTensor = new Vector3(1, 1, 1);

            // Define o parametro paiDeTodos como essa lista
            gj.paiDeTodos = this;

            // Adiciona ele à lista
            listaJoints.Add( new Joint(child, gj));
            // E pega o child dele
            child = child.GetChild(0);

            // O loop só vai parar quando child não tiver outro child
            // E vai pegando os filhos de cada um no processo
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Se a corda estiver ativa...
        if (naAtiva)
        {
            // Pega o joint mais próximo de player
            Joint jmp = JointMaisProximo();
            // Pega o Transform do joint mais próximo
            Transform jmpTransform = jmp.t_Joint;
            // Pega o Rigidbody do joint mais próximo
            Rigidbody rbAtual = jmp.gj_Joint.rb;

            // Define o joint mais próximo como parent da Ashley
            ash.transform.parent = jmpTransform;

            #region Movimento
            // Move a Ashley ao longo da corda
            float vertical = Input.GetAxis("LeftVertical") * Time.fixedDeltaTime * velocidade;
            // Se estiver indo pra cima...
            if (vertical > 0.01f && ash.transform.position.y < listaJoints[0].t_Joint.position.y)
            {
                // Move na direção do parent do joint mais próximo
                ash.transform.position = Vector3.MoveTowards(ash.transform.position, jmpTransform.parent.position, Mathf.Abs(vertical));
            }
            else if (vertical < -0.01f && ash.transform.position.y > listaJoints[listaJoints.Count - 1].t_Joint.position.y)
            {
                // Move na direção do child do joint mais próximo
                ash.transform.position = Vector3.MoveTowards(ash.transform.position, jmpTransform.GetChild(0).position, Mathf.Abs(vertical));
            }

            // Ativa a animação
            anim.SetFloat("DireCorda", Input.GetAxis("LeftVertical"));

            // Zera a rotação da Ashley
            ash.transform.rotation = Quaternion.Euler(Vector3.zero);

            // Adiciona força de acordo com o eixo horizontal pra fazer a corda se movimentar lateralmente
            float horizontal = Input.GetAxis("LeftHorizontal");            
            rbAtual.AddForce(new Vector3(horizontal * forcaBalango, 0, 0));
            #endregion

            if(Input.GetButton("FaceA"))
            {
                Abortar();
            }
        }
        else if(timeLeft >= -1)
        {
            UpdateTimer();
        }
    }

    public float timeLeft = 7;
    public bool podePegar = true;
    private void UpdateTimer()
    {
        timeLeft -= Time.fixedDeltaTime;
        if (timeLeft <= 0)
        {
            Physics.IgnoreLayerCollision(0, 10, false);
            podePegar = true;
        }
    }

    private Joint JointMaisProximo()
    {
        // jointMaisProximo sempre é o que tem a distancia correspondete a distanciaAnterior
        // Coloquei o joint0 só pra não ser nulo
        Joint jointMaisProximo = listaJoints[0];
        
        // A float distanciaAnterior é a que vai ser comparada com a nova distancia lá embaixo no for
        // Coloquei a distancia do joint0 só pra não ser nulo
        float distanciaAnterior = Vector3.Distance(listaJoints[0].t_Joint.position, ash.transform.position);

        // Pra cada joint na lista...
        for (int i = 0; i < listaJoints.Count; i++)
        {
            // Calcula a distancia entre esse joint e player
            float novaDistancia = Vector3.Distance(listaJoints[i].t_Joint.position, ash.transform.position);

            // Se essa distancia for menor que a anterior
            if (novaDistancia < distanciaAnterior)
            {
                // Define esse joint como o mais próximo até então
                jointMaisProximo = listaJoints[i];
                // E essa distância como a menor até então
                distanciaAnterior = novaDistancia;
            }

            // O processo se repete pra cada joint na lista, de forma que a distancia de todos os joints seja testada
        }

        // Retorna o resultado
        return jointMaisProximo;
    }

    public void Abortar()
    {
        //anim.speed = 1f;
        anim.SetTrigger("SaiCorda");
        // Reseta o momento dela
        ashCtrl.moveDirection = Vector3.zero;
        // Bota um impulso de pulo reduzido nela
        ashCtrl.moveDirection.y = ashCtrl.jumpSpeed / 1.5f;
        // Devolvo o controle ao Player
        ashCtrl.sobControle = true;
        // Reativa o CharacterController
        ashCC.enabled = true;

        ashCtrl.transform.parent = null;

        // Avisa o paiDeTodos pra parar de funcionar
        naAtiva = false;

        podePegar = false;
        timeLeft = 3;
    }
}

