using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class JetPack : MonoBehaviour
{

    //private float combustivelMaximo;
    //private float combustivel;

    private float combustivel;
    public float Combustivel { get { return combustivel; } }

    public float CombustivelMaximo;
    public float jetForce;
    public bool carregavel;

    GameObject corpo;
    public GameObject fogo;


    void Start()
    {
        combustivel = CombustivelMaximo;
        corpo = GetComponent<GameObject>();
    }

    public void Act(Rigidbody rb)
    {
        if (Mathf.Abs(rb.velocity.y) != 0 && Input.GetButton("Jump"))
        {
            Voar(rb);
        }
        else if (Mathf.Abs(rb.velocity.y) < .01)
        {           
            Carregar();
        }
        else if (fogo != null)
        {
            fogo.SetActive(false);
        }
    }

    public void Voar(Rigidbody rb)
    {
        if (combustivel > 0)
        {
            rb.AddRelativeForce(Vector3.up * jetForce);
            combustivel--;

            if (fogo != null)
            {
                fogo.SetActive(true);
            }
        }
    }

    public void Carregar()
    {
        if(combustivel <= CombustivelMaximo && carregavel)
        {
            combustivel++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player currentJet;
        currentJet = other.GetComponent<Player>();
        currentJet.jet = this;
    }
}

