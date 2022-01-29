using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    public BoxCollider bc;
    public float speed = 1f;
    public float speedAir = 1f;

    public float velocityCap;

    public LayerMask GroundLayer;

    public bool grounded;

    public bool triedJump;
    public float jumpForce = 3;
    public Vector3 movementVector;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }


    private void Update()
    {
        ReadInputs();



    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (Physics.OverlapBox(transform.position - new Vector3(0, .2f, 0), bc.size/2, Quaternion.Euler(0f, 0f, 0f), GroundLayer).Length != 0)
        {
            grounded = true;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rb.constraints = RigidbodyConstraints.FreezeRotation;

        }
        else
        {
            grounded = false;

            rb.constraints = RigidbodyConstraints.FreezeRotationY & RigidbodyConstraints.FreezeRotationZ;
        }

        AddVelocity();

        if (triedJump)
        {
            triedJump = false;
            if (grounded)
            {
                Vector3 newvel = rb.velocity;
                newvel.y = jumpForce;
                rb.velocity = newvel;
            }
        }
        
    }


    public void ReadInputs()
    {


        if (Input.GetKey(KeyCode.W))
        {
            movementVector.z = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementVector.z = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x = - 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x = 1;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            triedJump = true;
        }
        

        
    }


    public void AddVelocity()
    {
        
        movementVector = movementVector.normalized * (grounded ? speed : speedAir);

        if ((movementVector.z > 0 && rb.velocity.z > velocityCap) || (movementVector.z < 0 && rb.velocity.z < -velocityCap))
        {
            movementVector.z = 0;
        }

        if ((movementVector.x > 0 && rb.velocity.x > velocityCap) || (movementVector.x < 0 && rb.velocity.x < -velocityCap))
        {
            movementVector.x = 0;
        }


        rb.AddForce(movementVector, ForceMode.Impulse);
        print(movementVector);


        movementVector = Vector3.zero;
    }

}


