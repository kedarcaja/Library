using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTreeEditor
{
	[CreateAssetMenu(menuName ="BehaviourEditor/State")]
	public class State : ScriptableObject
	{
		public List<Transition> transitions = new List<Transition>();
		public void Tick()
		{

		}
		public Transition AddTransition()
		{
			Transition t = new Transition();
			transitions.Add(t);
			return t;
		}
	}
}