using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    public int ponto;
    public float forca = 10;

    public List<DistanceJoint3D> cordaDJs = new List<DistanceJoint3D>();
    public List<Rigidbody> cordaRBs = new List<Rigidbody>();
    // Start is called before the first frame update
    void Awake()
    {
        // Pra cada filho desse transform...
        for (int i = 0; i < transform.childCount; i++)
        {
            // Pega o filho pelo index
            // E pega o DistanceJoint3D dele
            DistanceJoint3D dj3d = transform.GetChild(i).GetComponent<DistanceJoint3D>();
            Rigidbody rb = transform.GetChild(i).GetComponent<Rigidbody>();
            if (dj3d != null)
            {
                cordaDJs.Add(dj3d);
                cordaRBs.Add(rb);
            }
        }
    }

    void FixedUpdate()
    {
        SetPonto();

        float horizontal = Input.GetAxis("Horizontal");

        for (int i = 0; i < ponto; i++)
        {
            cordaRBs[i].AddForce(new Vector3(horizontal * forca, 0, 0));
        }

        //for (int i = cordaDJs.Count; i > ponto; i--)
        //{
        //    cordaDJs[i].transform.parent = cordaDJs[ponto].transform;
        //}
    }

    private void SetPonto()
    {
        for (int i = 0; i < ponto; i++)
        {
            cordaDJs[i].penduradoEm = transform;
        }

        for (int i = ponto + 1; i <= cordaDJs.Count; i++)
        {
            cordaDJs[i].penduradoEm = cordaDJs[i -1].transform;
        }
    }
}
