
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : CharacterScript
{
	private Vector3 previousPosition, moveVector;
	public float curSpeed;
	public float rotateSpeed;
	private float magnditude = 0;
	private const float maxMagnitude = 1;

	Vector3 desiredDirection;

	private void Start()
	{
	}
	protected override void Update()
	{


		if (Input.GetKey(KeyCode.LeftShift))
		{
			agent.speed = 5f;
			anim.SetFloat("magnitudeSpeed", magnditude, 0.0f, Time.deltaTime);


		}
		else
		{
			agent.speed = 2.5f;
			anim.SetFloat("magnitudeSpeed", magnditude / 2, 0.0f, Time.deltaTime);
		}



		moveVector.x = Input.GetAxis("Horizontal");
		moveVector.z = Input.GetAxis("Vertical");

		Vector3 forward = Camera.main.transform.forward;
		Vector3 right = Camera.main.transform.right;
		forward.y = 0;
		right.y = 0;
		forward.Normalize();
		right.Normalize();

		magnditude = moveVector.sqrMagnitude < maxMagnitude ? moveVector.sqrMagnitude : maxMagnitude;

		desiredDirection = moveVector.z * forward + moveVector.x * right;


		agent.Move(desiredDirection*agent.speed * Time.deltaTime);

		if (moveVector != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredDirection), rotateSpeed);

		}

	}
}

