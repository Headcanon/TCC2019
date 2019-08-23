using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlatform : MonoBehaviour
{
    Rigidbody rb;
    Transform transformer;
    Transform start;


    public bool Foi;

    //public Vector3 distancia;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transformer = GetComponent<Transform>();
        start = new GameObject().transform;
        start.position = transformer.position;

    }

    
        // Vai
    public void Vai(Vector3 dis, float vel)
    {
        if (transformer.position.x < start.position.x + dis.x)
        {
            transformer.Translate(Vector3.right * Time.deltaTime * vel);
        }

        if (transformer.position.y < start.position.y + dis.y)
        {
            transformer.Translate(Vector3.up * Time.deltaTime * vel);
        }

        if (transformer.position.z < start.position.z + dis.z)
        {
            transformer.Translate(Vector3.forward * Time.deltaTime * vel);
        }

        if(transformer.position.x >= start.position.x + dis.x && transformer.position.y >= start.position.y + dis.y && transformer.position.z >= start.position.z + dis.z)
        {
            Foi = true;
        }

    }
    
    //VEM
    public void Volta(Vector3 dis, float vel)
    {
        if (transformer.position.x > start.position.x - dis.x)
        {
            transformer.Translate(Vector3.right * Time.deltaTime * -vel);
        }

        if (transformer.position.y > start.position.y - dis.y)
        {
            transformer.Translate(Vector3.up * Time.deltaTime * -vel);
        }

        if (transformer.position.z > start.position.z - dis.z)
        {
            transformer.Translate(Vector3.forward * Time.deltaTime * -vel);
        }

        if (transformer.position.x <= start.position.x - dis.x && transformer.position.y <= start.position.y - dis.y && transformer.position.z <= start.position.z - dis.z)
        {
            Foi = false;
        }
    }

}
