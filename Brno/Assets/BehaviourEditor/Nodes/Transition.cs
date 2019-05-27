using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [Serializable]
    public class Transition
    {
        public CharacterGraph graph;
        public BaseNode StartNode, EndNode;
        public Color CurveColor = Color.black;
        public bool ReadyToDraw { get; set; }
        public string ID = "";
        public bool Enable = true;
        public Transition(BaseNode start, BaseNode end, int id)
        {

            StartNode = start;
            EndNode = end;
            ID = GetID(id);
            Enable = true;
        }
        public string GetID(int id)
        {
            return StartNode.WindowTitle[0].ToString() + StartNode.ID.ToString() + id.ToString();
        }

        public void DrawConnection(Vector3 dir, Color label, string lab,bool left)
        {
            if(Enable)
            BehaviourEditor.DrawNodeCurve(StartNode.WindowRect, EndNode.WindowRect, left, CurveColor, lab, label, dir);
        }
        public void Init()
        {
            if (StartNode.transitions.Count == 0 && graph.InitTransitions.Count > 0)
            {
                Enable = true;
                StartNode = graph.nodes.Find(x => x.ID == StartNode.ID);
                EndNode = graph.nodes.Find(x => x.ID == EndNode.ID);
                StartNode.transitions.Add(this);
                EndNode.depencies.Add(this);
                ReadyToDraw = true;
                if (StartNode is ConditionNode)
                {
                    ConditionNode c = (ConditionNode)StartNode;
                    if (c.T_drawed && ID == c.TrueTransition.ID)
                    {
                        c.TrueTransition = this;
                    }
                    else if (c.F_drawed && ID == c.FalseTransition.ID)
                    {
                        c.FalseTransition = this;
                    }

                }
            }
        }
    }
}