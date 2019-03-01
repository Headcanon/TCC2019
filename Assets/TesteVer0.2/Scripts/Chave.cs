using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour {

    public BotaoPlataforma trava;

    private void OnTriggerEnter(Collider other)
    {
        trava.travado = false;
        Destroy(gameObject);
    }
}
