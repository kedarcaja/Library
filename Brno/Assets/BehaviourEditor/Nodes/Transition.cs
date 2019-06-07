using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
	[Serializable]
	public class Transition
	{
		public string ID = "";
		public Color Color = Color.green;
		[NonSerialized] // stops recusrsive serialization
		public BaseNode startNode, endNode;
		public bool disabled = false;
		public EWindowCurvePlacement startPlacement, endPlacement;
		public bool clicked = false;
		public object Value;


       
		public Transition(BaseNode start, BaseNode end, EWindowCurvePlacement sPos, EWindowCurvePlacement ePos, Color col, bool disable)
		{
            if (start.transitions.Exists(t => t.endNode == end)) return;


			DrawConnection(start, end, sPos, ePos, col, disable);
			start.transitions.Add(this);
			end.depencies.Add(this);
			ID = start.GetTransitionId(end.WindowTitle[0]);
		}
		public void DrawConnection(BaseNode start, BaseNode end, EWindowCurvePlacement sPos, EWindowCurvePlacement ePos, Color col, bool disable)
		{
			Color = col;
			startNode = start;
			endNode = end;
			disabled = disable;
			startPlacement = sPos;
			endPlacement = ePos;
		}



	}
}