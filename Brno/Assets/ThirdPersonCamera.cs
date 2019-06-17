using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	public GameObject target, player;
	public float rotateSpeed = 5;
	[SerializeField]
	Vector3 offset;

	public float damping = 1;

	void Start()
	{
		offset = target.transform.position - transform.position;
	}

	void LateUpdate()
	{


		MouseAim();
		LookAtPlayer();
	}
	public void MouseAim()
	{
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		target.transform.Rotate(0, horizontal, 0);

		Quaternion rotation = Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
		transform.position = target.transform.position - (rotation * offset);

		transform.LookAt(player.transform);
	}
	public void LookAtPlayer()
	{
		float currentAngle = transform.eulerAngles.y;

		float angle = Mathf.LerpAngle(currentAngle, player.transform.eulerAngles.y, Time.deltaTime * damping);

		Quaternion rotationr = Quaternion.Euler(0, angle, 0);
		transform.position = player.transform.position - (rotationr * offset);

		transform.LookAt(player.transform);
	}
}
