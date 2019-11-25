using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaJoints : MonoBehaviour
{
    private enum Estados { Subindo, Descendo, Fora, Parado}
    private Estados estadoAtual;

    public float forcaBalango = 2;

    // Lista de todos os joints na corda
    private ListaGrabJoints listaJoints;
    // Transform de player
    private GameObject ash;
    public ChrCtrl ashCtrl;
    public CharacterController ashCC;

    // bool que define se a corda está ativada
    public bool naAtiva = false;

    // Velocidade de escalada da corda
    public float velocidadeEscalada;

    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        // Cria a lista
        listaJoints = GetComponent<ListaGrabJoints>();

        // Acha a coisas da Ashley
        ash = GameObject.FindGameObjectWithTag("Player");
        ashCtrl = ash.GetComponent<ChrCtrl>();
        ashCC = ash.GetComponent<CharacterController>();
        anim = ash.transform.GetChild(0).GetComponent<Animator>();
        
    }

    GrabJoint jmp;

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Se a corda estiver ativa...
        if (naAtiva)
        {
            // Pega o joint mais próximo de player
             jmp = listaJoints.JointMaisProximo(ash.transform);
            // Pega o Transform do joint mais próximo
            Transform jmpTransform = jmp.transform;
            // Pega o Rigidbody do joint mais próximo
            Rigidbody rbAtual = jmp.rb;

            // Define o joint mais próximo como parent da Ashley
            ash.transform.SetParent(jmpTransform, true);

            #region Movimento
            // Move a Ashley ao longo da corda
            float vertical = Input.GetAxis("LeftVertical") * Time.fixedDeltaTime * velocidadeEscalada;
            // Se estiver indo pra cima...
            if (vertical > 0.01f && ash.transform.position.y < listaJoints.listaJoints[0].transform.position.y)
            {
                // Move na direção do parent do joint mais próximo
                ash.transform.position = Vector3.MoveTowards(ash.transform.position, jmpTransform.parent.position, Mathf.Abs(vertical));
            }
            else if (vertical < -0.01f && ash.transform.position.y > listaJoints.listaJoints[listaJoints.listaJoints.Count - 1].transform.position.y)
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
            podePegar = true;
        }
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

