using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;

    public Transform endPosObject;

    public float period;
    public bool bSin = true;
    private float time = 0;

    private void Start()
    {
        startPos = transform.position;
        endPos = endPosObject.position;
        Destroy(endPosObject);
    }

    // Update is called once per frame
    void Update()
    {
        float posT;
        time += Time.deltaTime;

        if (bSin)
        {
            posT = (Mathf.Sin(time / period) + 1) / 2.0f;
        } else
        {
            posT = (time % (period * 2));
            if (posT > period)
                posT = period - (posT % period);
        }

        transform.position = Vector3.Lerp(startPos, endPos, posT);
    }
}