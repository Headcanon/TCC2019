using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour
{
    public TipoGranada tg;

    #region Destroy
    public void Destroy()
    {
        //Destroy esse prefab
        Destroy(gameObject);
    }
    #endregion

    #region Freeze
    //Prefab para a versão congelada
    public GameObject freezedVersion;

    public void Freeze()
    {
        //Coloca um prefab congelado no lugar desse
        Instantiate(freezedVersion, transform.position, transform.rotation);
        
        //Destroy esse prefab
        Destroy(gameObject);
    }
    #endregion
}