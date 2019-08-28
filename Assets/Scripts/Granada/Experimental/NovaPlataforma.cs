using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaPlataforma : MonoBehaviour
{
    public Vector3 dis;
    public float vaiVelocidade, voltaVelocidade;

    float currentVel;

    Transform start;
    bool foi = false;
    ChrCtrl chrctrl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!foi)
        {
            currentVel = vaiVelocidade;
            if(transform.position.x < start.position.x + dis.x)
            {
                transform.Translate(Vector3.right * Time.deltaTime * vaiVelocidade);
            }

            if (transform.position.y < start.position.y + dis.y)
            {
                transform.Translate(Vector3.up * Time.deltaTime * vaiVelocidade);
            }

            if (transform.position.z < start.position.z + dis.z)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * vaiVelocidade);
            }

            if (transform.position.x >= start.position.x + dis.x && transform.position.y >= start.position.y + dis.y && transform.position.z >= start.position.z + dis.z)
            {
                foi = true;
            }
        }
        else if (foi)
        {
            currentVel = voltaVelocidade;

            if (transform.position.x > start.position.x - dis.x)
            {
                transform.Translate(Vector3.right * Time.deltaTime * voltaVelocidade);
            }

            if (transform.position.y > start.position.y - dis.y)
            {
                transform.Translate(Vector3.up * Time.deltaTime * voltaVelocidade);
            }

            if (transform.position.z > start.position.z - dis.z)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * voltaVelocidade);
            }

            if (transform.position.x <= start.position.x - dis.x && transform.position.y <= start.position.y - dis.y && transform.position.z <= start.position.z - dis.z)
            {
                foi = false;
            }
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        chrctrl = other.GetComponent<ChrCtrl>();
        if (chrctrl != null)
        {
            chrctrl.speed += currentVel;
        }
    }
}
