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

    public Vector2[] forwardStopPoints;
    public Vector2[] backwardStopPoints;

    private bool bForward = true;
    private float cachedWait = 0;
    private int currentIndex;

    private void Start()
    {
        startPos = transform.position;
        endPos = endPosObject.position;
        Destroy(endPosObject.gameObject);
        time = 0;
        bForward = true;
    }

    private void HandleForwardChange(bool newForward, float posT)
    {
        print(newForward);
        if (newForward != bForward)
        {
            if (bForward)
            {
                if (currentIndex < forwardStopPoints.Length)
                    cachedWait += forwardStopPoints[currentIndex].y;
                currentIndex = 0;
            } else
            {
                if (currentIndex < backwardStopPoints.Length)
                    cachedWait += backwardStopPoints[currentIndex].y;
                currentIndex = 0;
            }
        }
        else // NO CHANGE CHECK FOR WAITS
        {
            if (bForward && currentIndex < forwardStopPoints.Length && forwardStopPoints[currentIndex].x > posT)
            {
                cachedWait += forwardStopPoints[currentIndex].y;
                currentIndex++;
            }
            if ((!bForward) && currentIndex < backwardStopPoints.Length && backwardStopPoints[currentIndex].x < posT)
            {
                cachedWait += forwardStopPoints[currentIndex].y;
                currentIndex++;
            }
        }

        bForward = newForward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float timePour = Time.deltaTime;
        if (cachedWait > 0)
        {
            cachedWait = Mathf.Max(0, cachedWait - timePour);
            timePour = Mathf.Max(0, timePour - cachedWait);
        }

        float posT;
        time += timePour;

        if (bSin)
        {
            bool newForward = ((time / period) % 2 * Mathf.PI) < Mathf.PI;
            posT = (Mathf.Sin(time / period) + 1) / 2.0f;
            HandleForwardChange(newForward, posT);
        }
        else
        {
            posT = (time % (period * 2));
            bool newForward = posT > period;
            if (newForward)
            {
                posT = period - (posT % period);
            }
            posT /= period;
            HandleForwardChange(newForward, posT);
        }

        transform.position = Vector3.Lerp(startPos, endPos, posT);
    }
}