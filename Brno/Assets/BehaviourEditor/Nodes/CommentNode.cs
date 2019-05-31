using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Nodes/Comment")]
    public class CommentNode : DrawNode
    {

        public override void DrawWindow(BaseNode b)
        {
            b.comment = GUILayout.TextArea(b.comment, 200);
        }
        public override void DrawCurve(BaseNode b)
        {

            foreach (Transition t in b.transitions)
            {
                if (t == null) continue;
                BehaviourEditor.DrawNodeCurve(t, t.startNode.WindowRect, t.endNode.WindowRect, t.startPlacement, t.endPlacement, t.Color, t.disabled);
                t.DrawConnection(t.startNode, t.endNode, t.startPlacement, t.endPlacement, t.Color, t.disabled);
            }
        }

    }
}