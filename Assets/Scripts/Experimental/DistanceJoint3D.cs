using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Insipirado no vídeo Convert 2D Joint in 3D Joints: https://www.youtube.com/watch?v=ft6s09cq7DM */
public class DistanceJoint3D : MonoBehaviour
{
    public Transform ConnectedRigidbody;

    // Define se a distance é definida no Start
    public bool determineDistanceOnStart = true;
    public float distance;
    public float spring = 0.1f;
    public float damper = 5f;

    protected Rigidbody Rigidbody;

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Se eu marquei pra determinar no start e há um Rigidbody conectado...
        if (determineDistanceOnStart && ConnectedRigidbody != null)
        {
            // Determina a distancia entre Rigidbody desse objeto e o conectado
            distance = Vector3.Distance(Rigidbody.position, ConnectedRigidbody.position);
        }
    }

    void FixedUpdate()
    {

        // Conection é a distancia entre Rigidbody desse objeto e o conectado
        // Se você escolheu determinar a distância no start o Vector connection deve ser igual ao Vector3 Distance
        Vector3 connection = Rigidbody.position - ConnectedRigidbody.position;

        // Determina a diferença entre a distância estabelecida e a conexão real atual
        float distanceDiscrepancy = distance - connection.magnitude;

        // Multiplica a discrepância pela conexão atual
        // Depois adiciona o resultado à posição do Rigidbody
        // E isso de alguma forma faz com que a distância entre os dois não aumente
        Rigidbody.position += distanceDiscrepancy * connection.normalized;

        var velocityTarget = connection + (Rigidbody.velocity + Physics.gravity * spring);
        var projectOnConnection = Vector3.Project(velocityTarget, connection);
        Rigidbody.velocity = (velocityTarget - projectOnConnection) / (1 + damper * Time.fixedDeltaTime);
    }
}
