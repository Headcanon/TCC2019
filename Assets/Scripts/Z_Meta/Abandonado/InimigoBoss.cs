using UnityEngine;

public class InimigoBoss : MonoBehaviour
{
    #region Parametros
    Transform axel;
    float nextFire;

    // Prefab do projétil que será lançado
    public GameObject projetil;

    #region Tiro
    public bool atirar;
    public float fireRate;
    #endregion

    #region Movimento3D
    public bool ddd;
    public float minDistancia;
    public float velocidade;
    #endregion
    #endregion

    /* Esse script é uma junção dos scripts Inimigo e IniProjetil do commit número 15 (Coins - L)
    Não foi testado ainda. Se houver algum erro que não puder resolver volte ao commit 15 e recupere esses scripts */

    #region Lógica
    private void Start()
    {
        nextFire = Time.time;
        axel = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (atirar)
        {
            CheckIfTimeToFire();
        }

        // Verifica se está na distância ideal entre o Boss e a Axel
        if (Vector3.Distance(transform.position, axel.transform.position) < minDistancia && Vector3.Distance(transform.position, axel.transform.position) > 8)
        {
            // Se for 3D ele anda na direção da Axel
            if (ddd)
            {
                transform.position = Vector3.MoveTowards(transform.position, axel.transform.position, velocidade * Time.deltaTime);
            }

        }

    }

    // Checa e atira
    void CheckIfTimeToFire()
    {
        // Vê se já deu a hora de atirar
        if(Time.time > nextFire)
        {
            // Define os parametros para o projétil seguir
            Rigidbody rb = projetil.GetComponent<Rigidbody>();
            Vector3 moveDirection = (axel.transform.position - transform.position).normalized * 7f;

            // Instancia o prefab do projétil na cena
            Instantiate(projetil, transform.position, Quaternion.identity);

            // Define a velocidade do projetil
            rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

            // Manda o projetil ignorar colisão consigo mesmo e com o Boss para não haver autodestruição
            Physics.IgnoreCollision(projetil.GetComponent<Collider>(), GetComponent<Collider>());

            Destroy(projetil.gameObject, 6f);

            // Reinicia o timer
            nextFire = Time.time + fireRate;
        }
    }

    // Morte do boss
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    #endregion
}

