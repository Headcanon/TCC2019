using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste_JogaGranada : MouseFollower
{
    // Objetos necessparios para o script
    public GameObject seta, grenadePrefab;
    public LineRenderer lr;

    // Posição de lançamento
    public Transform posicao;

    // Força do arremesso
    public float throwSpeed = 40f;

    // Update is called once per frame
    void Update()
    {
        //Se apertar o ctrl
        if (Input.GetKey(KeyCode.LeftControl))
        {
            // Ativa a seta
            seta.gameObject.SetActive(true);
            lr.gameObject.SetActive(true);

            //Gira a seta seguindo o mouse
            SegueGiroCanvas(seta.transform, posicao);

            // Cria um arco seguindo o mouse
            Debug.Log("Arco = " + AnguloRotacao(transform).z * 100);
            Debug.Log("Seta = " + AnguloRotacao(seta.transform).z * 100);
            RenderArco(lr, 20, AnguloRotacao(transform).z * 100, throwSpeed, Mathf.Abs(Physics.gravity.y), posicao);

            // Se clicar com o mouse joga a granada
            if (Input.GetMouseButtonDown(0))
            {
                ThrowGrenade(AnguloRotacao(seta.transform));
            }

        }
        else
        {
            // Desativa a seta
            seta.gameObject.SetActive(false);
            lr.gameObject.SetActive(false);
        }
    }

    // Joga Granada
    private void ThrowGrenade(Quaternion rot)
    {
        GameObject grenade = Instantiate(grenadePrefab, posicao.position, rot);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.velocity = posicao.right * throwSpeed;
    }

    
}
