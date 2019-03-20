using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float patrolTime = 15; 
    public float aggroRange = 10; 
    public Transform[] waypoints; 

    int index; 
    float speed, agentSpeed; 
    Transform player; 

    Animator animator; 
    NavMeshAgent agent; 

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) { agentSpeed = agent.speed; }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length>0)
        {
            InvokeRepeating("Patrol",Random.Range(0, patrolTime),patrolTime);
        }
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    void Tick()
    {
        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 2;

        if (player!=null && Vector3.Distance(transform.position,player.position)<aggroRange)
        {
            agent.destination = player.position;
            agent.speed = agentSpeed;
            
        }
        // if (player != null&& Vector3.Distance(transform.position, player.position) < 2)
        //{
        //  //  animator.SetBool("attack", true);
        //}
        //else
        //{
        //    //animator.SetBool("attack", false);
        //}
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
