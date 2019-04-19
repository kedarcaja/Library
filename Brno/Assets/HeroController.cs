using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
	public static HeroController Instance { get; private set; }
	public int SkillPoints;
	public int XP, Gold, Silver, Copper;
    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
		Instance = FindObjectOfType<HeroController>();
		SkillPoints = 10;
    }

	void Update()
	{
		Move();
	}
    public void SetDestination(Vector3 destination)
    {
        agent.destination = destination;
    }
	public void Stop()
	{
		animator.SetFloat("Speed", 0);
	}
	public void Move()
	{
		animator.SetFloat("Speed", agent.velocity.magnitude);


	}
}
