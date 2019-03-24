using UnityEngine;
using UnityEngine.AI;

public class NavigationAnimation: MonoBehaviour
{
    Animator animator; // reference to the animator component
    NavMeshAgent agent; // reference to the NavMeshAgent

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        animator.SetFloat("Speed",agent.velocity.sqrMagnitude);
    }
}
