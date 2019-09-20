using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendCorda : MonoBehaviour
{
    // ???
    int blendShapeCount;
    // ???
    SkinnedMeshRenderer skinnedMeshRenderer;
    // Malha
    Mesh skinnedMesh;
    // ???
    float blendOne = 0f;
    float blendTwo = 0f;
    float blendSpeed = 1f;
    // ???
    bool blendOneFinished;

    private void Awake()
    {
        // skinning???
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Quantidade de blend shapes???
        blendShapeCount = skinnedMesh.blendShapeCount;
    }

    // Update is called once per frame
    void Update()
    {
            if (blendOne < 100f)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(0, blendOne);
                blendOne += blendSpeed;
            }
            else
            {
                blendOneFinished = true;
            }
    }
}
