using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;
using BehaviourTreeEditor;
public delegate void TimerEventHandler();
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class CharacterScript : MonoBehaviour
{

    protected Animator anim;
    protected NavMeshAgent agent;
    protected Rigidbody rigid;
    public State currentState;
    public BehaviourGraph Graph;
    private void Awake()
    {

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
        anim.SetFloat("speed", agent.velocity.magnitude);



        if (currentState != null)
        {
            currentState.Tick(this);
        }
    }
}