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
           
        }

        public override void DrawWindow(BaseNode b)
        {
#if UNITY_EDITOR

            b.condition = EditorGUILayout.ObjectField(b.condition, typeof(Condition), false) as Condition;
            if(b.condition == null)
            {
                EditorGUILayout.LabelField("No Condition!");
            }
#endif
        }
    }
}
