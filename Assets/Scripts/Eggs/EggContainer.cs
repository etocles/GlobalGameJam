using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EggContainer : MonoBehaviour
{
    public Transform eggPullPosition;
    [Header("EGG RETENTION PARAMS")]
    public float eggPullForce;
    public float eggVelocityLerpAmount = 0.12f;
    [Space]
    public float eggLossAngle = 50.0f;
    public Transform eggLossPosition;
    public float eggLossRadius;
    public float eggLossForceMin = 1000;
    public float eggLossForceMax = 2000;
    [Space]
    public GameObject invisibleTop;
    [Space]

    private HashSet<FragileEgg> eggs = new HashSet<FragileEgg>();

    public Movement holder;
    private bool bFrozen = false;

    private void Start()
    {
        
    }

    public HashSet<FragileEgg> GetEggs()
    {
        return new HashSet<FragileEgg>(eggs);
    }

    public void FreezeEggs()
    {
        bFrozen = true;
    }

    public void LoseEggsWithTeleport(int numEggs)
    {
        int i = 0;
        foreach (FragileEgg egg in eggs)
        {
            eggs.Remove(egg);
            if (i >= numEggs)
                break;
            egg.transform.position = eggLossPosition.position + (Random.insideUnitSphere * eggLossRadius);
            egg.transform.rotation = Random.rotation;
            egg.GetComponent<Rigidbody>().AddForce(RandomEggVector() * Random.Range(eggLossForceMin, eggLossForceMax), ForceMode.VelocityChange);
        }
    }
    


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Elephant")
        {

        }
    }
    private void FixedUpdate()
    {

        PullEggs(1200 * Time.fixedDeltaTime);
        if (holder != null)
        {
            GetComponent<Rigidbody>().velocity = (holder.eggHoldPosition.position - transform.position) * 20;
            GetComponent<Rigidbody>().velocity += Vector3.up * holder.GetComponent<Rigidbody>().velocity.y;

            foreach (FragileEgg e in eggs)
            {
                e.GetComponent<Rigidbody>().velocity = Vector3.Lerp(e.GetComponent<Rigidbody>().velocity, GetComponent<Rigidbody>().velocity, eggVelocityLerpAmount);
            }
        }
    }

    private void Update()
    {

        //PullEggs(1200 * Time.deltaTime);
        if (holder != null)
        {
            GetComponent<Rigidbody>().velocity = (holder.eggHoldPosition.position - transform.position) * 20;
            // transform.position = new Vector3(transform.position.x, holder.eggHoldPosition.position.y, transform.position.z);
            //GetComponent<Rigidbody>().velocity += Vector3.up * holder.GetComponent<Rigidbody>().velocity;
        }
    }

    private void LateUpdate()
    {
        //PullEggs(1200 * Time.deltaTime);
        if (holder != null)
        {
            GetComponent<Rigidbody>().velocity = (holder.eggHoldPosition.position - transform.position) * 20;
            //transform.position = new Vector3(transform.position.x, holder.eggHoldPosition.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bFrozen) return;

        if (other.GetComponent<FragileEgg>() != null)
        {
            print(other.gameObject.name);
            other.GetComponent<FragileEgg>().SetContainer(this);
            eggs.Add(other.GetComponent<FragileEgg>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (bFrozen) return;

        if (other.GetComponent<FragileEgg>() != null)
        {
            other.GetComponent<FragileEgg>().SetContainer(null);
            eggs.Remove(other.GetComponent<FragileEgg>());
        }
    }

    public int GetEggCount()
    {
        return eggs.Count;
    }



    /**
     * PULL EGGS TOWARDS THE CENTER, COULD OFFSET SOME OF THE CRAZY PHYSICS, GIVE IT A LITTLE BIT OF A MORE STUCK MAGNET FEEL?
     */ 
    private void PullEggs(float forceAmount)
    {
        foreach (FragileEgg egg in eggs)
        {
            egg.GetComponent<Rigidbody>().AddForce(
                (eggPullPosition.position - egg.transform.position).normalized * forceAmount, 
                ForceMode.Acceleration);
        }
    }

    public void SetInvisibleTopEnabled(bool enabled)
    {
        invisibleTop.SetActive(enabled);
    }


    Vector3 RandomEggVector()
    {
        float radius = Mathf.Tan(Mathf.Deg2Rad * eggLossAngle / 2);
        Vector2 circle = Random.insideUnitCircle * radius;
        Vector3 target = (transform.position + transform.forward) + (transform.rotation * new Vector3(circle.x, circle.y));
        return (target - transform.position).normalized;
    }

    public void DetachFromElephant()
    {
        holder = null;
        transform.parent = null;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void AttachToElephant(Movement elephant)
    {
        holder = elephant;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void SetNewPosition(Transform newTransform)
    {
        foreach (FragileEgg egg in eggs)
        {
            egg.transform.parent = transform;
        }
        transform.position = newTransform.position;
        transform.rotation = newTransform.rotation;

        foreach (FragileEgg egg in eggs)
        {
            egg.transform.parent = null;
        }
    }
}
