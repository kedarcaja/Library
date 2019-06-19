using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;
using BehaviourTreeEditor;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterScript : MonoBehaviour
{


    protected NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    protected Rigidbody rigid;

    protected Animator anim;
    public Animator Animator { get => anim; }
	public bool IsRunning = false;
    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }


    protected virtual void Update()
    {
        anim.SetFloat("magnitudeSpeed", agent.velocity.magnitude);

    }
    public bool AgentReachedTarget()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

   

}