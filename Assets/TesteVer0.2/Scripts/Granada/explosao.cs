using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosao : MonoBehaviour {

    public float delay = 3f;
    public float raio = 7f;
    bool explodiu = false;
    float countdown;
	// Use this for initialization
	void Start ()
    {
        countdown = delay;
	}
	
	// Update is called once per frame
	void Update ()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && explodiu == false)
        {
            Explode();
        }
	}

    void Explode()
    {
        Debug.Log("boom!");

        Collider[] colliders = Physics.OverlapSphere(transform.position, raio);
        foreach (Collider objetoProximo in colliders)
        {
            Destructible dstr = objetoProximo.GetComponent<Destructible>();
            if (dstr != null)
            {
                dstr.Destroy();
            }          
        }
        
        Destroy(gameObject);

        explodiu = true;
    }
}
