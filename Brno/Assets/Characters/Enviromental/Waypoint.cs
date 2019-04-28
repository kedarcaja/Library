using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public enum EPointType { Sitting, Waiting, Lieing, SittingWaiting, LieingWaiting, InstantNext }
public class Waypoint : MonoBehaviour
{
	//public UnityEvent PlayerEvent, NPCEvent, EnemyEvent;
	//[SerializeField]
	//private EPointType type;

	//public Transform NextPoint
	//{
	//	get
	//	{
	//		return nextPoint;
	//	}
	//}
	//[SerializeField]
	//private Transform nextPoint;
	//public UnityEvent<Vector3> OnTriggerEnterEvent;
	//[SerializeField]
	//private int sitting_Lieing_Rotation, waitTime;
	//private float time = 0;
	//bool startTime = false;
	//private void Awake()
	//{
	//	GetComponent<MeshRenderer>().enabled = false;
	//}
	//private void Update()
	//{
	//	if (startTime)
	//	{
	//		time -= Time.deltaTime;

	//	}
	//}
	//private void OnTriggerStay(Collider other)
	//{
	//	if (other.CompareTag("Player"))
	//	{

	//		PlayerEvent.Invoke();
	//	}
	//	else if (other.GetComponent<NPC>())
	//	{
	//		waitTime = Random.Range(0, 5);
	//		time = 0;
	//		GetComponent<SphereCollider>().enabled = false;
	//		Character n = other.GetComponent<NPC>();


	//		switch (type)
	//		{
	//			case EPointType.Sitting:
				
	//				break;
	//			case EPointType.Waiting:
	//				IncrementTimer t = new IncrementTimer();
	//				t.Init(1, 1, this);
	//				t.OnTimerUpdate += delegate { if (t.Time == waitTime) t.Stop(); };
	//				t.OnTimerEnd += delegate
	//				{
	//					if (t.Time == waitTime)
	//					{

					
	//					}

	//				};

	//				t.Start();

	//				break;
	//			case EPointType.Lieing:
				
	//				break;
	//			case EPointType.InstantNext:
				
	//				break;
	//			case EPointType.SittingWaiting:
				
	//				IncrementTimer tm = new IncrementTimer();
	//				tm.Init(1, 1, this);
	//				tm.OnTimerUpdate += delegate { if (tm.Time == waitTime) tm.Stop(); };

	//				tm.OnTimerEnd += delegate
	//				{
	//					if (tm.Time == waitTime)
	//					{
						
	//					}

	//				};

	//				tm.Start();
	//				break;

	//		}

	//	}
	//	else if (other.GetComponent<Enemy>())
	//	{

		
	//		Enemy n = other.GetComponent<Enemy>();


	//		switch (type)
	//		{
	//			case EPointType.Sitting:
	//				n.Agent.updateRotation = false;
	//				transform.Rotate(sitting_Lieing_Rotation, 0, 0);
	//				//n.Sit();
	//				break;
	//			case EPointType.Waiting:
	//				if(!startTime)
	//				{
	//					time = Random.Range(0, 5);
	//					startTime = true;

	//				}

	//				if (time < 0)
	//				{
	//					startTime = false;
	//					if (n.Stats.TargetVector.Target == transform && NextPoint != null)
	//					{
	//						n.SetTarget(NextPoint);
	//					}
	//				}

	//				break;
	//			case EPointType.Lieing:
	//				n.Agent.updateRotation = false;
	//				transform.Rotate(sitting_Lieing_Rotation, 0, 0);
	//				//n.Lie();
	//				break;
	//			case EPointType.InstantNext:
	//				if (n.Stats.TargetVector.Target == transform && NextPoint != null)
	//				{
	//					n.SetTarget(NextPoint);
	//				}
	//				break;
	//			case EPointType.SittingWaiting:
	//				n.Agent.updateRotation = false;
	//				transform.Rotate(sitting_Lieing_Rotation, 0, 0);
	//				//	n.Sit();

	//				if (time == 0)
	//				{
	//					//	n.StandUp();
	//					if (n.Stats.TargetVector.Target == transform && NextPoint != null)
	//					{
	//						n.SetTarget(NextPoint);
	//					}






	//				}
	//				break;

	//		}
	//	}
	

}


//private void OnDrawGizmos()
//{
//	if (NextPoint != null)
//	{
//		Gizmos.color = new Color32(255, 125, 0, 255);
//		Gizmos.DrawLine(transform.position, nextPoint.position);
//	}
//}

	
//}


