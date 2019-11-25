using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerInteracao : MonoBehaviour
{
    public Dialogo dialogo;

    // Controle pra ser tirado de Player
    private ChrCtrl chr;
    private string textDisplayed;
    private int sentenceIndex = 0;

    #region Lista de personagens
    public Personagems[] personagems;

    [System.Serializable]
    public struct Personagems
    {
        public Dialogo.Personagem personagem;
        public GameObject balao;
        public Animator anim;
        public TextManager tm;
    }
    #endregion

    #region Inicio da interação
    // Quando Player entra no trigger...
    private void OnTriggerEnter(Collider other)
    {
        // Tenta pegar o controle
        chr = other.GetComponent<ChrCtrl>();

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
            if (dialogo.GetPersonagem(sentenceIndex) == p.personagem)
            {
                // Ativa a imagem do balão
                p.balao.SetActive(true);

                // Bota o texto
                textDisplayed = dialogo.GetTexto(sentenceIndex);

                // Começa a digitar a primeira frase
                p.tm.Digitar(textDisplayed);
            }
            else
            {
                // Desativa a imagem do balão
                p.balao.SetActive(false);
            }
        }


    }
    #endregion

    #region Controle da interação
    private void Update()
    {
        // Pra cada personagem da lista...
        foreach (Personagems p in personagems)
        {
            // Se o falante dessa frase for igual a personagem que está sendo verificada...
            if (dialogo.GetPersonagem(sentenceIndex) == p.personagem)
            {
                // Se o texto em display for igual ao previsto na frase atual...
                if (p.tm.textDisplay.text == dialogo.GetTexto(sentenceIndex))
                {
                    // Se apertar o botão de interação...
                    if (dialogo.GetPassar(sentenceIndex) || Input.GetButtonDown("FaceX") && chr != null)
                    {
                        // Chama a próxima frase
                        NextSentence();
                    }
                }
            }
        }
    }
    #endregion

    #region Administra a sequencia de frases
    private void NextSentence()
    {
        // Se a frase atual ainda for menor do que o total de frases...
        if (sentenceIndex < dialogo.listaFrases.Length -1)
        {
            // Passa pra próxima frase
            sentenceIndex++;

            // Pra cada personagem da lista...
            foreach (Personagems p in personagems)
            {
                // Se o falante dessa frase for igual a personagem que está sendo verificada...
                if(dialogo.GetPersonagem(sentenceIndex) == p.personagem)
                {
                    // Se o texto dessa frase não for vazio...
                    // Isso é usado pra possibilitar frases que não tenham texto
                    if (dialogo.GetTexto(sentenceIndex) != "")
                    {
                        // Ativa a imagem do balão
                        p.balao.SetActive(true);
                    }

                    // Se a animação dessa frase não for vazia...
                    // Isso é usado pra possibilitar frases que não tenham animação
                    if (dialogo.GetAnimacao(sentenceIndex) != "")
                    {
                        p.anim.SetTrigger(dialogo.GetAnimacao(sentenceIndex));
                    }

                    if(dialogo.GetTempo(sentenceIndex) != 0.0f)
                    {
                        Invoke("Desativar", dialogo.GetTempo(sentenceIndex));
                    }

                    // Bota o texto
                    textDisplayed = dialogo.GetTexto(sentenceIndex);

                    // Começa a corrotina de digitação
                    p.tm.Digitar(textDisplayed);
                }
                else
                {
                    // Desativa a imagem do balão
                    p.balao.SetActive(false);
                }
            }
        }
    }
    #endregion

    #region Desativa
    private void Desativar()
    {
        // Retorna o controle de Player
        chr.sobControle = true;

        gameObject.SetActive(false);
    }
    #endregion
}
