using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace BehaviourTreeEditor
{
	public enum ECondition { IsThunder, IsSunnyDay, IsAlive, IsTimeToGoToWork, IsDead, IsNight, IsMorning, IsPlayerClose, ReachedDestination, }
	[CreateAssetMenu(menuName = "BehaviourEditor/Nodes/Condition")]
	public class ConditionNode : DrawNode
	{

		public override void DrawCurve(BaseNode b)
		{

		}

		public override void DrawWindow(BaseNode b)
		{
#if UNITY_EDITOR

			b.condition = (ECondition)EditorGUILayout.EnumPopup(b.condition);

#endif
		}
		
	}

}
