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

    public PlayerCamera pCam;
    public float rotSpeed;

    public EggContainer eggContainer;
    public float throwForce = 1000;

    [Space]
    public Transform eggHoldPosition;
    public Transform eggDropPosition;

    public float eggPickupRange;


    private LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        lr = GetComponent<LineRenderer>();

        PickUpContainer();
    }

    private void CheckContainerRange()
    {
        lr.enabled = false;
        if (eggContainer.holder == this)
            return;

        if ((eggContainer.transform.position - transform.position).sqrMagnitude < eggPickupRange * eggPickupRange)
        {
            lr.enabled = true;
            lr.SetPositions(new Vector3[] { transform.position, eggContainer.transform.position });
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckContainerRange();
        ReadInputs();

        if (Physics.OverlapBox(transform.position - new Vector3(0, .2f, 0), bc.size/2, Quaternion.Euler(0f, 0f, 0f), GroundLayer).Length != 0)
        {
            grounded = true;
            // transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rb.constraints = RigidbodyConstraints.FreezeRotationX & RigidbodyConstraints.FreezeRotationZ;

        }
        else
        {
            grounded = false;

            rb.constraints = RigidbodyConstraints.FreezeRotationX & RigidbodyConstraints.FreezeRotationZ;
        }
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        AddVelocity();

        if (triedJump)
        {
            triedJump = false;
            if (grounded)
            {
                Vector3 newvel = rb.velocity;
                newvel.y = jumpForce;
                rb.velocity = newvel;
            } else
            {
                SwingPoint closest = GetClosestSwingPoint();
                if (closest != null) { 
}
            }
        }
        
    }

    private SwingPoint GetClosestSwingPoint()
    {
        return null;
    }

    public void ReadInputs()
    {

        movementVector += Input.GetAxisRaw("Horizontal") * pCam.GetRight();
        movementVector += Input.GetAxisRaw("Vertical") * pCam.GetForward();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            triedJump = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (eggContainer.holder == this)
                PutDownContainer();
            else
                PickUpContainer();
        }

        if (Input.GetMouseButtonDown(1))
            ThrowContainer();
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


        rb.AddForce(movementVector * Time.deltaTime , ForceMode.Impulse);

        // Rotate the forward vector towards the target direction by one step
        // Vector3 newDirection = Vector2.RotateTowards(transform.forward, movementVector, rotSpeed * Time.deltaTime, 0.0f);
        // Calculate a rotation a step closer to the target and applies rotation to this object
        if (movementVector.sqrMagnitude > 0.01)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized, Vector3.up), 5f * Time.deltaTime);

        print(movementVector);


        movementVector = Vector3.zero;
    }

    public void PickUpContainer()
    {
        // NO pickup if not in range
        if ((eggContainer.transform.position - transform.position).sqrMagnitude > eggPickupRange * eggPickupRange)
            return;

        eggContainer.holder = this;
        eggContainer.SetNewPosition(eggHoldPosition);
        eggContainer.transform.parent = eggHoldPosition;

        eggContainer.AttachToElephant(this);
    }

    public void PutDownContainer()
    {
        eggContainer.DetachFromElephant();
    }

    public void ThrowContainer()
    {
        if (eggContainer.holder != this)
            return;
        Vector3 projectDir =(transform.forward + transform.up).normalized;
        eggContainer.DetachFromElephant();
        eggContainer.GetComponent<Rigidbody>().AddForce(projectDir * throwForce, ForceMode.VelocityChange);
    }
}


