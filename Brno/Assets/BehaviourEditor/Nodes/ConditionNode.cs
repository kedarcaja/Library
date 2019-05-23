using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace BehaviourTreeEditor
{
    public class ConditionNode : BaseNode
    {
        public Condition Condition { get; private set; }
        public Transition MainTransition, TrueTransition, FalseTransition;
        public bool CreatingFalseTransition = false, CreatingTrueTransition = false;

        public override void DrawWindow()
        {
            base.DrawWindow();
            Condition = EditorGUILayout.ObjectField(Condition, typeof(Condition), false) as Condition;

            if (Condition == null)
            {
                GUI.contentColor = Color.red;
                EditorGUILayout.LabelField("No Condition!");
            }
            else
            {
            }

        }

        public override void DrawCurve()
        {
            if (TrueTransition != null && TrueTransition.ReadyToDraw)
            {
                TrueTransition.CurveColor = Color.red;
                BehaviourEditor.DrawNodeCurve(TrueTransition.StartNode.WindowRect, TrueTransition.EndNode.WindowRect, false, TrueTransition.CurveColor,"True",Color.red,Vector3.left);
            }
            if (FalseTransition != null && FalseTransition.ReadyToDraw)
            {
                FalseTransition.CurveColor = Color.blue;
                BehaviourEditor.DrawNodeCurve(FalseTransition.StartNode.WindowRect, FalseTransition.EndNode.WindowRect, true, FalseTransition.CurveColor,"False",Color.blue,Vector3.right);
            }
            if (MainTransition != null)
            {
                MainTransition.CurveColor = Color.white;
                BehaviourEditor.DrawNodeCurve(MainTransition.StartNode.WindowRect, MainTransition.EndNode.WindowRect, true, MainTransition.CurveColor,"",Color.black,Vector3.zero);
            }
        }
    }
}