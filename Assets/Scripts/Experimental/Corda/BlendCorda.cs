using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendCorda : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;

    public Transform pontoAlvo;
    public float launchSpeed = 5f;
    public float pullSpeed = 2.5f;
    private float blendValue = 0f;

    private GanchoDeEscalada gde;

    private bool procurando = true;

    private void Awake()
    {
        skinnedMeshRenderer = transform.parent.GetComponent<SkinnedMeshRenderer>();

        gde = GameObject.FindGameObjectWithTag("Player").GetComponent<GanchoDeEscalada>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(skinnedMeshRenderer.transform.position, pontoAlvo.position, blendValue / 100);

        if (blendValue < 100f && procurando)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(0, blendValue);
            blendValue += launchSpeed * Time.deltaTime;
        }

        if (blendValue > 1f && Input.GetButton("Fire1"))
        {
            skinnedMeshRenderer.SetBlendShapeWeight(0, blendValue);
            blendValue -= pullSpeed * Time.deltaTime;
        }

        if(blendValue >= 100 && !gde.jointado)
        {
            gde.Abortar();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ancora"))
        {
            gde.conectadoEm = other.transform;
            gde.jointado = true;
            procurando = false;
        }
        else if (other.CompareTag("Puxavel"))
        {

        }
    }
}
