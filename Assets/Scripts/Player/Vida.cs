using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    [Header("Vida&Morte")]
    public float vida = 100;
    public Slider barraVida;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Atualiza a barra de vida
        if (barraVida != null)
        {
            barraVida.value = vida / 100;
        }
    }

    // Reduz a vida e mata player quando chega a 0
    public void Damage(float dano)
    {
        vida -= dano;
        if (vida > 0 && spawnPoint != null)
        {
            this.transform.position = spawnPoint.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Da um GameOver quando destroi Player
    private void OnDestroy()
    {
        if (vida <= 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
