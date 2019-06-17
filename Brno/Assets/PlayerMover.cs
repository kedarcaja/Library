using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	public float turningSpeed = 60;
	private float distanceFromPlayer = 2;

	
	private void Update()
	{
		if(PlayerInRange())
		Move();	
	}
	private void Move()
	{
		float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
		transform.Rotate(0, horizontal, 0);
		float vertical = Input.GetAxis("Vertical") * FindObjectOfType<PlayerScript>().Agent.speed * Time.deltaTime;
		transform.Translate(0, 0, vertical);
	}

	private bool PlayerInRange()
	{
		return distanceFromPlayer > Vector3.Distance(transform.position, PlayerScript.Instance.transform.position);
	}
}
