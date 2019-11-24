using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyManager : MonoBehaviour
{
    //public CinemachineSmoothPath csp;
    private CinemachineTrackedDolly ctd;
    private Animator anim;
    public NextCam[] cameras;

    [System.Serializable]
    public struct NextCam
    {
        public string cameraAtual;
        public float posicaoSaida;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ctd = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (ctd.m_PathPosition > cameras[i].posicaoSaida && ctd.m_PathPosition < cameras[i + 1].posicaoSaida)
            {
                anim.SetTrigger(cameras[i+1].cameraAtual);
            }
            else if (ctd.m_PathPosition < cameras[i].posicaoSaida && ctd.m_PathPosition > cameras[i - 1].posicaoSaida)
            {
                anim.SetTrigger(cameras[i].cameraAtual);
            }
        }
    }
}
