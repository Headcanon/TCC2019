using UnityEngine;

public class followCamera : MonoBehaviour
{
    public bool sideScrolling;
    public float smoothSpeed = 0.125f;

    private Vector3 desiredOffset;
    private Vector3 desiredLookAtOffset;

    public Vector3 newOffset;
    public Vector3 newLookAtOffset;

    public Transform target;

    private void Start()
    {
        sideScrolling = true;

        desiredOffset = newOffset;
        desiredLookAtOffset = newLookAtOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            // Posição desejada
            Vector3 desiredPosition;

            // Interpola o offset atual desejado e o novo
            desiredOffset = Vector3.Lerp(desiredOffset, newOffset, smoothSpeed);
            // Descobre a possição desejada baseada na posição atual e no offset desejado
            desiredPosition = target.position + desiredOffset;
            // Interpola a posição atual e a posição desejada
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Interpola o lookt desejado atual e o novo
            desiredLookAtOffset = Vector3.Lerp(desiredLookAtOffset, newLookAtOffset, smoothSpeed);
            // Olha para o lookat desejado atual
            transform.LookAt(target.position + desiredLookAtOffset);
        }
    }

    public void TrocaOffset(Vector3 offset, Vector3 lookAtOffset)
    {
        //desiredOffset = newOffset;
        //desiredLookAtOffset = newLookAtOffset;

        newOffset = offset;
        newLookAtOffset = lookAtOffset;
    }
}