using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    public class BaseNode : ScriptableObject
    {
        public int ID { get; set; }
        public Rect WindowRect { get; set; }
        public string WindowTitle { get; set; }
        protected Color32 nodeColor;
        public List<Transition> transitions { get; protected set; } = new List<Transition>();
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
                
                BehaviourEditor.DrawNodeCurve(t.Start.WindowRect,t.End.WindowRect,true,Color.red);
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