using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/Condition")]
    public class ConditionNode : DrawNode
    {

        public override void DrawCurve(BaseNode b)
        {
            if (b.condition != null)
            {
                foreach (Transition t in b.transitions)
                {
                    if (t == null) continue;
                    BehaviourEditor.DrawNodeCurve(t, t.startNode.WindowRect, t.endNode.WindowRect, t.startPlacement, t.endPlacement, t.Color, t.disabled);
                    t.DrawConnection(t.startNode, t.endNode, t.startPlacement, t.endPlacement, t.Color, t.disabled);
                }

            }
        }

        public override void DrawWindow(BaseNode b)
        {
            b.condition = EditorGUILayout.ObjectField(b.condition, typeof(Condition), false) as Condition;
            if(b.condition == null)
            {
                EditorGUILayout.LabelField("No Condition!");
            }
        }
    }
}
