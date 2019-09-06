using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseJoint : MonoBehaviour
{
    public float forca = 1;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector3(horizontal * forca, 0, 0));
    }
}
