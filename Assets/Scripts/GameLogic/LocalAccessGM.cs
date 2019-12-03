using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAccessGM : MonoBehaviour
{
    public void Resetar()
    {
        GameObject.Find("GM").GetComponent<Save_Pipilson>().Resetar();
    }
}
