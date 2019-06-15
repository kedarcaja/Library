using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Conditions/IsAlive")]
    public class IsAlive : Condition
    {
        public override bool IsChecked(CharacterScript character)
        {
            return character.Delete();
        }
    }
}
