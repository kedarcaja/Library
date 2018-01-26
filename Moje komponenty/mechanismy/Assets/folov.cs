using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class folov : MonoBehaviour
{
    public Transform target;
    public float speed;
    void Update()
    {
        float step = speed * UnityEngine.Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
