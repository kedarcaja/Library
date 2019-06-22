using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            CheckAlwaysConditions();


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
        public void CheckAlwaysConditions()
        {

            foreach (BaseNode b in graph.nodes)
            {
                if (b.drawNode is CheckAlwaysNode)
                {
                    foreach (ECondition c in b.alwaysCheckConditions)
                    {
                        if (ConditionNode.IsChecked(c, graph.character)) currentNode = b;
                    }
                }
            }
        }
        public void Init()
        {
            currentNode = graph.nodes.Find(f => (f.drawNode is EnterNode));
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


                if (ConditionNode.IsChecked(currentNode.condition, graph.character))
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
            if (currentNode.drawNode is CheckAlwaysNode)
            {
                currentNode = currentNode.transitions[0]?.endNode;
            }

        }

    }
}