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
        [HideInInspector]
        public CharacterGraph CharacterGraph;
        public int ID;
        public Rect WindowRect;
        public string WindowTitle;
        public Color32 nodeColor = Color.grey;
        public List<Transition> transitions = new List<Transition>();
        public bool collapse = false;
        public float normalHeight = 0;
        public int TransitionsIds = 0;
        public List<Transition> depencies = new List<Transition>();
        public Type nodeType = null;


        public virtual void DrawWindow()
        {
            EditorGUI.DrawRect(new Rect(0, 17, WindowRect.width, WindowRect.height - 17), nodeColor);
            EditorGUILayout.LabelField("Node Color: ");
            nodeColor = EditorGUILayout.ColorField(nodeColor);
            collapse = EditorGUILayout.Toggle(collapse);
            if (collapse)
            {
                WindowRect.height = 100;
            }
            else
            {
                WindowRect.height = normalHeight;

            }

        }
        public bool IsTransitionDuplicateOrSelve(Transition tr)
        {
            return tr.StartNode.ID == tr.EndNode.ID || transitions.Exists(f => f.StartNode.ID == tr.StartNode.ID && f.EndNode.ID == tr.EndNode.ID);
        }
        public virtual void DrawCurve()
        {
            foreach (Transition t in transitions)
            {

                BehaviourEditor.DrawNodeCurve(t.StartNode.WindowRect, t.EndNode.WindowRect, true, t.CurveColor, "", Color.black, Vector3.zero);
            }
        }
    }
    [Serializable]
    public class Transition
    {
        public BaseNode StartNode, EndNode;
        public Rect End;
        public Rect Start;
        public Color CurveColor = Color.black;
        public bool ReadyToDraw { get; set; }
        public int ID = 0;


        public Transition(BaseNode start, BaseNode end, Rect start_, Rect end_)
        {
            Start = start_;
            End = end_;
            StartNode = start;
            EndNode = end;
            ID = start.transitions.Count;
            StartNode.CharacterGraph.Saved = false;

        }

    }

}

