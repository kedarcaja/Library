using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum EWindowCurvePlacement { LeftTop, LeftBottom, CenterBottom, CenterTop, RightTop, RightBottom, RightCenter, LeftCenter, Center }

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
		public bool clicked = false; // vratit na object
        public string Value = null;
       
		public Transition(BaseNode start, BaseNode end, EWindowCurvePlacement sPos, EWindowCurvePlacement ePos, Color col, bool disable,string val)
		{
            if (start.transitions.Exists(t => t.endNode == end)) return;


			DrawConnection(start, end, sPos, ePos, col, disable);
			start.transitions.Add(this);
			end.depencies.Add(this);
			ID = start.GetTransitionId(end.WindowTitle[0]);
            this.Value = val;
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