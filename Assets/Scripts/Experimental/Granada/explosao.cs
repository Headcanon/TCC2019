using UnityEngine;

public class Explosao : MonoBehaviour {

    //Enum definida no script Public Enums
    //Define o tipo de efeito da granada (gelo, destruir, etc.)
    public TipoGranada tg;

    //Tempo entre o spawn da granada e a sua explosão
    public float delay = 3f;
    //Raio da distância de efeito da granada
    public float raio = 7f;

    //Trava pra explosão
    bool explodiu = false;
    //Número para a contagem do cronometro
    float countdown;

    #region Pavio
    // Use this for initialization
    void Start ()
    {
        //Inicia a contagem do cronometro
        countdown = delay;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Contagem do cronometro
        countdown -= Time.deltaTime;
        if (countdown <= 0f && explodiu == false)
        {
            Explode();
        }
	}
    #endregion

    void Explode()
    {
        Debug.Log("boom!");

        //Cria uma esfera ao redor da granada usando a variável "raio"
        //Então guarda em um array todos os objetos com colliders que estiverem no raio da esfera
        Collider[] colliders = Physics.OverlapSphere(transform.position, raio);

        //Procura por objetos do tipo Explodable dentro daquela array
        foreach (Collider objetoProximo in colliders)
        {
            Explodable expl = objetoProximo.GetComponent<Explodable>();

            //Se encontrar um objeto do tipo Explodable com o mesmo TipoGranada executa o efeito deste TipoGranada
            if (expl != null && expl.tg == tg)
            {
                switch (tg)
                {
                    case TipoGranada.DESTROY:
                        expl.Destroy();                        
                        break;
                    case TipoGranada.GELO:
                        expl.Freeze();
                        break;
                }
            }          
        }
        
        //Explode a granada
        Destroy(gameObject);
        explodiu = true;
    }
}