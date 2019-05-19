using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
	public class TransitionNode : BaseNode
	{
        public bool isDuplicate;
		public Condition targeCondition;
        public Condition previousCondition;
        public Transition transition;
		public StateNode enterState,targetState;
		public void Init(StateNode enterState,Transition transition)
		{
			this.enterState = enterState;
		}
		public override void DrawWindow()
		{
			

			EditorGUILayout.LabelField("");
			targeCondition = (Condition)EditorGUILayout.ObjectField(targeCondition,typeof(Condition),false);
			if(targeCondition == null)
			{
				EditorGUILayout.LabelField("No Condition!");
           
            }
       
            else
			{
                if (isDuplicate)
                {
                    EditorGUILayout.LabelField("Duplicate Condition!");
                }
                else
                {
                    //targeCondition.disable = EditorGUILayout.Toggle("Disabled: ", targeCondition.disable);
                }
			}

     

            if (previousCondition != targeCondition)
            {
                isDuplicate = BehaviourEditor.currentGraph.IsTransitionDuplicate(this);
                if (!isDuplicate)
                {
                    BehaviourEditor.currentGraph.SetNode(this);
                }
                previousCondition = targeCondition;

            }
        }
		public override void DrawCurve()
		{
			if (enterState)
			{
				Rect rect = windowRect;
				rect.y += windowRect.height * 0.5f;
				rect.width = 1;
				rect.height = 1;
				BehaviourEditor.DrawNodeCurve(enterState.windowRect,rect,true,Color.red);
			}
		}
	}
}