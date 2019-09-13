using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanchoDeEscalada : MonoBehaviour
{
    // Joint de conexão da ash com o objeto
    //HingeJoint hj;
    DistanceJoint3D dj3d;

    // Posição em que a corda é mirada
    Vector3 targetPos;

    // Alcance da corda
    public float alcance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // O joint já é colocado na Ash desde o começo
        // Essa parte deixa ele desativado até ser chamado
        dj3d = GetComponent<DistanceJoint3D>();
        dj3d.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            // Mira na posição do mouse
            targetPos = Input.mousePosition;
            targetPos.z = 0;

            // Desenha um raio da Ash até a posição da mira
            Debug.DrawRay(transform.position, targetPos - transform.position, Color.green);
            RaycastHit hit;
            Physics.Raycast(transform.position, targetPos - transform.position, out hit, alcance);

            // Se o raio encostou em alguma coisa com um RigidBody...
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody>() != null)
            {
                // Se atirar...
                if (Input.GetButton("Fire1"))
                {
                    //hj.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
                    dj3d.enabled = true;
                    dj3d.penduradoEm = hit.collider.gameObject.GetComponent<Rigidbody>().transform;
                }
            }
        }
        else
        {
            dj3d.enabled = false;
        }
    }
}
