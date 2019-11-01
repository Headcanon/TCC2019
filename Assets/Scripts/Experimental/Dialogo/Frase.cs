using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Frase", menuName = "ScriptableObjects/Frase", order = 1)]
public class Frase : ScriptableObject
{
    public enum Personagem { Ashley, Rival };

    public Personagem falante;
    public string texto;
    public Vector2 pos;
}
