using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
	public class StateNode : BaseNode
	{
		bool collapse;
		public State currentState;
		public override void DrawCurve()
		{

		}
		public override void DrawWindow()
		{
			if (currentState == null)
			{
				EditorGUILayout.LabelField("Add State to modify: ");
			}
			else
			{
				if (!collapse)
				{
					windowRect.height = 300;
				}
				else
				{
					windowRect.height = 100;

				}
				collapse = EditorGUILayout.Toggle("collapse: ",collapse);
			}
			currentState = (State)EditorGUILayout.ObjectField(currentState,typeof(State),false);
		}
		public Transition AddTransition()
		{
			return currentState.AddTransition();
		}

	}
}