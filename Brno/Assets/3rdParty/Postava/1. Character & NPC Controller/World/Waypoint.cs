using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour
{
	[SerializeField]
	private EProffesion profession;
	public EProffesion Profession
	{
		get
		{
			return profession;
		}
	}

	public Transform NextPoint
	{
		get
		{
			return nextPoint;
		}
	}
	[SerializeField]
	private Transform nextPoint;
	public UnityEvent<Vector3> OnTriggerEnterEvent;
	private void Awake()
	{
	
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Inventory.Instance.AddItemWithName("Sword",1);
		}
		if (other.GetComponent<NPC>() != null)
		{

			NPC n = other.GetComponent<NPC>();
			if (n.Stats.TargetVector.Target == transform && NextPoint != null)
			{
				n.SetTarget(NextPoint);
			}
		}
	}
	private void OnDrawGizmos()
	{
		if (NextPoint != null)
		{
			Gizmos.color = new Color32(255,125,0,255);
			Gizmos.DrawLine(transform.position,nextPoint.position);
		}
	}
}


