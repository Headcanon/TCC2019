using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezing : MonoBehaviour {

    public float delay = 3f;
    public float raio = 7f;

    bool explodiu = false;

    float countdown;

    // Use this for initialization
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && explodiu == false)
        {
            Explode();
        }
    }

    void Explode()
    {
        Debug.Log("Fica frio aí!");

        Collider[] colliders = Physics.OverlapSphere(transform.position, raio);
        foreach (Collider objetoProximo in colliders)
        {
            Freezable frz = objetoProximo.GetComponent<Freezable>();
            if (frz != null)
            {
                frz.Freeze();
            }
        }

        Destroy(gameObject);

        explodiu = true;
    }
}
