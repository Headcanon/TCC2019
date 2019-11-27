using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public Dialogo dialogo;

    // Controle pra ser tirado de Player
    private ChrCtrl chr;
    private string textDisplayed;
    private int sentenceIndex = 0;
    private bool conversaAtiva = false;

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
            // Ativa a gravidade secundária para que player não fique flutuando
            chr.gravidadeSecundaria = true;

            conversaAtiva = true;

            NextSentence();
        }


    }
    #endregion

    #region Controle da interação
    private void Update()
    {
        if (PermitePassar())
        {           
            // Chama a próxima frase
            Invoke("Avancar", 0);
        }
    }

    private void Avancar()
    {
        // Passa pra próxima frase
        sentenceIndex++;
        NextSentence();
    }
    #endregion

    #region Administra a sequencia de frases
    private void NextSentence()
    {
        // Se a frase atual ainda for menor do que o total de frases...
        if (sentenceIndex < dialogo.listaFrases.Length)
        {           

            // Pra cada personagem da lista...
            foreach (Personagems p in personagems)
            {
                // Se o falante dessa frase for igual a personagem que está sendo verificada...
                if(dialogo.GetPersonagem(sentenceIndex) == p.personagem)
                {
                    // Bota o texto
                    textDisplayed = dialogo.GetTexto(sentenceIndex);

                    ExecutarAcoes(p);
                }
                else if(p.balao != null)
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
        // Desativa a gravidade secundária
        chr.gravidadeSecundaria = false;

        conversaAtiva = false;
        gameObject.SetActive(false);
    }
    #endregion

    private float timeCounter;

    private bool PermitePassar()
    {
        bool permite = false;

        if (conversaAtiva)
        {
            // Pra cada personagem da lista...
            foreach (Personagems p in personagems)
            {
                //Debug.Log("Testou " + p.personagem);
                // Se o falante dessa frase for igual a personagem que está sendo verificada...
                if (dialogo.GetPersonagem(sentenceIndex) == p.personagem)
                {
                    //Debug.Log("Confirmou " + p.personagem);
                    // Se o texto em display for igual ao previsto na frase atual...
                    if (p.tm.textDisplay.text == dialogo.GetTexto(sentenceIndex) && timeCounter >= dialogo.GetTempoEsperar(sentenceIndex))
                    {
                        Debug.Log("O texto é igual");
                        // Se apertar o botão de interação...
                        if (dialogo.GetPassar(sentenceIndex) || Input.GetButtonDown("FaceX") && chr != null)
                        {
                            timeCounter = 0;
                            Debug.Log("Foi liberado");
                            permite = true;
                        }
                    }
                    else if(p.tm.textDisplay.text == dialogo.GetTexto(sentenceIndex))
                    {
                        timeCounter += Time.deltaTime;
                    }
                }
            }
        }

        return permite;
    }

    private void ExecutarAcoes(Personagems p)
    {
        #region Texto
        // Se o texto dessa frase não for vazio...
        // Isso é usado pra possibilitar frases que não tenham texto
        if (dialogo.GetTexto(sentenceIndex) != "")
        {
            // Ativa a imagem do balão
            p.balao.SetActive(true);
        }

        // Começa a corrotina de digitação
        p.tm.Digitar(textDisplayed);
        #endregion

        #region Animação
        // Se a animação dessa frase não for vazia...
        // Isso é usado pra possibilitar frases que não tenham animação
        if (dialogo.GetAnimacao(sentenceIndex) != "")
        {
            p.anim.SetTrigger(dialogo.GetAnimacao(sentenceIndex));
            Debug.Log("foi " + dialogo.GetAnimacao(sentenceIndex));
        }
        #endregion

        #region Desativação
        // Se tiver colocado um tempo de desativar...
        if (dialogo.GetTempoDesativar(sentenceIndex) != 0.0f)
        {
            // Desativa depois do tempo indicado
            Invoke("Desativar", dialogo.GetTempoDesativar(sentenceIndex));
        }
        #endregion

        #region Redução de pontuação
        if (dialogo.GetReducao(sentenceIndex) != 0)
        {
            Pontuacao pontuacao = FindObjectOfType<Pontuacao>();
            Debug.Log(pontuacao.name);
            pontuacao.Reduzir(dialogo.GetReducao(sentenceIndex));
        }
        #endregion
    }
}
