using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrownObject : MonoBehaviour
{
    private void Start()
    {
        gameObject.layer = LayerMask.GetMask("ThrownObject");
    }
}
