using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyFollowCamera : MonoBehaviour
{
    public bool sideScrolling;
    public float smoothSpeed = 0.125f;

    private Vector3 Offset;
    private Vector3 lookAtOffset;

    public Vector3 newOffset;
    public Vector3 newLookAtOffset;

    public Transform target;

    Vector3 desiredPosition;

    private void Start()
    {
        sideScrolling = true;

        Offset = newOffset;
        lookAtOffset = newLookAtOffset;
    }

    private void Update()
    {

        lookAtOffset = Vector3.Lerp(lookAtOffset, newLookAtOffset, smoothSpeed);

        Offset = Vector3.Lerp(Offset, newOffset, smoothSpeed);
        desiredPosition = target.position + Offset;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {

            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.LookAt(target.position + lookAtOffset);
        }
    }
}
