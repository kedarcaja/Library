using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    public abstract class DrawNode : ScriptableObject
    {

        public float Width = 200, Height = 200;
        public Color NodeColor = Color.grey;
        public abstract void DrawWindow(BaseNode node);
        public abstract void DrawCurve(BaseNode node);


    }
}