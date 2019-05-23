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
        public T AddNode<T>(float x, float y, float width, float height, string title) where T : BaseNode
        {

            BaseNode n = (T)Activator.CreateInstance(typeof(T));
            n.WindowRect = new Rect(x, y, width, height);
            n.WindowTitle = title;
            nodes.Add(n);
            n.CharacterGraph = this;
            n.ID = nodeIDS;
            nodeIDS++;
            n.normalHeight = n.WindowRect.height;
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
