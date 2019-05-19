using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor {
    [CreateAssetMenu(menuName = "BehaviourEditor/Actions/ChangeHealth")]
    public class ChangeHealth : StateActions
    {
        public override void Execute(StateManager states)
        {
            states.health += 10;

        }
    }
}