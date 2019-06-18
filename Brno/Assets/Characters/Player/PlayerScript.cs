
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : CharacterScript
{
	private Vector3 previousPosition;
	public float curSpeed;
	

	protected override void Update()
	{

		anim.SetFloat("speed",GetAgentVelocityManditude());


		if (Input.GetKey(KeyCode.LeftShift))
		{
			agent.speed = 5;
		}
		else
		{
			agent.speed = 2.25f;
		}
	}



	private float GetAgentVelocityManditude()
	{
		Vector3 curMove = transform.position - previousPosition;
		curSpeed = curMove.magnitude / Time.deltaTime;
		previousPosition = transform.position;
		return curSpeed;
	}
}

