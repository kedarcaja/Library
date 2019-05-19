using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
namespace BehaviourTreeEditor
{
    [Serializable]
    public class BaseNode
    {
       // [HideInInspector]
        public DrawNode drawNode;
        //[HideInInspector]
        public Rect windowRect;
       // [HideInInspector]
        public string windowTitle;
        public StateNodeReferences stateRef;

        public void DrawWindow()
        {
            if(drawNode != null)
            {
                drawNode.DrawWindow(this);
            }
        }
        public void DrawCurve()
        {
            if (drawNode != null)
            {
                drawNode.DrawCurve(this);
            }
        }

    }
    [Serializable]
    public class StateNodeReferences
    {
        [HideInInspector]
        public bool collapse;
        [HideInInspector]
        public bool isDuplicate;
        [HideInInspector]
        public bool previousCollapse;
        [HideInInspector]
        public State currentState;
        [HideInInspector]
        public State previousState;
        [HideInInspector]
        public SerializedObject serializedState;
        [HideInInspector]
        public ReorderableList onStateList;
        [HideInInspector]
        public ReorderableList onExitList;
        [HideInInspector]
        public ReorderableList onEnterList;
        [HideInInspector]
        public List<BaseNode> depencies = new List<BaseNode>();
    }
}