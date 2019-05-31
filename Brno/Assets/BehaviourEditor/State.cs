using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/State")]
    public class State : ScriptableObject
    {
        public StateAction[] onFixed;
        public StateAction[] onUpdate;
        public StateAction[] onEnter;
        public StateAction[] onExit;
    }
}