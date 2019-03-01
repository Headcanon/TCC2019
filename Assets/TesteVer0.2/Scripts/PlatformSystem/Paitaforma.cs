using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paitaforma : MonoBehaviour
{
    public PlatManager[] plats;

    private void Start()
    {
        for (int i = 0; i < plats.Length; i++)
        {
            plats[i] = new PlatManager();
        }
    }

}
