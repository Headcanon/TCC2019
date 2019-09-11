using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPack_Pipilson : MonoBehaviour
{
    // Script de movimento da Ash
    ChrCtrl_Pipilson ashCtrl;

    public ParticleSystem ps;

    #region Combustivel
    [Header("Combustivel")]
    // Combustível máximo
    public float combustivelMaximo = 100f;
    // Combustivel atual
    float combustivelAtual;

    // Barra para representar visualmente o combustível restante
    public Slider barraCombustivel;
    #endregion

    // Força da gravidade quando em estado de jetpack
    public float novaGravidade = 0.8f;    


    void Start()
    {
        // Pega o controle da Ash no mesmo GameObject
        ashCtrl = GetComponent<ChrCtrl_Pipilson>();

        // Começa com o combustível atual no máximo
        combustivelAtual = combustivelMaximo;
    }


    void Update()
    {
        // Se a Ash pular sendo que ela já deu o número máximo de pulos e o combustível é maior que zero
        if (Input.GetButton("Jump") && (ashCtrl.pulosDados == ashCtrl.puloLimite) && (combustivelAtual > 0.0f))
        {
            // Cancela o momento linear dela
            // Sem isso, se você ativa o jetpack no começo do pulo
            // O momento linear do pulo + a gravidade baixa do jetpack faz ela ir muito alto, como se fosse um moon jump.
            ashCtrl.moveDirection = Vector3.zero;
            // Substitui a gravidade da Ash, fazendo ela cair mais devagar
            ashCtrl.gravity = novaGravidade;
            // Diminui o combustível a cada frame
            combustivelAtual--;
            ps.Play();
        }

        // Se o combusível tiver acabado...
        if (combustivelAtual <= 0.0f)
        {
            ps.Stop();
            // Vota a gravidade ao normal
            ashCtrl.gravity = 20f;
            // Não deixa ser menor que zero
            combustivelAtual = 0.0f;

            //Eu deixei o esquema de pulo duplo, porém o segundo pulo automaticamente ativa o jetpack (quando possível), então se o jogador não tem mais combustível mudei o limite de pulos para 1. Ou o jogador poderia dar pulos duplos quando o combustivel acabasse.
            ashCtrl.puloLimite = 1;
        }
        else
        {
            // O segundo pulo ativa o jetpack. Então, se o jogador recupera combustivel volto o limite para 2, sendo possível novamente dar pulos duplos (que na verdade só ativa o jetpack).
            ashCtrl.puloLimite = 2;
        }

        if (Input.GetButtonUp("Jump") && (ashCtrl.pulosDados == ashCtrl.puloLimite))
        {
            ashCtrl.gravity = 20f;
        }

        // Se Ash está no chão e o combustível atual é menor do que o máximo...
        if (ashCtrl.noChao && combustivelAtual < combustivelMaximo)
        {
            // Aumenta o combustível atual a cada frame
            ps.Stop();
            combustivelAtual++;
        }

        // Atualiza a barra de combustível de acorda com a porcentagem de combustível
        barraCombustivel.value = combustivelAtual / combustivelMaximo;
    }
}