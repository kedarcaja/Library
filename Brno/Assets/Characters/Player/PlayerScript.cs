
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : CharacterScript
{
    [SerializeField]
    private Transform moveTarget;
    protected override void Update()
    {
        agent.SetDestination(moveTarget.position);
        base.Update();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            agent.speed = 5;
        }
        else
            agent.speed = 2.2f;
    }
    
 

}

