using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerInteracao : MonoBehaviour
{
    // Controle pra ser tirado de Player
    private ChrCtrl_Pipilson chr;

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    private bool podePassar = false;

    // Quando Player entra no trigger...
    private void OnTriggerEnter(Collider other)
    {
        // Tenta pegar o controle
        chr = other.GetComponent<ChrCtrl_Pipilson>();

        // Se o controle existir...
        if(chr != null)
        {
            // Zera o vetor de movimento
            chr.moveDirection = Vector3.zero;
            // Tira o controle de Player
            chr.sobControle = false;
        }

        StartCoroutine(Type());
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            podePassar = true;
        }

        if (Input.GetButtonDown("FaceX") && chr != null && podePassar)
        {
            NextSentence();            
            
            if(index == sentences.Length-1)
            {
                chr.sobControle = true;
            }            
        }
    }

    IEnumerator Type()
    {
        foreach (char letra in sentences[index].ToCharArray())
        {
            textDisplay.text += letra;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        podePassar = false;

        if (index < sentences.Length -1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
        }
    }
}
