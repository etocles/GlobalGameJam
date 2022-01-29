using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject elephant;
    public Vector3 offset;
    public float mouseScrollMultiplier = .4f;
    public float offsetMult;
    [Range(0f, 1f)]
    public float speedOfFollow = .8f;

    public float zoomMin= 1;

    public float zoomMax = 7;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        offsetMult = Mathf.Clamp(offsetMult + (Input.mouseScrollDelta.y * mouseScrollMultiplier), zoomMin, zoomMax);


        transform.position = Vector3.Lerp(elephant.transform.position, elephant.transform.position + (offset * offsetMult), speedOfFollow);

        
    }
}
