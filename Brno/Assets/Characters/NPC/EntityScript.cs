﻿using BehaviourTreeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EntityScript : CharacterScript
{
    protected BehaviourGraph currentGraph;
 
    protected void InitGraph()
    {
        if (currentGraph)
        {
            if (currentGraph.LiveCycle == null)
            {
                currentGraph.LiveCycle = new LiveCycle();
            }
            currentGraph.LiveCycle.graph = currentGraph;
            currentGraph.LiveCycle.Init();
            currentGraph.character = this;
        }
    }
    protected override void Awake()
    {
        InitGraph();
        base.Awake();
    }
    protected override void Update()
    {
        base.Update();
        if (currentGraph != null)
        {
            currentGraph.LiveCycle.Tick();
        }
    }

    public Vector3 GetRandomMoveArea(RandomMoveArea area)
    {
        if (AgentReachedTarget())
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
    
    public bool PlayerIsClose()
    {
        return ObjectIsClose(PlayerScript.Instance.transform, characterData.InteractionRadius);
    }

}
