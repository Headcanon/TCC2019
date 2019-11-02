using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    [Header("Vida&Morte")]
    public float vida = 100;
    public Transform spawnPoint;
    private Slider barraVida;

    private CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        barraVida = FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Atualiza a barra de vida
        if (barraVida == null)
        {
            barraVida = FindObjectOfType<Slider>();
        }
        else
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
            cc.enabled = false;
            this.transform.position = spawnPoint.position;
            cc.enabled = true;
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
