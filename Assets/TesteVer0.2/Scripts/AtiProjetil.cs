using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiProjetil : MonoBehaviour
{
    Rigidbody rb;
    Inimigo target;
    //Atirador spawner;
    Movimento axel;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<Inimigo>();
        //spawner = FindObjectOfType<Atirador>();
        axel = FindObjectOfType<Movimento>();

        moveDirection = (target.transform.position - transform.position).normalized * 7f;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
        //Physics.IgnoreCollision(GetComponent<Collider>(), spawner.GetComponent<Collider>());
        Physics.IgnoreCollision(GetComponent<Collider>(), axel.GetComponent<Collider>());

        Destroy(gameObject, 6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Trigger" && other.tag != "Atirador")
        {
            Destroy(gameObject);
        }
    }
}
