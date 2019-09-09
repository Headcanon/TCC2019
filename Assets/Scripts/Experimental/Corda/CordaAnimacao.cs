using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordaAnimacao : MonoBehaviour
{
    public float velocity, distance;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetFloat("BlendCorda", Mathf.Sin(Time.time * velocity) * distance);
    }
}
