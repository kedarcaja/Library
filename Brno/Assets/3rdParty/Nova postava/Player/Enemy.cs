using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : Entity, IID
{
	public Equipment Head, Spaulder, Arm, Chest, Trausers, Shoes;
	public Weapon Weapon, Bow, SecondHand;
	private Vector3 lastPlayerPosition;// save
	[SerializeField]
	private float playerSearchRadius;
	[SerializeField]
	private List<FieldOfView> fieldsOfView = new List<FieldOfView>();

	[SerializeField]
	private float maxFieldOfViewAngle;
	private IncrementTimer playerSearchTimer = new IncrementTimer();
	[SerializeField]
	private int playerSearchTime = 0;
	private Vector3 startPosition; // uložení ?
	[SerializeField]
	private float maxDistanceFromPlayer;
	[SerializeField]
	private int id;
	public int ID
	{
		get
		{
			return id;
		}
	}
	[SerializeField]
	private MoveArea randomMoveArea;

	protected override void Awake()
	{

		playerSearchTimer.Init(1, 1, this);
		startPosition = transform.position;
		playerSearchTimer.OnTimerStart += new TimerHandler(() => (stats as EnemyStats).State = EEnemyState.Search);
		playerSearchTimer.OnTimerEnd += new TimerHandler(() => (stats as EnemyStats).State = EEnemyState.Neutral);
		base.Awake();
		Idle();

		(stats as EnemyStats).State = EEnemyState.Neutral;
	}
	public override void Die()
	{
		
		QuestManager.Instance.UpdateKillQuestParts(this);
		base.Die();
	}
	protected override void Update()
	{
		//if (Input.GetKeyDown(KeyCode.B))
		//{
		//	Die();
		//}

		//if (CanSeeTarget(PlayerScript.Instance.transform, PlayerScript.Instance.Agent.height))
		//{
		//	/////////////////// interaction
		//	QuestManager.Instance.UpdateKillQuestParts(this);

		//	//////////////////
		//	Attack(PlayerScript.Instance);
		//	lastPlayerPosition = PlayerScript.Instance.transform.position;
		//	(stats as EnemyStats).State = EEnemyState.Attack;
		//	if (playerSearchTimer.isRunning)
		//	{
		//		playerSearchTimer.Stop();
		//	}
		//}
		//else
		//{
		//	if ((stats as EnemyStats).State == EEnemyState.Attack || (stats as EnemyStats).State == EEnemyState.Search)
		//	{
		//		(stats as EnemyStats).State = EEnemyState.Search;
		//		if (!playerSearchTimer.isRunning && Vector3.Distance(transform.position, lastPlayerPosition) <= Agent.stoppingDistance)
		//		{
		//			playerSearchTimer.Start();
		//		}
		//		if (playerSearchTimer.isRunning && playerSearchTimer.GetTimeInt() < playerSearchTime)
		//		{
		//			stats.TargetVector.Target = null;
		//			if (AgentIsOnPosition)
		//			{
		//				SetDestination(GetRandomPosition(lastPlayerPosition, playerSearchRadius));
		//			}
		//		}
		//		else if (playerSearchTimer.isRunning)
		//		{

		//			playerSearchTimer.Stop();
		//			SetDestination(startPosition);

		//		}

		//	}
		//	if ((stats as EnemyStats).State != EEnemyState.Search && stats.TargetVector.Destination == startPosition && AgentIsOnPosition)
		//	{
		//		(stats as EnemyStats).State = EEnemyState.Neutral;
		//	}

		//}


		if (TargetInRange(PlayerScript.Instance.transform, maxDistanceFromPlayer))
		{
			MeelAttack();
			DisableAgent();
		}
		else
		{
			RestoreAgent();
		}
		if (AgentIsOnPosition)
		{
			if ((stats as EnemyStats).RandomMove)
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
	public override void Defend()
	{
		Idle();
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="enemy">nevíme zda bude cíl jen hráč nebo i třeba nějaký jiný cíl</param>
	public override void Attack()
	{
		
	}

	/// <summary>
	/// řešeno úhlem a vzdáleností
	/// </summary>
	/// <returns></returns>
	public bool CanSeeTarget(Transform target, float targetHeight)
	{

		if ((stats as EnemyStats).State == EEnemyState.Search || (stats as EnemyStats).State == EEnemyState.Attack)
		{
			Vector3 targetDir = target.position - transform.position;
			float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

			if (!Physics.Linecast(transform.position, target.position))
			{
				if (angleToPlayer >= -maxFieldOfViewAngle && angleToPlayer <= maxFieldOfViewAngle && Vector3.Distance(transform.position, target.position) < maxDistanceFromPlayer)
				{
					Debug.DrawLine(transform.position, target.position, Color.red);
					return true;
				}
			}
		}
		else
		{
			if (fieldsOfView.Any(f => f.CanSeeTarget(target, targetHeight)))
			{
				(stats as EnemyStats).State = EEnemyState.Search;
				return true;
			}
		}
		return false;
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

	protected override void OnDrawGizmos()
	{

		if ((stats as EnemyStats).State == EEnemyState.Neutral)
		{
			fieldsOfView.ForEach(f => f.Draw());
		}
		else if ((stats as EnemyStats).State == EEnemyState.Detected)
		{
			fieldsOfView.ForEach(f => f.Draw());
		}
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,maxDistanceFromPlayer);
		base.OnDrawGizmos();
	}
 
 void ApplyDamage(float damage)
	{
		if (stats.Health > 0)
		{ 
			stats.Health -= damage;if (!stats.IsAlive)
			{
				if (wasStunt)
					Die();
				else
					Stun();
			}
		}
	}

    private void OnMouseUp()
    {
        if (Vector3.Distance(transform.position, PlayerScript.Instance.transform.position) < MouseManager.Instance.MoveClickRange)
        {
            PlayerScript.Instance.SetTarget(transform);
            PlayerScript.Instance.Attack();
        }
    }
	public void MeelAttack()
	{
		anim.SetTrigger("attack");
		SetTarget(PlayerScript.Instance.transform);
		Collider[] colls = Physics.OverlapSphere(transform.position, maxDistanceFromPlayer);
		foreach (Collider hit in colls)
		{
			if (hit && hit.tag == "Player")
			{
				float angle = Vector3.Angle(transform.position, hit.transform.position);
				

				var dist = Vector3.Distance(hit.transform.position, transform.position);
				if (dist <= maxDistanceFromPlayer && angle <= 90)
				{
					hit.SendMessage("GotHit");
				}
			}
		}
	}
}
