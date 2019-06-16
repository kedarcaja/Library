using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private float distanceFromPlayer;
    private Rigidbody rb;
    Vector3 dir = Vector3.zero;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Move()
    {
        rb.velocity = dir * FindObjectOfType<PlayerScript>().Agent.speed;
    }
    public void GetInput()
    {
        dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { dir = Vector3.forward; }
        if (Input.GetKey(KeyCode.S)) { dir = -Vector3.forward; }
        if (Input.GetKey(KeyCode.A)) { dir = Vector3.left; }
        if (Input.GetKey(KeyCode.D)) { dir = Vector3.right; }
    }
    private void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {
        if(distanceFromPlayer>Vector3.Distance(transform.position,FindObjectOfType<PlayerScript>().transform.position))
        Move();
    }
}
