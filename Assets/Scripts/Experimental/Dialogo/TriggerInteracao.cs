using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerInteracao : MonoBehaviour
{
    // Controle pra ser tirado de Player
    private ChrCtrl_Pipilson chr;

    public TextMeshProUGUI textDisplay;
    public Dialogo sentences;
    private int sentenceIndex;
    public float typingSpeed;

    #region Lista de personagens
    public Personagems[] personagems;

    [System.Serializable]
    public struct Personagems
    {
        public Dialogo.Personagem personagem;
        public Transform pos;
        public GameObject balao;
    }
    #endregion

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

        // Pra cada personagem da lista...
        foreach (Personagems p in personagems)
        {
            // Se o falante dessa frase for igual a personagem que está sendo verificada...
            if (sentences.GetPersonagem(sentenceIndex) == p.personagem)
            {
                // Bota o Display na posição indicada
                textDisplay.transform.position = p.pos.position;

                // Ativa a imagem do balão
                p.balao.SetActive(true);

                // Zera o display
                textDisplay.text = "";
            }
            else
            {
                // Desativa a imagem do balão
                p.balao.SetActive(false);
            }
        }

        // Começa a digitar a primeira frase
        StartCoroutine(TypeSentence());
    }
    #endregion

    #region Controle da interação
    private void Update()
    {
        // Se o texto em display for igual ao previsto na frase atual...
        if (textDisplay.text == sentences.GetTexto(sentenceIndex))
        {
            // Se apertar o botão de interação...
            if (Input.GetButtonDown("FaceX") && chr != null)
            {
                // Chama a próxima frase
                NextSentence();

                // Se já estiver na última frase...
                if (sentenceIndex == sentences.listaFrases.Length - 1)
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
        foreach (char letra in sentences.GetTexto(sentenceIndex))
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
        if (sentenceIndex < sentences.listaFrases.Length -1)
        {
            // Passa pra próxima frase
            sentenceIndex++;

            // Pra cada personagem da lista...
            foreach(Personagems p in personagems)
            {
                // Se o falante dessa frase for igual a personagem que está sendo verificada...
                if(sentences.GetPersonagem(sentenceIndex) == p.personagem)
                {                    
                    // Bota o Display na posição indicada
                    textDisplay.transform.position = p.pos.position;

                    // Ativa a imagem do balão
                    p.balao.SetActive(true);

                    // Zera o display
                    textDisplay.text = "";
                }
                else
                {
                    // Desativa a imagem do balão
                    p.balao.SetActive(false);
                }
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
