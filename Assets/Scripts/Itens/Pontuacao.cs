using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{
    public int pontos = 0;
    public Text txtPts;

    public float tempoReducao = 0.1f;

    private void Start()
    {
        txtPts.text = pontos.ToString();
    }

    public void AlteraPontos(int pts)
    {        
        pontos += pts;

        txtPts.text = pontos.ToString();
    }


    public void Reduzir(int ptsReduzir)
    {
        StartCoroutine(Reduzindo(ptsReduzir));
    }

    IEnumerator Reduzindo(int ptsReduzir)
    {
        Debug.Log("chamou");
        for (int i = 0; i < ptsReduzir; i++)
        {
            pontos--;
            Debug.Log(pontos);
            txtPts.text = pontos.ToString();
            yield return new WaitForSeconds(tempoReducao);
        }
        Debug.Log("algo acontece depois disso?");
    }
}
