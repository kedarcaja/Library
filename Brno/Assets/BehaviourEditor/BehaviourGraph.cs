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


            }
            if (node is CommentNode)
            {

            }
        }
        #region State Nodes

        public void SetStateNode(StateNode node)
        {
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
        }
        public void ClearStateNode(StateNode node)
        {
            Saved_StateNode s = GetSavedState(node);
            if (s != null)
            {
                savedStateNodes.Remove(s);
                stateNodesDict.Remove(node);
            }
        }
        Saved_StateNode GetSavedState(StateNode node)
        {
            Saved_StateNode r = null;
            stateNodesDict.TryGetValue(node, out r);
            return r;
        }
        public StateNode GetStateNode(State state)
        {
            StateNode n = null;
            stateDict.TryGetValue(state, out n);
            return n;
        }
        #endregion



    }
    [System.Serializable]
    public class Saved_StateNode
    {
        public State state;
        public Vector2 position;
        public bool isCollepsed;
    }
    [System.Serializable]
    public class Saved_Transition
    {

    }
}