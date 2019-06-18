
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : CharacterScript
{
	public float rotateSpeed;
	private float magnditude = 0;
	private const float maxMagnitude = 1;

	Vector3 desiredDirection;

	
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



		float inputX = Input.GetAxis("Horizontal");
		float inputZ = Input.GetAxis("Vertical");

		Vector3 forward = Camera.main.transform.forward;
		Vector3 right = Camera.main.transform.right;
		forward.y = 0;
		right.y = 0;
		forward.Normalize();
		right.Normalize();
		Vector3 v = new Vector3(inputX, inputZ);

		magnditude = v.sqrMagnitude < maxMagnitude ? v.sqrMagnitude : maxMagnitude;

		desiredDirection = inputZ * forward + inputX * right;

		if(magnditude > 0.3f)
		agent.Move(desiredDirection*agent.speed * Time.deltaTime);

		if (v != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredDirection), rotateSpeed);

		}

	}
}

