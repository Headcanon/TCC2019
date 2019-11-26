using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogo", menuName = "ScriptableObjects/Dialogo", order = 1)]
public class Dialogo : ScriptableObject
{
    public enum Personagem { Ashley, Rival, Camera };    

    public Frase[] listaFrases;

    [System.Serializable]
    public struct Frase
    {
        public string texto;
        public Personagem falante;
        public string animacao;
        public float tempoDesativar;
        public bool autoPassar;
        public float esperar;
        public int reduzirPontos;
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

    public float GetTempoDesativar(int index)
    {
        return listaFrases[index].tempoDesativar;
    }

    public bool GetPassar(int index)
    {
        return listaFrases[index].autoPassar;
    }

    public float GetTempoEsperar(int index)
    {
        return listaFrases[index].esperar;
    }

    public int GetReducao(int index)
    {
        return listaFrases[index].reduzirPontos;
    }
}
