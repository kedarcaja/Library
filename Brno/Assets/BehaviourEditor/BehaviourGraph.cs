using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Graph")]
    public class BehaviourGraph : ScriptableObject
    {
        public List<Saved_StateNode> savedStateNodes = new List<Saved_StateNode>();
        Dictionary<StateNode, Saved_StateNode> stateNodesDict = new Dictionary<StateNode, Saved_StateNode>();
        Dictionary<State, StateNode> stateDict = new Dictionary<State, StateNode>();
        public void Init()
        {
            stateNodesDict.Clear();
            stateDict.Clear();
        }
        public void SetNode(BaseNode node)
        {
            if (node is StateNode)
            {
                SetStateNode(node as StateNode);
            }
            if (node is TransitionNode)
            {

                SetTransitionNode(node as TransitionNode);
            }
            if (node is CommentNode)
            {

            }
        }



        #region State Nodes
        public bool IsStateNodeDuplicate(StateNode node)
        {
            bool rVal = false;
            StateNode prevNode = null;

            stateDict.TryGetValue(node.currentState, out prevNode);
            if(prevNode != null)
            {
                rVal = true;
            }
            return rVal;
        }
        void SetStateNode(StateNode node)
        {
            if (node.isDuplicate)
                return;
            if (node.previousState != null)
            {
                stateDict.Remove(node.previousState);
            }

            if (node.currentState == null)
            {
                return;
            }

            Saved_StateNode s = GetSavedState(node);
            if (s == null)
            {
                s = new Saved_StateNode();
                savedStateNodes.Add(s);
                stateNodesDict.Add(node, s);
            }
            s.state = node.currentState;
            s.position = new Vector2(node.windowRect.x, node.windowRect.y);
            s.isCollepsed = node.collapse;
            stateDict.Add(s.state, node);
        }
        void ClearStateNode(StateNode node)
        {
            Saved_StateNode s = GetSavedState(node);
            if (s != null)
            {
                savedStateNodes.Remove(s);
                stateNodesDict.Remove(node);
            }
        }
        public Saved_StateNode GetSavedState(StateNode node)
        {
            Saved_StateNode r = null;
            stateNodesDict.TryGetValue(node, out r);
            return r;
        }
        StateNode GetStateNode(State state)
        {
            StateNode n = null;
            stateDict.TryGetValue(state, out n);
            return n;
        }
        #endregion

        #region Transition Nodes
        public void SetTransitionNode(TransitionNode node)
        {
            Saved_StateNode sn = GetSavedState(node.enterState);
            sn.SetTransitionNode(node);

        }
        public bool IsTransitionDuplicate(TransitionNode node)
        {
            return GetSavedState(node.enterState).IsTransitionDuplicate(node);

        }
        #endregion

    }
    [System.Serializable]
    public class Saved_StateNode
    {
        public State state;
        public Vector2 position;
        public bool isCollepsed;

        public List<Saved_Conditions> savedConditions = new List<Saved_Conditions>();
        Dictionary<TransitionNode, Saved_Conditions> savedTransDict = new Dictionary<TransitionNode, Saved_Conditions>();
        Dictionary<Condition, TransitionNode> condDict = new Dictionary<Condition, TransitionNode>();

        public void Init()
        {
            savedTransDict.Clear();
            condDict.Clear();
        }



        #region Transition Nodes
        public bool IsTransitionDuplicate(TransitionNode node)
        {
            TransitionNode s = null;
            condDict.TryGetValue(node.targeCondition, out s);
            return s != null;
        }
        public void SetTransitionNode(TransitionNode node)
        {
            if (node.isDuplicate)
                return;
            if (node.previousCondition != null)
            {
                condDict.Remove(node.targeCondition);
            }
            if (node.targeCondition == null)
            {
                return;
            }
            Saved_Conditions c = GetSavedCondition(node);
            if (c == null)
            {
                c = new Saved_Conditions();
                savedConditions.Add(c);
                savedTransDict.Add(node, c);
                node.transition = node.enterState.currentState.AddTransition();
            }
            c.transition = node.transition;
            c.condition = node.targeCondition;
            c.position = new Vector2(node.windowRect.x, node.windowRect.y);
            condDict.Add(c.condition, node);

        }
        void ClearTransitionNode(TransitionNode node)
        {

        }
        #endregion

        Saved_Conditions GetSavedCondition(TransitionNode node)
        {
            Saved_Conditions r = null;
            savedTransDict.TryGetValue(node, out r);
            return r;
        }
        public TransitionNode GetTransitionNode(Transition trans)
        {
            TransitionNode n = null;
            condDict.TryGetValue(trans.condition, out n);
            return n;
        }
    }
    [System.Serializable]
    public class Saved_Conditions
    {
        public Transition transition;
        public Condition condition;
        public Vector2 position;

    }
}