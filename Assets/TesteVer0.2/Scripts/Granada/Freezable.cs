using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezable : MonoBehaviour
{

    public GameObject freezedVersion;

    public void Freeze()
    {
        Instantiate(freezedVersion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
