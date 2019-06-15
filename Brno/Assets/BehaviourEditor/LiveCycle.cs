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
            if (currentNode.drawNode is ExecutableNode && currentNode.nodeCompleted)
            {

                currentNode.nodeCompleted = false;
                currentNode = t.endNode;

                return;
            }
            else if (currentNode.drawNode is ConditionNode)
            {

                if (currentNode.condition.IsChecked(graph.character))
                {
                    if(currentNode.transitions.Exists(x=>x.Value == "true"))
                    {
                       currentNode = currentNode.transitions.Find(x=>x.Value == "true").endNode;

                    }
                    return;

                }
                else
                {
                    if (currentNode.transitions.Exists(x => x.Value == "false"))
                    {
                        currentNode = currentNode.transitions.Find(x => x.Value == "false").endNode;

                    }
                }


            }
        }
    }
}