using UnityEngine;

public class followCamera : MonoBehaviour {

    public bool sideScrolling;
    private float smoothSpeed =  0.125f;

    private Vector3 Offset;
    private Vector3 lookAtOffset;

    public Vector3 newOffset;
    public Vector3 newLookAtOffset;

    public Transform target;

    private void Start()
    {
        sideScrolling = true;

        Offset = newOffset;
        lookAtOffset = newLookAtOffset;
    }

    // Update is called once per frame
    void LateUpdate ()
    {
        Vector3 desiredPosition;

        Offset = Vector3.Lerp(Offset, newOffset, smoothSpeed);
        desiredPosition = target.position + Offset;      
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        lookAtOffset = Vector3.Lerp(lookAtOffset, newLookAtOffset, smoothSpeed);
        transform.LookAt(target.position + lookAtOffset);
	}
}
