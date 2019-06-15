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
            DecideForNextNode();


            if (currentNode != null)
            {
                currentNode.nodeColor = Color.green;
                currentNode.Execute();
            }

        }
        public void CheckTransitions()
        {
            if (currentNode != null)
            {
                foreach (Transition t in currentNode.transitions)
                {
                    currentNode.nodeCompleted = false;
                    currentNode = t.endNode;
                }

            }

        }
        public void Init()
        {
            currentNode = graph.nodes[0];
        }
        public void DecideForNextNode()
        {


            if (currentNode.drawNode is ExecutableNode && currentNode.nodeCompleted)
            {
                CheckTransitions();
              
                return;
            }
            if (currentNode.drawNode is ConditionNode)
            {

                if (currentNode.condition.IsChecked(graph.character))
                {
                    if (currentNode.transitions.Exists(x => x.Value == "true"))
                    {

                        currentNode = currentNode.transitions.Find(x => x.Value == "true").endNode;


                    }
                }
                else
                {
                    if (currentNode.transitions.Exists(x => x.Value == "false"))
                    {

                        currentNode = currentNode.transitions.Find(x => x.Value == "false").endNode;

                    }
                }
            

                return;
            }
            if (currentNode.drawNode is PortalNode)
            {
                BaseNode b = graph.nodes.Find(n => n.ID == currentNode.portalTargetNodeID);
                currentNode = b != null ? b : currentNode;
               
            }

        }
     
    }
}