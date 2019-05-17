using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
	[CreateAssetMenu(menuName = "BehaviourEditor/Conditions/IsDead")]
	public class IsDead : Condition
	{
		public override bool CheckedCondition(StateManager state)
		{
			return state.health <= 0;
		}
	}
}