using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextIniciator : MonoBehaviour
{
    public TextManager tm;
    public string texto;

    private void OnTriggerEnter(Collider other)
    {
        tm.Digitar(texto);
        Destroy(gameObject);
    }
}
