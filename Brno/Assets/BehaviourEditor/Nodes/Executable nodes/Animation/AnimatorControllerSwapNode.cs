using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
	[CreateAssetMenu(menuName ="BehaviourEditor/Nodes/Animator/Swap")]
	public class AnimatorControllerSwapNode : ExecutableNode
	{
		public override void DrawCurve(BaseNode b)
		{

		}

		public override void DrawWindow(BaseNode b)
		{
			b.Animator = EditorGUILayout.ObjectField(b.Animator, typeof(Animator), false) as Animator;
		}

		public override void Execute(BaseNode b)
		{
			
		}
	}
}