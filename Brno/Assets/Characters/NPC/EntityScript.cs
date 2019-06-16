using BehaviourTreeEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityScript : CharacterScript
{
    [SerializeField]
    protected BehaviourGraph Graph;
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
    public void SetTarget(Transform target)
    {
        SetDestination(target.position);
    }
    public void SetDestination(Vector3 dest)
    {
        agent.SetDestination(dest);
    }
    protected override void Awake()
    {
        if (Graph)
        {
            if (Graph.LiveCycle == null)
            {
                Graph.LiveCycle = new LiveCycle();
            }
            Graph.LiveCycle.graph = Graph;
            Graph.LiveCycle.Init();
            Graph.character = this;
        }
        base.Awake();
    }
    protected override void Update()
    {
        base.Update();
        if (Graph != null)
        {
            Graph.LiveCycle.Tick();
        }
    }
}
