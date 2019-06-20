﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/Stop Move")]

    public class StopNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode node)
        {
        }

        public override void DrawWindow(BaseNode b)
        {

            GUIStyle s = new GUIStyle();
            s.fontSize = 30;
            s.richText = true;
            
            BehaviourEditor.GetEGLLable("Stop", s);
        }

        public override void Execute(BaseNode b)
        {
            b.Graph.character.SetDestination(Vector3.zero);
            b.nodeCompleted = b.Graph.character.AgentReachedTarget() && b.Graph.character.Agent.velocity.magnitude == 0;
        }
    }
}
