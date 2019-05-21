using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    public class StateNode : BaseNode
    {
        public override void DrawWindow()
        {
            base.DrawWindow();
         //   EditorGUI.DrawRect(new Rect(0, 16, WindowRect.width, WindowRect.height - 16), Color.red);
         //   GUI.DrawTexture(new Rect(WindowRect.width / 2 - 3, WindowRect.height / 2 - 16, 100, 100), (Texture)Resources.Load<Texture>("rtg"));
        }
    }
}
