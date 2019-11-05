using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogo", menuName = "ScriptableObjects/Dialogo", order = 1)]
public class Dialogo : ScriptableObject
{
    public enum Personagem { Ashley, Rival };    

    public Frase[] listaFrases;

    [System.Serializable]
    public struct Frase
    {
        public string texto;
        public Personagem falante;
        public string animacao;
        public float tempoDesativar;
    }

    public Frase GetFrase(int index)
    {
        return listaFrases[index];
    }

    public string GetTexto(int index)
    {
        return listaFrases[index].texto;
    }

    public Personagem GetPersonagem(int index)
    {
        return listaFrases[index].falante;
    }

    public string GetAnimacao(int index)
    {
        return listaFrases[index].animacao;
    }

    public float GetTempo(int index)
    {
        return listaFrases[index].tempoDesativar;
    }
}
