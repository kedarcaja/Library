using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
	[CreateAssetMenu(menuName ="BehaviourEditor/Nodes/SetDestination")]
	public class SetDestinationNode : ExecutableNode
	{
		public override void DrawCurve(BaseNode node)
		{
		}

		public override void DrawWindow(BaseNode b)
		{
		
			BehaviourEditor.GetEGLLable("target: ",GUIStyle.none);
			b.destinationTarget = EditorGUILayout.ObjectField(b.destinationTarget, typeof(Transform), true) as Transform;
		}

		public override void Execute(BaseNode b)
		{
			b.Graph.character?.SetTarget(b.destinationTarget);
		}
	}
}