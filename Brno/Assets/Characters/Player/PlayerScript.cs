
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
	private const float  maxMagnitude = 1;



	private void Start()
	{
	}
	protected override void Update()
	{
		if (magnditude > maxMagnitude) magnditude = maxMagnitude;

		if (Input.GetKey(KeyCode.LeftShift))
		{
			agent.speed = 5f;
			anim.SetFloat("magnitudeSpeed", magnditude , 0.0f, Time.deltaTime);


		}
		else
		{
			agent.speed = 2.5f;
			anim.SetFloat("magnitudeSpeed", magnditude / 2, 0.0f, Time.deltaTime);


		}



		moveVector.x = Input.GetAxis("Horizontal");
		moveVector.z = Input.GetAxis("Vertical");

		magnditude = moveVector.sqrMagnitude;



		agent.Move(moveVector * agent.speed * Time.deltaTime);

		if (moveVector != Vector3.zero)
		{
			agent.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveVector), rotateSpeed);

		}

	}
}

