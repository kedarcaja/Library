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

        public void OnEnter(CharacterScript states)
        {
            ExecuteActions(states, onEnter);
        }

        public void FixedTick(CharacterScript states)
        {
            ExecuteActions(states, onFixed);
        }

        public void Tick(CharacterScript states)
        {
            ExecuteActions(states, onUpdate);
            CheckTransitions(states);
        }
        public void CheckTransitions(CharacterScript states)
        {

        }

        public void OnExit(CharacterScript states)
        {
            ExecuteActions(states, onExit);
        }
        public void ExecuteActions(CharacterScript states, StateAction[] l)
        {
            for (int i = 0; i < l.Length; i++)
            {
                if (l[i] != null)
                    l[i].Execute(states);
            }
        }
    }
}