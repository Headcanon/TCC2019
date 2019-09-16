using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaJoints : MonoBehaviour
{
    #region Parametros
    // Lista de todos os joints na corda
    private List<Transform> listaJoints;
    // Transform de player
    private Transform ash;

    // bool que define se a corda está ativada
    public bool naAtiva = false;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        // Cria a lista e acha a Ashley
        listaJoints = new List<Transform>();
        ash = GameObject.FindGameObjectWithTag("Player").transform;

        // Pega o filho desse objeto
        Transform child = transform.GetChild(0);

        // Enquanto o filho não for nulo, ou seja, child tiver outro child...
        while (child != null)
        {
            // Adiciona o componente GrabJoint a ele
            GrabJoint gj = child.gameObject.AddComponent<GrabJoint>();
            // Define o parametro rb como o Rigidbody dele mesmo
            gj.rb = child.gameObject.GetComponent<Rigidbody>();
            // Define o parametro paiDeTodos como essa lista
            gj.paiDeTodos = this;

            // Adiciona ele à lista
            listaJoints.Add(child);
            // E pega o child dele
            child = child.GetChild(0);

            // O loop só vai parar quando child não tiver outro child
            // E vai pegando os filhos de cada um no processo
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Se a corda estiver ativa...
        if (naAtiva)
        {
            // Pega o joint mais próximo de player
            Transform jmp = JointMaisProximo();
            /* Eu usava esse debug pra ver qual era o joint mais próximo */
            //Debug.Log(jmp);

            // Pega o Rigidbody ei GrabJoint do joint mais próximo
            Rigidbody rbAtual = jmp.GetComponent<Rigidbody>();
            GrabJoint gjAtual = jmp.GetComponent<GrabJoint>();

            // Define o joint mais próximo como parent da Ashley
            ash.parent = jmp;

            #region Movimento
            // Move a Ashley ao longo da corda
            float vertical = Input.GetAxis("Vertical") * Time.fixedDeltaTime;
            if (vertical > 0.01f && Vector3.Distance(ash.position, listaJoints[0].position) > 0.2f)
            {
                ash.transform.position = Vector3.MoveTowards(ash.transform.position, jmp.parent.position, vertical);
            }
            else if (vertical < -0.01f && Vector3.Distance(ash.position, listaJoints[listaJoints.Count - 1].position) > 0.2f)
            {
                ash.transform.position = Vector3.MoveTowards(ash.transform.position, jmp.GetChild(0).position, vertical);
            }

            // Adiciona força de acordo com o eixo horizontal
            float horizontal = Input.GetAxis("Horizontal");            
            rbAtual.AddForce(new Vector3(horizontal * 2, 0, 0));
            #endregion

            if(Input.GetButton("Jump"))
            {
                ash.parent = null;
                gjAtual.Abortar();
            }
        }
    }

    private Transform JointMaisProximo()
    {
        // jointMaisProximo sempre é o que tem a distancia correspondete a distanciaAnterior
        // Coloquei o joint0 só pra não ser nulo
        Transform jointMaisProximo = listaJoints[0];
        
        // A float distanciaAnterior é a que vai ser comparada com a nova distancia lá embaixo no for
        // Coloquei a distancia do joint0 só pra não ser nulo
        float distanciaAnterior = Vector3.Distance(listaJoints[0].position, ash.position);

        // Pra cada joint na lista...
        for (int i = 0; i < listaJoints.Count; i++)
        {
            // Calcula a distancia entre esse joint e player
            float novaDistancia = Vector3.Distance(listaJoints[i].position, ash.position);

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
}

