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
        public event Action OnDestroy,OnCreate;
        [NonSerialized]
        public List<Transition> transitions = new List<Transition>();
        public bool collapse = false;
        public float normalHeight = 0;
        public int TransitionsIds = 0;
        [NonSerialized]
        public List<Transition> depencies = new List<Transition>();
        public bool Enable = true;

        public BaseNode()
        {
            OnCreate?.Invoke();
            Enable = true;
        }
        public virtual void DrawWindow()
        {
            if (!Enable) return;
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
        public  bool IsTransitionDuplicateOrSelve(Transition tr)
        {
            return tr.StartNode.ID == tr.EndNode.ID || transitions.Exists(f => f.StartNode.ID == tr.StartNode.ID && f.EndNode.ID == tr.EndNode.ID);
        }
        public virtual void DrawCurve()
        {
            if (!Enable) return;

            foreach (Transition t in transitions)
            {
                if (!t.ReadyToDraw) continue;

                t.DrawConnection(Vector3.zero, Color.black, "", false);
            }
        }
        public void Destroy()
        {
            foreach (Transition t in transitions)
            {
                t.ReadyToDraw = false;
                t.Enable = false;
                if(this is ConditionNode)
                {
                    ConditionNode c = this as ConditionNode;
                    c.T_drawed = false;
                    c.F_drawed = false;
                }
                CharacterGraph.AddInitiTransitionToRemove(t.ID);
            }
            foreach (Transition t in depencies)
            {
                t.ReadyToDraw = false;
                t.Enable = false;
                CharacterGraph.AddInitiTransitionToRemove(t.ID);
                if (t.StartNode is ConditionNode)
                {
                    ConditionNode c = t.StartNode as ConditionNode;
                    c.TrueTransition = null;
                    c.FalseTransition = null;
                    c.T_drawed = false;
                    c.F_drawed = false;
                }
                t.StartNode.depencies.Remove(t);
            }

            depencies.Clear();
            transitions.Clear();
            OnDestroy?.Invoke();
            Enable = false;
            if (this is ConditionNode && CharacterGraph.conds.Contains(this as ConditionNode))
            {
                CharacterGraph.conds.Remove(this as ConditionNode);
            }
            if (this is CommentNode && CharacterGraph.coms.Contains(this as CommentNode))
            {
                CharacterGraph.coms.Remove(this as CommentNode);
            }
            if (this is StateNode && CharacterGraph.states.Contains(this as StateNode))
            {
                CharacterGraph.states.Remove(this as StateNode);

            }
            CharacterGraph.removeNodesIDs.Add(ID);
            
            
        }
    }
}

