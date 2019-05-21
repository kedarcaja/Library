using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/State")]
    public class StateNode : BaseNode
    {
        public StateNode(Rect windowRect, string title) : base(windowRect, title)
        {
            WindowRect = windowRect;
            WindowTitle = title;

        }

        public StateNode(Vector2 position, Vector2 size, string title) : base(position, size, title)
        {
            WindowRect = new Rect(position.x, position.y, size.x, size.y);
            WindowTitle = title;
        }

        public StateNode(float x, float y, float width, float height, string title) : base(x, y, width, height, title)
        {
            WindowRect = new Rect(x, y, width, height);
            WindowTitle = title;

        }
        public override void DrawWindow()
        {
            EditorGUILayout.LabelField("Add State to modify: ");
        }
    }
}
