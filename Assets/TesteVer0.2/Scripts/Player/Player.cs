using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
      #region Variaveis

      #region Movimento
      bool pulou = false;
      Transform paiTransform;

      public float velocidade;
      public float jumpForce;
      public bool ddd;
      #endregion

      #region JetPack
      public JetPack jet;
      public Slider tanque;
      #endregion

      #region Vida&Morte
      public float vida = 100;
      public Slider barraVida;
      public Transform spawnPoint;
      #endregion

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

        barraVida.value = vida / 100;

        #region Jet Pack
        if (jet != null)
        {
            jet.Act(rb);
            tanque.value = jet.Combustivel / jet.CombustivelMaximo;            
        }
        #endregion
    }

    public void Damage(float dano)
    {
        vida -= dano;
        if (vida > 0 && spawnPoint!=null)
        {
            this.transform.position = spawnPoint.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(vida <= 0)
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
