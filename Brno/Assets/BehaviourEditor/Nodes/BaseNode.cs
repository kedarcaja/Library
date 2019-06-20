using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR

using UnityEditorInternal;
#endif
using UnityEngine;
public enum EAnimatorActivator { Trigger, Float, Bool, Int }
namespace BehaviourTreeEditor
{
    [Serializable]
    public class BaseNode
    {
        #region Variables
        public BehaviourGraph Graph;
        public string ID;
        public Rect WindowRect;
        public string WindowTitle;
        public Color32 nodeColor = Color.grey;
        public Color32 savedNodeColor = Color.grey;
        public bool collapse = false;
        public float normalHeight = 0;
        public DrawNode drawNode;
        public bool previousCollapse;
        [SerializeField]
        public List<Transition> transitions = new List<Transition>();
        [SerializeField]
        public List<Transition> depencies = new List<Transition>();
        public List<string> transitionsIdsToRemove = new List<string>();
        public Rect savedWindowRect;
        public bool nodeCompleted = false;
        #endregion

        #region Comment node Variables
        public string comment = "";
        #endregion

        #region Condition node Variables
        public ECondition condition;
        #endregion
        #region Animator nodes Variables

        public string parameter;
        public EAnimatorActivator AnimatorActivatorType;
        public bool AnimatorActivatorBoolValue;
        public int AnimatorActivatorIntValue;
        public float AnimatorActivatorFloatValue;
        public int animationLayer = 0;

        #region Animator swap nodes Variables
        public RuntimeAnimatorController animatorController;
        #endregion
        #endregion
        #region Timing nodes Variables
        public _Timer timer;
        public float delay;
        #endregion
        #region Random move nodes Variables
        public string randomMoveArea;
        public bool randomSet = false;
        #endregion
        #region Portal nodes Variables
        public string portalTargetNodeID;
        #endregion

        #region SetDestination node Variables
        public Transform destinationTarget;
        public string destinationTargetName;
        #endregion

        public string GetTransitionId(char end)
        {
            return "T" + DateTime.Now.Second.ToString() + transitions.Count.ToString() + end;
        }
        public BaseNode(DrawNode draw, float x, float y, float width, float height, string title, string id)
        {
            ID = id;
            WindowRect = new Rect(x, y, width, height);
            WindowTitle = title;
            normalHeight = height;
            drawNode = draw;
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
#if UNITY_EDITOR

            EditorGUI.DrawRect(new Rect(0, 17, WindowRect.width, WindowRect.height - 17), nodeColor);
            EditorGUILayout.LabelField("Node Color: ", GColor.White);
            if (this == BehaviourEditor.currentGraph.LiveCycle.currentNode)
            {
                savedNodeColor = EditorGUILayout.ColorField(nodeColor);


            }
            else
            {
                nodeColor = EditorGUILayout.ColorField(nodeColor);
            }

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
#endif
        }

        public void DrawCurve()
        {
            foreach (Transition t in transitions)
            {
                if (t == null || t.endNode == null || t.startNode == null || WindowRect.size == Vector2.zero) continue;
#if UNITY_EDITOR

                BehaviourEditor.DrawNodeCurve(t, t.startNode.WindowRect, t.endNode.WindowRect, t.startPlacement, t.endPlacement, t.Color, t.disabled);
#endif
                t.DrawConnection(t.startNode, t.endNode, t.startPlacement, t.endPlacement, t.Color, t.disabled);
            }
            drawNode?.DrawCurve(this);
        }
        public void Execute()
        {
            if (!(drawNode is ExecutableNode)) return;

            (drawNode as ExecutableNode).Execute(this);
        }
    }
}

