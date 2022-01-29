using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrownObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Elephant")
        {
            // TODO : set knock back force
            GetComponent<Rigidbody>().AddForce(collision.GetContact(0).normal * 1000, ForceMode.VelocityChange);
        }
    }
}
