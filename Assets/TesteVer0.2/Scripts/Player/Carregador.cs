using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carregador : MonoBehaviour {

    Movimento move;

    private void OnTriggerEnter(Collider other)
    {
        move = other.GetComponent<Movimento>();
        if (move.jet != null)
        {
            move.jet.carregavel = true;
            move.jet.Carregar();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (move.jet != null)
        {
            move.jet.carregavel = false;
        }
        move = null;
    }
}
