using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace BehaviourTreeEditor
{
    public class ConditionNode : BaseNode
    {
        public Condition Condition { get; private set; }
        public override void DrawWindow()
        {
            base.DrawWindow();
            Condition = EditorGUILayout.ObjectField(Condition,typeof(Condition),false) as Condition;

            if(Condition == null)
            {
                EditorGUILayout.LabelField("No Condition!");


            }
            else
            {
                GUI.DrawTexture(new Rect(WindowRect.width/2-42,WindowRect.height/2+5,70,70), Condition.Icon);

            }

        }
    }
}