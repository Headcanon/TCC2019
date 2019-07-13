using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mensagem : MonoBehaviour {

    //public GameObject texto;
    public bool display;

    [TextArea(3,10)]
    public string[] textos;
    string textoFinal;

    private void OnTriggerEnter(Collider other)
    {
        //texto.SetActive(true);
        for (int i = 0; i < textos.Length; i++)
        {
            textoFinal = textoFinal + "\n" + textos[i];
        }
        display = true;
    }
    private void OnTriggerExit(Collider other)
    {
        //Destroy(texto);
        display = false;
        textoFinal = null;
    }

    private void OnGUI()
    {
        if (display)
        {
            //GUI.Box(new Rect(50, 50, 200, 200), texto);
            GUI.Box(new Rect(50, 50, 200, 200), textoFinal);
        }       
    }
}
