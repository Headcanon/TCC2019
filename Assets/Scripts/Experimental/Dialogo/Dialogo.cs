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
    }

    public string GetTexto(int index)
    {
        return listaFrases[index].texto;
    }

    public Personagem GetPersonagem(int index)
    {
        return listaFrases[index].falante;
    }
}
