using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour {

    [FMODUnity.EventRef]
    public string keySound;
    public BotaoPlataforma trava;

    private void OnTriggerEnter(Collider other)
    {
        FMODUnity.RuntimeManager.PlayOneShot(keySound);
        trava.travado = false;
        gameObject.SetActive(false);
    }
}
