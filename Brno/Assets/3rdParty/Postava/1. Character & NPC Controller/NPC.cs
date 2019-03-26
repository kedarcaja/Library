using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.AI;

public class NPC : Character
{
	protected override void Awake()
	{
		
		NPCWatcher.NPCS.Add(this);
		base.Awake();
	}
	protected override void Update()
	{
		if ((stats as NPCStats).RandomMove)
		{
			RandomMove(transform.position, 50); // specific radius
		}
        if (AgentIsOnPosition)
        {
            Idle();
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

	public void RandomMove(Vector3 startPos, float radius)
	{
		if (AgentIsOnPosition)
		{

			SetDestination(GetRandomPosition(startPos, radius));
		}
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
}