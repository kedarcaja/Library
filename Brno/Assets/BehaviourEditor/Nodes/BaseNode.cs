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
        #region Variables

        public string ID;
        public Rect WindowRect;
        public string WindowTitle;
        public Color32 nodeColor = Color.grey;
        public bool collapse = false;
        public float normalHeight = 0;
        public DrawNode drawNode;
        public bool isAssigned;
        public bool isDuplicate = false;
        public bool previousCollapse;
        [SerializeField]
        public List<Transition> transitions = new List<Transition>();
        [SerializeField]
        public List<Transition> depencies = new List<Transition>();
        public List<string> transitionsIdsToRemove = new List<string>();

        #endregion

        #region Comment node Variables
        public string comment = "";
        #endregion
        #region State node Variables
        [SerializeField]
        public StateNodeReferences stateRef;
        public bool showActions = false;
        public bool showEnterExit = false;
        #endregion
        #region Condition node Variables
        public Condition condition;
        #endregion
        public string GetTransitionId(char end)
        {
            return "T"+ DateTime.Now.Second.ToString() + transitions.Count.ToString() + end;
        }
        public BaseNode(DrawNode draw, float x, float y, float width, float height, string title, string id)
        {
            ID = id;
            WindowRect = new Rect(x, y, width, height);
            WindowTitle = title;
            normalHeight = height;
            drawNode = draw;
            stateRef = new StateNodeReferences();
        }

        public void AddTransitionsToRemove(string id)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].ID == id)
                {
                    transitionsIdsToRemove.Add(id);
                }
            }
        }
        public void RemoveTransitions()
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitionsIdsToRemove.Contains(transitions[i].ID))
                {
                    transitions.Remove(transitions[i]);
                }
            }
        }
        public void DrawWindow()
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
            drawNode?.DrawWindow(this);

        }

        public void DrawCurve()
        {
            drawNode?.DrawCurve(this);
        }
    }
    [Serializable]
    public class StateNodeReferences
    {
        public State currentState;
        [HideInInspector]
        public State previousState;
        public SerializedObject serializedState;
        public ReorderableList onFixedList;
        public ReorderableList onUpdateList;
        public ReorderableList onEnterList;
        public ReorderableList onExitList;

    }
    
}

