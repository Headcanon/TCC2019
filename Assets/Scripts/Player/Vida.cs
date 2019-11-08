using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    [Header("Vida&Morte")]
    // Vida atual de player
    public float vida = 100;
    // Local pra renascer
    public Transform spawnPoint;
    // Tempo pro respawn
    public float spawnTime = 2f;

    // Slider que representa a vida
    private Slider barraVida;

    #region Slider
    // Update is called once per frame
    void Update()
    {
        // Caso não haja uma barra de vida indicada...
        if (barraVida == null)
        {
            // Encontra o slider da barra
            barraVida = FindObjectOfType<Slider>();
        }
        // Caso já haja um slider...
        else
        {
            // Atualiza a barra de vida
            barraVida.value = vida / 100;
        }
    }
    #endregion

    // Reduz a vida e mata player quando chega a 0
    public void Damage(float dano)
    {
        // Diminui a vida atuaç
        vida -= dano;

        // Se a vida ainda for maior que zero e houver um spawnpoint indicado...
        if (vida > 0 && spawnPoint != null)
        {
            // Desativa o Game Object
            gameObject.SetActive(false);
            // Espera o tempo indicado para ativar o respawn
            Invoke("Respawn", spawnTime);
        }
        // Caso contrário...
        else
        {
            // Destrói player
            Destroy(gameObject);
        }
    }

    // Da um GameOver quando destroi Player
    private void OnDestroy()
    {
        if (vida <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    private void Respawn()
    {
        // Transporta player pro checkpoint
        transform.position = spawnPoint.position;
        // Reativa o Game Object
        gameObject.SetActive(true);
    }
}
