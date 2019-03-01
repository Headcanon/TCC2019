using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimento : MonoBehaviour
{
    #region Variaveis
    bool pulou = false;
    Transform paiTransform;

    public float velocidade;
    public float jumpForce;
    public bool ddd;

    public JetPack jet;
    public Slider tanque;

    public Transform spawnPoint;
    #endregion

    Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {

        if (!ddd)
        {
            float horizontal = Input.GetAxis("Horizontal");
            transform.Translate(transform.TransformDirection(transform.right) * Time.deltaTime * velocidade * horizontal);
        }
        else if(ddd)
        {
            float horizontal = Input.GetAxis("Horizontal");
            transform.Translate(-transform.TransformDirection(transform.forward) * Time.deltaTime * velocidade * horizontal);

            float vertical = Input.GetAxis("Vertical");
            transform.Translate(transform.TransformDirection(transform.right) * Time.deltaTime * velocidade * vertical);
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < .01)
        {
            rb.AddRelativeForce(Vector3.up * jumpForce);
        }

        #region Jet Pack
        if (jet != null)
        {
            jet.Act(rb);
            tanque.value = jet.Combustivel / jet.CombustivelMaximo;            
        }
        #endregion
    }

    public void Respawn()
    {
        this.transform.position = spawnPoint.position;
    }
}
