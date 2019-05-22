using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [Serializable]
    public class BaseNode 
    {
        public int ID;
        public Rect WindowRect;
        public string WindowTitle;
        protected Color32 nodeColor;
        public List<Transition> transitions = new List<Transition>();
        public virtual void DrawWindow()
        {
            nodeColor = new Color32(nodeColor.r, nodeColor.g, nodeColor.b, 255);
            EditorGUI.DrawRect(new Rect(0, 17, WindowRect.width, WindowRect.height - 17), nodeColor);
            EditorGUILayout.LabelField("Node Color: ");
            nodeColor = EditorGUILayout.ColorField(nodeColor);

        }
        public void DrawCurve()
        {
            foreach (Transition t in transitions)
            {

                BehaviourEditor.DrawNodeCurve(t.Start.WindowRect, t.End.WindowRect, true, Color.red);
            }
        }

    }
    [Serializable]
    public class Transition
    {
        public BaseNode Start { get; set; }
        public BaseNode End { get; set; }
        public int ID { get; set; }

    }
}