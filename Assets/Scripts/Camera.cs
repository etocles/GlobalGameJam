using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject elephant;
    public Vector3 offset;
    [Range(0f, 1f)]
    public float speedOfFollow = .8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(elephant.transform.position, elephant.transform.position + offset, speedOfFollow);
    }
}
