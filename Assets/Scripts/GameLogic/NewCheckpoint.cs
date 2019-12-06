using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Quando o player passa por esse GameObject ele se torno seu novo checkpoint */
public class NewCheckpoint : MonoBehaviour
{
    public Transform spawnPoint;
    private Vida pl;
    private Save_Pipilson save;

    private void Start()
    {
        save = GameObject.Find("GM").GetComponent<Save_Pipilson>();
    }

    private void OnTriggerEnter(Collider other)
    {
        pl = other.GetComponent<Vida>();

        if(pl != null )
        {
            pl.spawnPoint = spawnPoint;
            transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = Color.green;
            save.CheckPoint(spawnPoint.position);
        }
    }
}
