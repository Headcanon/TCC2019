using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centralizador : MonoBehaviour
{
    bool centralizado = false;

    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<Player>() != null && !centralizado)
        {
            if (other.transform.position.z < this.transform.position.z && !centralizado)
            {
                other.transform.Translate(Vector3.forward * Time.deltaTime * 7);                
            }
            else if(other.transform.position.z > this.transform.position.z && !centralizado)
            {
                other.transform.Translate(Vector3.forward * Time.deltaTime * -7);               
            }

            if(other.transform.position.z >= this.transform.position.z - 1 && other.transform.position.z <= this.transform.position.z + 1)
            {
                centralizado = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        centralizado = false;
    }
}
