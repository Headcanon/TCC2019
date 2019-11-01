using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerInteracao : MonoBehaviour
{
    // Controle pra ser tirado de Player
    private ChrCtrl_Pipilson chr;

    public TextMeshProUGUI textDisplay;
    public Frase[] sentences;
    private int sentenceIndex;
    public float typingSpeed;

    #region Inicio da interação
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

        // Zera o display
        textDisplay.text = "";

        // Começa a digitar a primeira frase
        StartCoroutine(TypeSentence());
    }
    #endregion

    #region Controle da interação
    private void Update()
    {
        // Se o texto em display for igual ao previsto na frase atual...
        if (textDisplay.text == sentences[sentenceIndex].texto)
        {
            // Se apertar o botão de interação...
            if (Input.GetButtonDown("FaceX") && chr != null)
            {
                // Chama a próxima frase
                NextSentence();

                // Se já estiver na última frase...
                if (sentenceIndex == sentences.Length - 1)
                {
                    // Retorna o controle de Player
                    chr.sobControle = true;
                }
            }
        }
    }
    #endregion

    #region Digitar
    // Corrotina
    IEnumerator TypeSentence()
    {
        // Pra cad letra da frase atual...
        foreach (char letra in sentences[sentenceIndex].texto.ToCharArray())
        {
            // Adiciona uma letra no texto de display
            textDisplay.text += letra;
            // Espera o tempo determinado
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    #endregion

    #region Administra a sequencia de frases
    private void NextSentence()
    {
        // Se a frase atual ainda for menor do que o total de frases...
        if (sentenceIndex < sentences.Length -1)
        {
            // Passa pra próxima frase
            sentenceIndex++;

            if (sentences[sentenceIndex].falante == Frase.Personagem.Ashley)
            {
                // Zera o display
                textDisplay.transform.position = chr.gameObject.transform.position;
                textDisplay.text = "";
            }
            else
            {
                // Zera o display
                textDisplay.text = "";
            }

            // Começa a corrotina de digitação
            StartCoroutine(TypeSentence());
        }
        // Caso contrário...
        else
        {
            // Zera o display
            textDisplay.text = "";
        }
    }
    #endregion
}
