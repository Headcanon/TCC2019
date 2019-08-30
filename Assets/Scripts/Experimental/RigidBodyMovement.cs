using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    #region CharCtrl
    Rigidbody rb;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    #endregion

    public LayerMask Ground;

    public GameObject ashModel;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        anim = ashModel.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(rb.velocity.y) < .5)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = transform.right * horizontal;

            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        else
        {
            moveDirection = new Vector3(transform.right.x * horizontal, moveDirection.y, 0);

            moveDirection.x *= speed;
        }


        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        //moveDirection.y -= gravity * Time.deltaTime;

        //if ( Physics.BoxCast(transform.position, Vector3.down, Vector3.down))
        //{
        //    Debug.Log("air");
        //    moveDirection = transform.right * horizontal;

        //    moveDirection *= speed;

        //    if (Input.GetButton("Jump"))
        //    {
        //        moveDirection.y = jumpSpeed;
        //    }
        //}
        //else
        //{
        //    moveDirection = new Vector3(transform.right.x * Input.GetAxis("Horizontal"), moveDirection.y, 0);

        //    moveDirection.x *= speed;
        //    Debug.Log("air");
        //}


        ashModel.transform.rotation = Quaternion.Euler(0, 180 - 90 * horizontal, 0);
        if (anim != null)
        {
            anim.SetFloat("Vel", Mathf.Abs(horizontal));
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveDirection * Time.fixedDeltaTime);
    }

    //bool Grounded(Vector3 pos, float raio)
    //{
    //    bool isgrounded;
    //    isgrounded = Physics.OverlapSphere(pos, raio, Ground);
    //    return isgrounded;
    //}
}
