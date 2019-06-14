using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [Serializable]
    public class LiveCycle
    {
        [NonSerialized]
        public BehaviourGraph graph;
        public BaseNode currentNode;



        public BaseNode CurrentNode { get => currentNode; }
        public void Tick()
        {
            CheckTransitions();

            if (currentNode != null)

            {
                currentNode.Execute();
            }

        }
        public void CheckTransitions()
        {
            if (currentNode != null)
            {
                foreach (Transition t in currentNode.transitions)
                {
                    DecideForNextNode(t);
                }
            }

        }
        public void Init()
        {
            currentNode = graph.nodes[0];
        }
        public void DecideForNextNode(Transition t)
        {
            if (currentNode.collapse)
            {
                currentNode = t.endNode;
               
            }
        }
    }
}