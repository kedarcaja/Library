using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigation : MonoBehaviour
{

    public List<Transform> waypoints;
    private Transform currentWaypoint;
    public float speed = 5f;
    private float closeEnouth = 0.5f;
    int point = 0;

    void Start()
    {
        currentWaypoint = waypoints[point];
    }
    // Update is called once per frame
    void Update()
    {
        /*Quaternion rotation = Quaternion.LookRotation(waypoints[point].position - transform.position, transform.TransformDirection(Vector3.forward));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);*/
       float dist = Vector3.Distance(waypoints[point].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[point].position, UnityEngine.Time.deltaTime * speed);
        if (Vector3.Distance(this.transform.position, waypoints[point].position) < closeEnouth)
        {
            if (point + 1 < waypoints.Count)
                point++;

        }
    }
}