using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/State")]
    public class State : ScriptableObject
    {
        public List<Transition> transitions = new List<Transition>();
        public StateActions[] onState;
        public StateActions[] onEnter;
        public StateActions[] onExit;

        public void OnEnter(StateManager state)
        {
            ExecuteActions(state, onEnter);

        }

        public void Tick(StateManager state)
        {
            ExecuteActions(state, onState);
            CheckTransitions(state);

        }
        public void OnExit(StateManager state)
        {
            ExecuteActions(state, onExit);
        }
        public void ExecuteActions(StateManager state, StateActions[] stateActions)
        {
            for (int i = 0; i < stateActions.Length; i++)
            {
                if (stateActions[i] != null)
                {
                    stateActions[i].Execute(state);
                }
            }
        }
        public void CheckTransitions(StateManager state)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].disable)
                    continue;
                if (transitions[i].condition.CheckedCondition(state))
                {
                    if (transitions[i].targetState != null)
                    {
                        state.currentState = transitions[i].targetState;
                        OnExit(state);
                        state.currentState.OnEnter(state);
                    }
                    return;
                }
            }
        }
        public Transition AddTransition()
        {
            Transition t = new Transition();
            transitions.Add(t);
            return t;
        }
    }
}