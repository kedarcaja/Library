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

    public float delete = 10;

    protected NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    protected Rigidbody rigid;

    protected Animator anim;

    public Animator Animator { get => anim; }

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }


    protected virtual void Update()
    {
      //  anim.SetFloat("speed", agent.velocity.magnitude);
    }
    public bool AgentReachedTarget()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

    public bool Delete()
    {
        return delete > 0;
    }

}