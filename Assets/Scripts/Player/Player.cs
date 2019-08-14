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

    [Header("Movimento")]
    public float velocidade;
    public float jumpForce;
    public bool ddd;

    public GameObject ashModel;
    Animator anim;
    #endregion

    #region JetPack
    [Header("JetPack")]
    public JetPack jet;
    public Slider tanque;
    #endregion

    #region Vida&Morte
    [Header("Vida&Morte")]
    public float vida = 100;
    public Slider barraVida;
    public Transform spawnPoint;
    #endregion

    #endregion

    Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        anim = ashModel.GetComponent<Animator>();
    }
	
	void Update ()
    {
        // Movimento 2.5D
        if (!ddd)
        {
            float horizontal = Input.GetAxis("Horizontal");
            transform.Translate(transform.TransformDirection(transform.right) * Time.deltaTime * velocidade * horizontal);
            ashModel.transform.rotation = Quaternion.Euler(0, 180 - 90 * horizontal, 0);
            if (anim != null)
            {
                anim.SetFloat("Vel", Mathf.Abs(horizontal));
            }
        }
        // Movimento 3D
        else if(ddd)
        {
            float horizontal = Input.GetAxis("Horizontal");
            transform.Translate(-transform.TransformDirection(transform.forward) * Time.deltaTime * velocidade * horizontal);

            float vertical = Input.GetAxis("Vertical");
            transform.Translate(transform.TransformDirection(transform.right) * Time.deltaTime * velocidade * vertical);
        }

        // Pulo
        if (Mathf.Abs(rb.velocity.y) < .01)
        {
            //anim.SetBool("Pulano", false);
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddRelativeForce(Vector3.up * jumpForce);
                //anim.SetBool("Pulano", true);
            }
        }

        // Atualiza a barra de vida
        if (barraVida != null)
        {
            barraVida.value = vida / 100;
        }

        #region Jet Pack
        if (jet != null)
        {
            jet.Act(rb);
            tanque.value = jet.Combustivel / jet.CombustivelMaximo;            
        }
        #endregion
    }
    
    // Reduz a vida e mata player quando chega a 0
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

    // Da um GameOver quando destroi Player
    private void OnDestroy()
    {
        if(vida <= 0)
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
