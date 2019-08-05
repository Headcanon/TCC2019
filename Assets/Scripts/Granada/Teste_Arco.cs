using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Teste_Arco : MonoBehaviour
{
    //Linha para o arco
    LineRenderer lr;

    //Velocidade do lançamento
    public float velocidade;
    //Angulo do lançamento
    public float angulo;
    //Quantidade de vértices da linha
    public int resolucao = 10;

    float gravidade;
    float radAngulo;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        gravidade = Mathf.Abs(Physics.gravity.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        RenderArco();
    }

    void RenderArco()
    {
        lr.positionCount = resolucao + 1;
        lr.SetPositions(CalculateArcArray());
    }

    // Cria o array de posições para cada vértice da linha
    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolucao + 1];

        // Converte o angulo fornecido de graus pra radianos
        radAngulo = Mathf.Deg2Rad * angulo;
        // Mágica da física q descobre a distância máxima do arco
        float maxDistance = (velocidade * velocidade * Mathf.Sin(2 * radAngulo)) / gravidade;

        for(int i = 0; i <= resolucao; i++)
        {
            // Porcentagem de progresso desse for
            float prog = (float) i / resolucao;
            arcArray[i] = CalculateArcPoint(prog, maxDistance);
        }

        return arcArray;
    }


    // Calcula a distância e altura de cada ponto individual
    Vector3 CalculateArcPoint(float p, float md)
    {
        // Multiplica a porcentegem de progresso do for pela distância máxima
        // Isso serve para encontrar a distância do ponto
        float x = p * md;

        // Magia da física que usa o ângulo, a velocidade e a distância do ponto determinado para descobrir a sua altura
        float y = x * Mathf.Tan(radAngulo) - ((gravidade * x * x) / (2 * velocidade * velocidade * Mathf.Cos(radAngulo) * Mathf.Cos(radAngulo)));

        // Retorna o ponto resultante
        return new Vector3(x, y);
    }
}
