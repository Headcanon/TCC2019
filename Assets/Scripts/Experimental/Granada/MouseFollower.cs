using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    // Retorna uma rotação de acordo com a posição do mouse e um ponto de referência
    public Quaternion AnguloRotacao(Transform obj)
    {
        // Posição do mouse
        Vector2 mousePos = Input.mousePosition - obj.transform.position;

        // Cria um anglo apontado pra posição do mouse
        float angulo = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        Quaternion rotacao = Quaternion.Euler(0, 0, angulo);
        
        return rotacao;
    }


    #region SegueSeta
    // Método que faz imagens do canvas girarem seguindo o ponteiro do mouse
    public void SegueGiroCanvas(Transform obj, Transform pos)
    {
        // Coloca a seta na posição da personagem relativa à câmera
        obj.position = Camera.main.WorldToScreenPoint(pos.position);
        
        // Rotaciona o objeto
        Quaternion rotacao = AnguloRotacao(obj);
        obj.rotation = rotacao;
        pos.rotation = rotacao;
    }    
    #endregion


    #region SegueArco
    // Renderiza o Arco
    public void RenderArco(LineRenderer lr, int resolucao, float angulo, float velocidade, float gravidade, Transform origem)
    {
        lr.positionCount = resolucao + 1;
        lr.SetPositions(CalculateArcArray(resolucao, angulo, velocidade, gravidade, origem));
    }


    // Cria o array de posições para cada vértice da linha
    Vector3[] CalculateArcArray(int resolucao, float angulo, float velocidade, float gravidade, Transform origem)
    {
        Vector3[] arcArray = new Vector3[resolucao + 1];

        // Converte o angulo fornecido de graus pra radianos
        float radAngulo = Mathf.Deg2Rad * angulo;
        // Mágica da física q descobre a distância máxima do arco
        float maxDistance = (velocidade * velocidade * Mathf.Sin(2 * radAngulo)) / gravidade;

        for (int i = 0; i <= resolucao; i++)
        {
            // Porcentagem de progresso desse for
            float prog = (float)i / resolucao;
            arcArray[i] = CalculateArcPoint(prog, maxDistance, radAngulo, velocidade, gravidade) + origem.position;
        }

        return arcArray;
    }


    // Calcula a distância e altura de cada ponto individual
    Vector3 CalculateArcPoint(float p, float md, float radAngulo, float velocidade, float gravidade)
    {
        // Multiplica a porcentegem de progresso do for pela distância máxima
        // Isso serve para encontrar a distância do ponto
        float x = p * md;

        // Magia da física que usa o ângulo, a velocidade e a distância do ponto determinado para descobrir a sua altura
        float y = x * Mathf.Tan(radAngulo) - ((gravidade * x * x) / (2 * velocidade * velocidade * Mathf.Cos(radAngulo) * Mathf.Cos(radAngulo)));

        //y -= transform.position.y;

        // Retorna o ponto resultante
        return new Vector3(x, y);
    }
    #endregion 
}