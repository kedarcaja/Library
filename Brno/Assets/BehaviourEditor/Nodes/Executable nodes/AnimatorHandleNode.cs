using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
	[CreateAssetMenu(menuName ="BehaviourEditor/Nodes/Animator/Handling")]
	public class AnimatorHandleNode : ExecutableNode
	{
		public override void DrawCurve(BaseNode node)
		{
		}

		public override void DrawWindow(BaseNode b)
		{
			b.AnimatorActivatorType = (EAnimatorActivator)EditorGUILayout.EnumPopup(b.AnimatorActivatorType);

			BehaviourEditor.GetEGLLable("parameter: ", GUIStyle.none);
			b.parameter = EditorGUILayout.TextField(b.parameter);

			BehaviourEditor.GetEGLLable("Value: ", GUIStyle.none);
			switch (b.AnimatorActivatorType)
			{
				case EAnimatorActivator.Trigger:
					BehaviourEditor.GetEGLLable("Trigger", GUIStyle.none);
					break;
				case EAnimatorActivator.Float:
					b.AnimatorActivatorFloatValue = EditorGUILayout.FloatField(b.AnimatorActivatorFloatValue);
					break;
				case EAnimatorActivator.Bool:
					b.AnimatorActivatorBoolValue = EditorGUILayout.Toggle(b.AnimatorActivatorBoolValue);
					break;
				case EAnimatorActivator.Int:
					b.AnimatorActivatorIntValue = EditorGUILayout.IntField(b.AnimatorActivatorIntValue);
					break;

			}

		}

		public override void Execute(BaseNode b)
		{
			switch (b.AnimatorActivatorType)
			{
				case EAnimatorActivator.Trigger:
					b.Graph.character.Animator.SetTrigger(b.parameter);
					break;
				case EAnimatorActivator.Float:
					b.Graph.character.Animator.SetFloat(b.parameter,b.AnimatorActivatorFloatValue);

					break;
				case EAnimatorActivator.Bool:
					b.Graph.character.Animator.SetBool(b.parameter,b.AnimatorActivatorBoolValue);

					break;
				case EAnimatorActivator.Int:
					b.Graph.character.Animator.SetInteger(b.parameter,b.AnimatorActivatorIntValue);
					break;
			}
		}
	}
}