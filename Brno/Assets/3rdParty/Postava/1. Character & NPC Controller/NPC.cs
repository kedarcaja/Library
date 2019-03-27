using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.AI;
public enum ECharacterState {Standing,Sitting,Croutching,Lie }
public class NPC : Character
{
	[SerializeField]
	private MoveArea randomMoveArea;
	[SerializeField]
	private Transform startPoint;
	[SerializeField]
	Transform smazat;
	protected override void Awake()
	{

		NPCWatcher.NPCS.Add(this);
		base.Awake();
		if (startPoint)
		{
			SetTarget(startPoint);
		}
	}

	protected override void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			StandUp();
		}
		
		if (AgentIsOnPosition)
		{
			if ((stats as NPCStats).RandomMove)
			{
				SetDestination(GetRandomPosition(randomMoveArea.transform.position, randomMoveArea.Range));
			}

			else
			{
				Idle();
			}

		}
		else
		{
			if ((stats.TargetVector.Target != null && TargetInRange(stats.TargetVector.Target, InteractionRadius)) || (stats.TargetVector.Destination != Vector3.zero && DestinationInRange(stats.TargetVector.Destination, InteractionRadius)))
			{
				Run();
			}

			else
			{
				Walk();
			}
		}

		base.Update();
	}

	
	private Vector3 GetRandomPosition(Vector3 startPosition, float rds)
	{
		
		Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * rds;
		randomDirection += startPosition;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, rds, 1);
		Vector3 finalPosition = hit.position;
		return finalPosition;
	}
	private void OnMouseUp()
	{

		if (Vector3.Distance(transform.position, PlayerScript.Instance.transform.position) < MouseManager.Instance.MoveClickRange)
			PlayerScript.Instance.SetTarget(transform);
	}
	public void Sit()
	{
		if (State != ECharacterState.Standing) return;
		stats.TargetVector.Target = null;
		stats.TargetVector.Destination = Vector3.zero;
		transform.LookAt(smazat);
		anim.SetBool("isSitting", true);

		State = ECharacterState.Sitting;

	}
	public void StandUp()
	{
		if (State != ECharacterState.Sitting) return;
		stats.TargetVector.Target = null;
		stats.TargetVector.Destination = Vector3.zero;
		anim.SetBool("isSitting", false);
		Agent.updateRotation = true;
		State = ECharacterState.Standing;
	}
	public void Lie()
	{
		if (State != ECharacterState.Lie) return;
		stats.TargetVector.Target = null;
		stats.TargetVector.Destination = Vector3.zero;
		State = ECharacterState.Lie;


	}

}