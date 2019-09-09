using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanchoDeEscalada : MonoBehaviour
{
    DistanceJoint3D_Gravity dj3d;
    Vector3 targetPos;
    RaycastHit hit;
    public float distancia = 10f;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        dj3d = GetComponent<DistanceJoint3D_Gravity>();
        dj3d.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            targetPos = Input.mousePosition;
            targetPos.z = 0;

            Debug.DrawRay(transform.position, targetPos - transform.position, Color.green);
            Physics.Raycast(transform.position, targetPos - transform.position, out hit, distancia);

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody>() != null)
            {
                if (Input.GetButton("Fire1"))
                {
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
