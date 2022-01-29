using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up;
    public float rotationSpeed = 10;
    public bool bRotate = true;
    private float rot;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis, Time.deltaTime * rotationSpeed);
    }

    public void ToggleRotation()
    {
        bRotate = !bRotate;
    }
}
