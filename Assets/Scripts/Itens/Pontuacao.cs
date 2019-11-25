using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{
    public int pontos = 0;
    public Text txtPts;

    public float tempoReducao;
    private int ptsReduzidos;

    private void Start()
    {
        txtPts.text = pontos.ToString();
    }

    public void AlteraPontos(int pts)
    {        
        pontos += pts;
        //Debug.Log("Pontuacao" + pts);

        txtPts.text = pontos.ToString();
    }


    public void Reduzir(int ptsReduzir)
    {
        ptsReduzidos = pontos -= ptsReduzir;
        StartCoroutine(Reduzindo(ptsReduzir));
    }

    IEnumerator Reduzindo(int ptsReduzir)
    {
        for (int i = ptsReduzir; i > ptsReduzidos; i--)
        {
            pontos--;
            txtPts.text = pontos.ToString();

            yield return new WaitForSeconds(tempoReducao);
        }
    }
}
