using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 offset;
    public float mouseScrollMultiplier = .4f;
    public float offsetMult;
    [Range(0f, 1f)]
    public float speedOfFollow = .8f;

    public float zoomMin= 1;
    public float zoomMax = 7;
    public float zoomSpeed = 10;
    private float currentZoom = 1;
    
    public GameObject cameraTarget;
    public float rotateSpeed = 5;

    private float vertLook = 0;
    public float minVertAngle = -160;
    public float maxVertAngle = -20;

    public Movement elephant;

    void Start()
    {
        offset = cameraTarget.transform.position - transform.position;
    }

    void LateUpdate()
    {
        // get the mouse inputs
        float horiLook = Input.GetAxis("Mouse X") * rotateSpeed;
        vertLook = Mathf.Clamp(vertLook + Input.GetAxis("Mouse Y") * rotateSpeed, minVertAngle, maxVertAngle);
        currentZoom = Mathf.Clamp(currentZoom - Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed, zoomMin, zoomMax);

        // rotate the camera
        transform.eulerAngles = new Vector3(-vertLook, transform.eulerAngles.y + horiLook, 0);

        transform.position = Vector3.Lerp(transform.position, cameraTarget.transform.position - (transform.rotation * offset * currentZoom), speedOfFollow);


    }

    public Vector3 GetForward()
    {
        return new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
    }
    public Vector3 GetRight()
    {
        return new Vector3(transform.right.x, 0, transform.right.z).normalized;
    }
}
