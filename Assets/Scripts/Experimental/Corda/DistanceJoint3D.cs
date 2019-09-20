using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Insipirado no vídeo Convert 2D Joint in 3D Joints: https://www.youtube.com/watch?v=ft6s09cq7DM */
public class DistanceJoint3D : MonoBehaviour
{
    // Transform onde esse objeto vai se pendurar
    public Transform penduradoEm;
    // Distancia entre esse objeto e o que ele está pendurado
    public float distance;

    // Rigidbody desse objeto
    protected Rigidbody rb;

    #region Propriedades da conexão
    [Header("Propriedades da conexão")]
    // "Mola", quanto maior mais velocidade tem esse objeto
    public float spring = 0.1f;
    // "Amortecedor, quanto maior menos velocidade tem esse objeto
    public float damper = 5f;
    #endregion

    void Awake()
    {
        // Pega o Rigidbody desse objeto
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Determina a distancia entre Rigidbody desse objeto e o conectado
        distance = Vector3.Distance(rb.position, penduradoEm.position) - 0.5f;
    }

    void FixedUpdate()
    {
        // Conection é a distancia entre Rigidbody desse objeto e o conectado
        // Se você escolheu determinar a distância no start o Vector connection deve ser igual ao Vector3 Distance
        Vector3 connection = rb.position - penduradoEm.position;

        // Determina a diferença entre a distância estabelecida e a conexão real atual
        float distanceDiscrepancy = distance - connection.magnitude;

        // Multiplica a discrepância pela conexão atual
        // Depois adiciona o resultado à posição do Rigidbody
        // E isso de alguma forma faz com que a distância entre os dois não aumente
        rb.position += distanceDiscrepancy * connection.normalized;

        // Velocidade desejada
        Vector3 velocityTarget = connection + (rb.velocity + Physics.gravity * spring);

        // Projeta o 
        Vector3 projectOnConnection = Vector3.Project(velocityTarget, connection);
        rb.velocity = (velocityTarget - projectOnConnection) / (1 + damper * Time.fixedDeltaTime);

        if (Input.GetButton("Jump") && transform.position.y < penduradoEm.position.y - 2)
            distance-= 0.1f;

    }
}
