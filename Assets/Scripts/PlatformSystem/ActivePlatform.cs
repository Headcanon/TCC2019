using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlatform : MonoBehaviour
{
    Rigidbody rb;
    Transform start;

    public bool Foi;

    //public Vector3 distancia;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Cria um novo game object pra servir de referencia pro ponto inicial
        start = new GameObject().transform;
        start.position = transform.position;

    }

    
        // Vai
    public void Vai(Vector3 dis, float vel)
    {
        if (transform.position.x < start.position.x + dis.x)
        {
            transform.Translate(Vector3.right * Time.deltaTime * vel);
        }

        if (transform.position.y < start.position.y + dis.y)
        {
            transform.Translate(Vector3.up * Time.deltaTime * vel);
        }

        if (transform.position.z < start.position.z + dis.z)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * vel);
        }

        if(transform.position.x >= start.position.x + dis.x && transform.position.y >= start.position.y + dis.y && transform.position.z >= start.position.z + dis.z)
        {
            Foi = true;
        }

    }
    
    //VEM
    public void Volta(Vector3 dis, float vel)
    {
        if (transform.position.x > start.position.x - dis.x)
        {
            transform.Translate(Vector3.right * Time.deltaTime * -vel);
        }

        if (transform.position.y > start.position.y - dis.y)
        {
            transform.Translate(Vector3.up * Time.deltaTime * -vel);
        }

        if (transform.position.z > start.position.z - dis.z)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -vel);
        }

        if (transform.position.x <= start.position.x - dis.x && transform.position.y <= start.position.y - dis.y && transform.position.z <= start.position.z - dis.z)
        {
            Foi = false;
        }
    }

}
