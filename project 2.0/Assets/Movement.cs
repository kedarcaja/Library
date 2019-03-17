using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
	private Rigidbody rb;
	[SerializeField]
	private float speed, jumpHeight;
	private Vector3 direction = Vector3.zero;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Move();
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
	}
	void Update()
	{
		
		GetInput();
	}
	public void Move()
	{
		rb.velocity = direction * 20*(speed * Time.deltaTime);
	}
	public void GetInput()
	{
		direction = Vector3.zero;
		if (Input.GetKey(KeyCode.W))
		{
			direction = Vector3.forward;
			transform.eulerAngles = new Vector3(0, 0, 0);
		}
		if (Input.GetKey(KeyCode.S))
		{
			direction = Vector3.back;
			transform.eulerAngles = new Vector3(0, 180, 0);

		}
		if (Input.GetKey(KeyCode.A))
		{
			direction = Vector3.left;
			transform.eulerAngles = new Vector3(0, -90, 0);
		}
		if (Input.GetKey(KeyCode.D))
		{
			direction = Vector3.right;
			transform.eulerAngles = new Vector3(0, 90, 0);

		}
	}
	public void Jump()
	{
       
		rb.AddForce(transform.TransformDirection(Vector3.up) * (100*jumpHeight));
	}

}
