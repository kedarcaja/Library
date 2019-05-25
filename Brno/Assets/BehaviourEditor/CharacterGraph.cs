using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Character Graph")]
    public class CharacterGraph : ScriptableObject
    {
        public List<BaseNode> nodes = new List<BaseNode>();
        [SerializeField]
        private Character character;
        public List<int> removeNodesIDs = new List<int>();
        public Character Character { get => character; }
        [SerializeField]
        private int nodeIDS = 0;

        public bool Saved = false;

        #region Saving/Loading

        public List<ConditionNode> conds = new List<ConditionNode>();
        public List<StateNode> states = new List<StateNode>();
        public List<CommentNode> coms = new List<CommentNode>();
        public List<T> CloneList<T>() where T : BaseNode
        {



            List<T> saved = new List<T>();
            
            foreach (BaseNode b in nodes)
            {
                if (b?.GetType() == typeof(T))
                {

                    saved.Add((T)b);
                }
            }

            return saved;
        }
        public void SaveNodes()
        {
            coms = CloneList<CommentNode>();
            conds = CloneList<ConditionNode>();
            states = CloneList<StateNode>();
            nodes.Clear();
            Debug.Log(string.Format("<color=green>Nodes has been saved</color>"));
            Saved = true;

        }
        public void LoadNodes()
        {
            if (coms.Count > 0)
                nodes.AddRange(coms);
            if (conds.Count > 0)
                nodes.AddRange(conds);
            if (states.Count > 0)
                nodes.AddRange(states);

            conds.Clear();
            coms.Clear();
            states.Clear();

            Debug.Log(string.Format("<color=green>Nodes has been loaded</color>"));

        }

        #endregion

        public T AddNode<T>(float x, float y, float width, float height, string title) where T : BaseNode
        {
            Saved = false;

            BaseNode n = (T)Activator.CreateInstance(typeof(T));
            n.WindowRect = new Rect(x, y, width, height);
            n.WindowTitle = title;
            nodes.Add(n);
            n.CharacterGraph = this;
            n.ID = nodeIDS;
            nodeIDS++;
            n.normalHeight = n.WindowRect.height;
            n.nodeType = typeof(T);
            return n as T;
        }


        public void AddToRemoveNode(int id)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (id == nodes[i].ID)
                {
                    removeNodesIDs.Add(id);
                }

            }

        }

        public void RemoveNodeSelectedNodes()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] == null) continue;

                if (removeNodesIDs.Contains(nodes[i].ID))
                {
                    ClearNodeDepencies(nodes[i]);
                    nodes.Remove(nodes[i]);
                }

            }
            removeNodesIDs.Clear();
        }

        public void ClearNodeDepencies(BaseNode node)
        {
            for (int i = 0; i < node.depencies.Count; i++)
            {
                if (node.depencies[i] == null) continue;

                if (node.depencies[i].StartNode is ConditionNode)
                {
                    ConditionNode c = node.depencies[i].StartNode as ConditionNode;
                    if (node.depencies[i] == c.TrueTransition)
                    {
                        c.TrueTransition.ReadyToDraw = false;
                        c.TrueTransition = null;
                    }
                    else if (node.depencies[i] == c.FalseTransition)
                    {
                        c.FalseTransition.ReadyToDraw = false;
                        c.FalseTransition = null;
                    }
                }
                node.depencies[i].StartNode.transitions.Remove(node.depencies[i]);

            }
        }
    }
}
