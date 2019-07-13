using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour {

    //bool fim = false;
    //Transform start;

    private void Start()
    {
        //start = transform;
    }

    // Update is called once per frame
    void Update () {
        //if (transform.position.z >= start.position.z + 100 && !fim)
        //{
        //    transform.Translate(Vector3.forward * Time.deltaTime * 5);
        //}
        //else if (transform.position.z < start.position.z && fim)
        //{
        //    transform.Translate(Vector3.back * Time.deltaTime * 5);
        //}
        //else
        //{
        //    fim = !fim;
        //}
	}

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
