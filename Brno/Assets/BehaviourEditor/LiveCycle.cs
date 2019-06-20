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
				bool isChecked = false;
				switch (currentNode.condition)
				{
					case ECondition.IsThunder:
						isChecked = MainOpossum.GetWeather(currentNode.Graph.character) == EWeather.Thunder;
						break;
					case ECondition.IsSunnyDay:
						isChecked = MainOpossum.GetWeather(currentNode.Graph.character) == EWeather.SunnyDay;

						break;
					case ECondition.IsAlive:

                        isChecked =
                            currentNode.Graph.character.CharacterData.IsAlive;

						break;
					case ECondition.IsTimeToGoToWork:

						break;
					case ECondition.IsDead:
                        isChecked = currentNode.Graph.character.CharacterData.IsAlive == false;

                        break;
					case ECondition.IsNight:
                        break;
					case ECondition.IsMorning:
						break;
					case ECondition.IsPlayerClose:
                        isChecked = currentNode.Graph.character.PlayerIsClose();
						break;
					case ECondition.ReachedDestination:
                        isChecked = currentNode.Graph.character.AgentReachedTarget();
                        break;
			
				}
                if(isChecked)
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