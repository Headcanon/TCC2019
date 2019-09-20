using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendCorda : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;

    public Transform pontoAlvo;
    public float blendSpeed = 0.1f;
    private float blendValue = 0f;

    private GanchoDeEscalada gde;

    private bool procurando = true;

    // ??? Não sei se vai funfar se desinstanciar e reinstanciar ???
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
            blendValue += blendSpeed;
        }

        if (blendValue > 1f && Input.GetButton("Jump"))
        {
            skinnedMeshRenderer.SetBlendShapeWeight(0, blendValue);
            blendValue -= blendSpeed;
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
