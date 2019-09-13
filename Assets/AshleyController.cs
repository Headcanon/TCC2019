using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshleyController : MonoBehaviour
{
    public bool joint;
    public Rigidbody conectadoEm;

    CharacterController cc;
    Rigidbody rb;
    HingeJoint hj;      

    private void Update()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        hj = GetComponent<HingeJoint>();

        if (joint && rb == null && hj == null)
        {
            cc.enabled = false;
            hj = gameObject.AddComponent<HingeJoint>();
            hj.axis = new Vector3(0, 0, 1);
            //hj.anchor = new Vector3(0, 0.2f);
            hj.connectedBody = conectadoEm;
        }
        else if(!joint && rb != null && hj != null)
        {
            Destroy(hj);
            Destroy(rb);
            cc.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (joint && rb != null && hj != null)
        {
            float horizontal = Input.GetAxis("Horizontal");
            rb.AddForce(new Vector3(horizontal * 1, 0, 0));
        }
    }


    /* Porque usar Character controller?
     * - Tem função is grounded
     * - Posso personalizar a gravidade
     * Porque trocar pro Rigidbody?
     * - Hingejoint
     */
}
