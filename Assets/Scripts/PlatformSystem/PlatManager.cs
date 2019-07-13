using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatManager : MonoBehaviour
{

    public GameObject plataforma;
    public Vector3 distancia;
    public float vaiVelocidade, voltaVelocidade;

    ActivePlatform plataformaAtiva;

    // Use this for initialization
    void Start()
    {
        plataformaAtiva = plataforma.GetComponent<ActivePlatform>();
    }

    private void Update()
    {
        if (!plataformaAtiva.Foi)
        {
            plataformaAtiva.Vai(distancia, vaiVelocidade);
        }
        else if (plataformaAtiva.Foi)
        {
            plataformaAtiva.Volta(distancia, voltaVelocidade);
        }
    }
}
