
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : CharacterScript
{
	public float rotateSpeed;

	Vector3 desiredDirection;

	private float maxSpeed = 5;
	private float minSpeed = 2.5f;
	protected override void Update()
	{

		anim.SetFloat("magnitudeSpeed", agent.velocity.magnitude, 0.0f, Time.deltaTime);

		if (Input.GetKey(KeyCode.LeftShift))
		{
			agent.speed = Mathf.Lerp(agent.speed, maxSpeed, Time.deltaTime);


		}
		else
		{
			if (agent.speed > minSpeed && agent.velocity != Vector3.zero)
			{
			agent.speed = 	Mathf.Lerp(agent.speed, minSpeed, Time.deltaTime);
			}

		}



		float inputX = Input.GetAxis("Horizontal");
		float inputZ = Input.GetAxis("Vertical");

		Vector3 forward = Camera.main.transform.forward;
		Vector3 right = Camera.main.transform.right;
		forward.y = 0;
		right.y = 0;
		forward.Normalize();
		right.Normalize();
		Vector3 v = new Vector3(inputX, inputZ);


		desiredDirection = inputZ * forward + inputX * right;

		agent.velocity = desiredDirection * agent.speed; ;

		if (v != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredDirection), rotateSpeed);

		}

	}
}

