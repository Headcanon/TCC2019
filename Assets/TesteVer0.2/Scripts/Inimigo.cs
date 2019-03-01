using UnityEngine;

public class Inimigo : MonoBehaviour
{
    Movimento axel;
    float nextFire;

    public GameObject projetil;

    public bool atirar;
    public float fireRate;

    public bool ddd;
    public float minDistancia;
    public float velocidade;

    private void Start()
    {
        nextFire = Time.time;
        axel = FindObjectOfType<Movimento>();
    }

    private void Update()
    {
        if (atirar)
        {
            CheckIfTimeToFire();
        }

        if (Vector3.Distance(transform.position, axel.transform.position) < minDistancia && Vector3.Distance(transform.position, axel.transform.position) > 8)
        {
            if (ddd)
            {
                transform.position = Vector3.MoveTowards(transform.position, axel.transform.position, velocidade * Time.deltaTime);
            }

        }

    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            Instantiate(projetil, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
