using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniProjetil : MonoBehaviour
{
    Rigidbody rb;
    Movimento target;
    Inimigo spawner;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<Movimento>();
        spawner = FindObjectOfType<Inimigo>();

        moveDirection = (target.transform.position - transform.position).normalized * 7f;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
        Physics.IgnoreCollision(GetComponent<Collider>(), spawner.GetComponent<Collider>());

        Destroy(gameObject, 6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Trigger")
        {
            Destroy(gameObject);
        }
    }
}
