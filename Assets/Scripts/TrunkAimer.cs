using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkAimer : MonoBehaviour
{

    public Camera cam;
    public Transform elephant;
    public Transform theRealBall;
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

    public bool jayTesting = true;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        fakeScreenPos.x = Screen.width / 2;
        fakeScreenPos.y = Screen.height / 2;
    }

    private Vector3 fakeScreenPos;

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetMouseButtonDown(2))
            fakeScreenPos = cam.WorldToScreenPoint(wreckingBall.position);

        if (Input.GetMouseButton(2))
        {
            fakeScreenPos.x = Mathf.Clamp(fakeScreenPos.x + Input.GetAxis("Mouse X") * 900 * Time.deltaTime, 0, Screen.width);
            fakeScreenPos.y = Mathf.Clamp(fakeScreenPos.y + Input.GetAxis("Mouse Y") * 900 * Time.deltaTime, 0, Screen.height);

            // get the mouse position
            mousePos = Input.mousePosition;
            // turn it into world space
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(fakeScreenPos);

            // IF CLOSE TO COLLIDING WITH SOMETHING NEW, RESET OBJECT HIT AND TIME REMAINING
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Default")))
            {
                Debug.DrawLine(cam.transform.position, hit.point, Color.red, 100);
                lastObjectHit = hit.transform;
                print("HIT: " + lastObjectHit.gameObject.name);
                timeRemaining = timeToSet;
                point = hit.point + ray.direction * 0.1f;
            }
            //// OVERWRITE THE POINT
            //if (lastObjectHit != null && timeRemaining >= 0.0f)
            //{
            //    point.z = lastObjectHit.position.z;
            //    point.y = lastObjectHit.position.y;
            //    timeRemaining -= Time.deltaTime;
            //}

            // move the trunk of the elephant to that position.
            elephantTrunkTip.position = point;
        }

        Vector3 runDir = (wreckingBall.position - theRealBall.position);
        if (runDir.sqrMagnitude < 1)
            runDir.Normalize();
        theRealBall.GetComponent<Rigidbody>().velocity = runDir * 30;


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
