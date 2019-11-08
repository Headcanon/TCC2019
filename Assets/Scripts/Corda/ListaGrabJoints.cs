using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaGrabJoints : MonoBehaviour
{
    public List<GrabJoint> listaJoints;

    // Start is called before the first frame update
    void Start()
    {
        // Pega o filho desse objeto
        Transform child = transform.GetChild(0);

        // Enquanto o filho não for nulo, ou seja, child tiver outro child...
        for (int i = 0; child.childCount == 1; i++)
        {
            // Adiciona o componente GrabJoint a ele
            GrabJoint gj = child.gameObject.AddComponent<GrabJoint>();
            // Define o parametro rb como o Rigidbody dele mesmo
            gj.rb = child.gameObject.GetComponent<Rigidbody>();
            gj.rb.centerOfMass = new Vector3(0, 0, 0);
            gj.rb.inertiaTensor = new Vector3(1, 1, 1);

            // Define o parametro paiDeTodos como essa lista
            gj.paiDeTodos = GetComponent<ListaJoints>();

            // Adiciona ele à lista
            listaJoints.Add(gj);
            // E pega o child dele
            child = child.GetChild(0);

            // O loop só vai parar quando child não tiver outro child
            // E vai pegando os filhos de cada um no processo
        }
    }

    public GrabJoint JointMaisProximo(Transform compare)
    {
        // jointMaisProximo sempre é o que tem a distancia correspondete a distanciaAnterior
        // Coloquei o joint0 só pra não ser nulo
        GrabJoint jointMaisProximo = listaJoints[0];

        // A float distanciaAnterior é a que vai ser comparada com a nova distancia lá embaixo no for
        // Coloquei a distancia do joint0 só pra não ser nulo
        float distanciaAnterior = Vector3.Distance(listaJoints[0].transform.position, compare.position);

        // Pra cada joint na lista...
        for (int i = 0; i < listaJoints.Count; i++)
        {
            // Calcula a distancia entre esse joint e player
            float novaDistancia = Vector3.Distance(listaJoints[i].transform.position, compare.position);

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
