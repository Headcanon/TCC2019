using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste_FollowArrow : MonoBehaviour {

    public float speed = 1f, throwForce = 40f;
    public GameObject seta, grenadePrefab, axolota;
    public Transform posicao;

    // Update is called once per frame
    void Update()
    {
        //Camera.current.WorldToScreenPoint(mina.transform.position);
        if (Input.GetKey(KeyCode.LeftControl))
        {
            seta.gameObject.SetActive(true);// Ativa a seta
            seta.transform.position = Camera.main.WorldToScreenPoint(posicao.position);// Coloca a seta na posição da personagem

            Vector2 direcao = Input.mousePosition - seta.transform.position;// Posição do mouse

            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;// Cria um anglo apontado pra posição do mouse
            Quaternion rotacao = Quaternion.AngleAxis(angulo, Vector3.forward);

            seta.transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, speed * Time.deltaTime);// Rotaciona a seta para esse angulo
            posicao.rotation = Quaternion.Slerp(transform.rotation, rotacao, speed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                ThrowGrenade(rotacao);
            }

        }
        else
        {
            seta.gameObject.SetActive(false);
        }
    }

    private void ThrowGrenade(Quaternion rot)
    {
        GameObject grenade = Instantiate(grenadePrefab, posicao.position, rot);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(posicao.right * throwForce);
    }
}
