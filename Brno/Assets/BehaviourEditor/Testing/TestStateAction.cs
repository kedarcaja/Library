using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Fall")]
    public class TestStateAction : StateAction
    {
        public override void Execute(CharacterScript states)
        {

            Debug.Log("Fall was executed");
            states.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}