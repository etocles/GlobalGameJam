using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(HingeJoint))]
public class SwingPoint : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Swing(Movement elephant)
    {
        print("SWING");
        GetComponent<HingeJoint>().connectedBody = elephant.GetComponent<Rigidbody>();
    }

    public void StopSwing(Movement elephant)
    {
        GetComponent<HingeJoint>().connectedBody = null;
    }
}
