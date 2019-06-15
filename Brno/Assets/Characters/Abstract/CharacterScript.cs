using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;
using BehaviourTreeEditor;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class CharacterScript : MonoBehaviour
{

    public float delete = 10;

    protected Animator anim;
    protected NavMeshAgent agent;
    protected Rigidbody rigid;
    [SerializeField]
    private BehaviourGraph Graph;

    public Animator Animator { get => anim; }
#if UNITY_EDITOR

    public AnimatorController AnimatorController { get => (AnimatorController)anim.runtimeAnimatorController; set => anim.runtimeAnimatorController = value; }
#endif
    private void Awake()
    {
        if (Graph.LiveCycle == null)
        {
            Graph.LiveCycle = new LiveCycle();
        }
        Graph.LiveCycle.graph = Graph;
        Graph.LiveCycle.Init();
        Graph.character = this;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }
    public void SetTarget(Transform target)
    {
        SetDestination(target.position);
    }
    public void SetDestination(Vector3 dest)
    {
        agent.SetDestination(dest);
    }

    private void Update()
    {
      //  anim.SetFloat("speed", agent.velocity.magnitude);

        if (Graph != null)
        {
            Graph.LiveCycle.Tick();
        }

    }
    public bool AgentReachedTarget()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

    public bool Delete()
    {
        return delete > 0;
    }
    public Vector3 GetRandomMoveArea(RandomMoveArea area)
    {
        if(AgentReachedTarget())
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * area.radius;
            randomDirection += area.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, area.radius, 1);
            Vector3 finalPosition = hit.position;
            return finalPosition;
        }
        return agent.destination;
    }
    public void RandomMove(RandomMoveArea area)
    {
        SetDestination(GetRandomMoveArea(area));
    }
}