using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    public abstract class DrawNode : ScriptableObject
    {
        public abstract void DrawWindow(BaseNode node);
        public abstract void DrawCurve(BaseNode node);

		
    }
}