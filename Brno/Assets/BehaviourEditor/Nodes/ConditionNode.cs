using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace BehaviourTreeEditor
{
    [Serializable]
    public class ConditionNode : BaseNode
    {
        public Condition Condition;
        public Transition MainTransition = null, TrueTransition = null, FalseTransition = null;
        public bool CreatingFalseTransition = false, CreatingTrueTransition = false, T_drawed, F_drawed;

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

        public bool IsTransitionInSameState(Transition t)
        {
            return  (T_drawed && TrueTransition.EndNode == t.EndNode) || (F_drawed && FalseTransition.EndNode == t.EndNode);
        }
        public override void DrawCurve()
        {
            if (TrueTransition != null && TrueTransition.ReadyToDraw && T_drawed)
            {
                TrueTransition.CurveColor = Color.red;
                TrueTransition.DrawConnection(Vector3.left, Color.red, "True", true);
            }
            if (FalseTransition != null && FalseTransition.ReadyToDraw && F_drawed)
            {
                FalseTransition.CurveColor = Color.blue;
                FalseTransition.DrawConnection(Vector3.right, Color.blue, "False", true);

            }
            if (MainTransition != null)
            {
                MainTransition.CurveColor = Color.white;
                MainTransition.DrawConnection(Vector3.zero, Color.black, "", false);
            }
        }
    }
}