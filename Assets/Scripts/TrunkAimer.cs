using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkAimer : MonoBehaviour
{

    public Camera cam;
    public Transform elephantTrunkTip;
    public Transform elephantMinDist;
    public Transform elephantMaxDist;
    public Transform wreckingBall;
    public LineRenderer pseudoTrunk;

    private Vector3 mousePos = new Vector3();
    private Vector3 point = new Vector3();

    private Transform lastObjectHit;
    private float timeRemaining;
    public float timeToSet = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // get the mouse position
        mousePos = Input.mousePosition;
        // turn it into world space
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        // ASSUME NO COLLISION
        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, howFar(mousePos.y)));

        Vector3 origin = point;
        Vector3 dir = cam.transform.forward;
        float radius = 5.0f;
        // IF CLOSE TO COLLIDING WITH SOMETHING NEW, RESET OBJECT HIT AND TIME REMAINING
        if (Physics.SphereCast(cam.transform.position, radius, dir, out hit))
        {
            lastObjectHit = hit.transform;
            print("HIT: " + lastObjectHit.gameObject.name);
            timeRemaining = timeToSet;
        }
        // OVERWRITE THE POINT
        if (lastObjectHit != null && timeRemaining >= 0.0f)
        {
            point.z = lastObjectHit.position.z;
            point.y = lastObjectHit.position.y;
            timeRemaining -= Time.deltaTime;
        }

        // move the trunk of the elephant to that position.
        elephantTrunkTip.position = point;
        pseudoTrunk.SetPosition(0, pseudoTrunk.transform.position);
        pseudoTrunk.SetPosition(1, wreckingBall.transform.parent.position);
    }

    // returns a Z value scaled between the minDist and maxDist of the elephant's trunk
    private float howFar(float mouseY)
    {
        float frac = mouseY / Screen.height; // at the top of the screen, this is 1.0. At bottom, it's 0.0
        return elephantMaxDist.position.z * frac + elephantMinDist.position.z * (1 - frac);
    }
}
