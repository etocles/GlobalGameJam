using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    public Animator anim;
    public Transform cameraStartPos;
    private BoxCollider bc;
    public float speed = 1f;
    public float speedAir = 1f;

    public float velocityCap;
    
    public LayerMask GroundLayer;

    public bool grounded;

    private bool triedJump;
    public float jumpForce = 3;
    private Vector3 movementVector;

    public PlayerCamera pCam;
    public float rotSpeed;

    public EggContainer eggContainer;
    public float throwForce = 1000;

    [Space]
    public Transform eggHoldPosition;
    public Transform eggDropPosition;

    public float eggPickupRange;
    [Space]
    public float turnVelMax = 10;
    [Range(0, 1)]
    public float slideDropSpeed = 0.91f;
    public float downwardForce = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();

        Invoke("PickupCrate", 0.05f);
    }

    private void PickupCrate()
    {
        eggContainer.holder = this;
        eggContainer.SetNewPosition(eggHoldPosition);
        eggContainer.AttachToElephant(this);
    }

    private void CheckContainerRange()
    {
    }

    public Vector3 RequestCameraStartOffset()
    {
        if (cameraStartPos != null)
            return transform.position - cameraStartPos.position;
        else
            return - new Vector3(-1.29827881f, 1.24824834f, -6.01367331f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckContainerRange();
        ReadInputs();

        rb.velocity -= (Vector3.up * downwardForce * Time.deltaTime);
        print("VEL: " + rb.velocity);

        if (Physics.CheckBox(bc.bounds.center, bc.bounds.extents, transform.rotation, GroundLayer))
        {
            grounded = true;
            // transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        }
        else
        {
            grounded = false;

            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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
            } /*else if (swinger == null)
            {
                swinger = GetClosestSwingPoint();
                swinger?.Swing(this);
            } else
            {
                swinger.StopSwing(this);
                swinger = null;
            }*/
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

        anim.SetBool("IsWalking", movementVector.sqrMagnitude > 0.1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            triedJump = true;
        }

        if (Input.GetMouseButtonDown(0) && !PauseCanvasScript.isPaused)
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

        if (movementVector.sqrMagnitude > 0.01)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(movementVector.x, 0, movementVector.z).normalized, Vector3.up), 20f * Time.deltaTime);
        else if (grounded)
            rb.velocity = (new Vector3(rb.velocity.x, 0, rb.velocity.z) * slideDropSpeed) + (Vector3.up * rb.velocity.y);

        if (movementVector.sqrMagnitude > 0.01 && rb.velocity.sqrMagnitude < turnVelMax && Vector3.Dot(rb.velocity, movementVector.normalized) < 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        if ((movementVector.z > 0 && rb.velocity.z > velocityCap) || (movementVector.z < 0 && rb.velocity.z < -velocityCap))
        {
            movementVector.z = 0;
        }

        if ((movementVector.x > 0 && rb.velocity.x > velocityCap) || (movementVector.x < 0 && rb.velocity.x < -velocityCap))
        {
            movementVector.x = 0;
        }


        rb.AddForce(movementVector * Time.deltaTime , ForceMode.Impulse);

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
        // eggContainer.transform.parent = eggHoldPosition;

        eggContainer.AttachToElephant(this);
    }

    public void PutDownContainer()
    {
        eggContainer.SetNewPosition(eggDropPosition);
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


