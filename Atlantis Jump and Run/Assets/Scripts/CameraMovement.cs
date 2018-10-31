using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private Rigidbody2D rig;
    public float cameraspeed;
    private Transform target;
    private int waypointcount = 0;
	// Use this for initialization
	void Start () {
        //rig = GetComponent<Rigidbody2D>();
        //rig.velocity = new Vector2(cameraspeed, 0);
        target = Waypoints.points[0];
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * cameraspeed * Time.deltaTime, Space.World);
        if(Vector2.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
	}
    void GetNextWaypoint()
    {
        waypointcount++;
        target = Waypoints.points[waypointcount];
    }
}
