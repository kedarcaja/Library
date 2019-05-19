using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace BehaviourTreeEditor
{
    public class StateNode : BaseNode
    {
        public bool collapse;
        public bool isDuplicate;
        bool previousCollapse;
        public State currentState;
        public State previousState;
        SerializedObject serializedState;
        ReorderableList onStateList;
        ReorderableList onExitList;
        ReorderableList onEnterList;
        public List<BaseNode> depencies = new List<BaseNode>();
        public override void DrawCurve()
        {

        }
        public override void DrawWindow()
        {
            if (currentState == null)
            {
                EditorGUILayout.LabelField("Add State to modify: ");
            }
            else
            {
                if (!collapse)
                {
                }
                else
                {
                    windowRect.height = 100;

                }
                collapse = EditorGUILayout.Toggle("collapse: ", collapse);
            }
            currentState = (State)EditorGUILayout.ObjectField(currentState, typeof(State), false);
            if (previousCollapse != collapse)
            {
                previousCollapse = collapse;
                 //BehaviourEditor.currentGraph.SetNode(this);
                 //
            }
            if (previousState != currentState)
            {
                serializedState = null;
                isDuplicate = BehaviourEditor.currentGraph.IsStateNodeDuplicate(this);
                if (!isDuplicate)
                {
                    BehaviourEditor.currentGraph.SetNode(this);
                    previousState = currentState;

                    for (int i = 0; i < currentState.transitions.Count; i++)
                    {
                    }
                }
            }
            if (isDuplicate)
            {
                EditorGUILayout.LabelField("State is a duplicate!");
                windowRect.height = 100;
                return;
            }
            if (currentState != null)
            {
                if (serializedState == null)
                {
                    serializedState = new SerializedObject(currentState);
                    onStateList = new ReorderableList(serializedState, serializedState.FindProperty("onState"), true, true, true, true);
                    onExitList = new ReorderableList(serializedState, serializedState.FindProperty("onExit"), true, true, true, true);
                    onEnterList = new ReorderableList(serializedState, serializedState.FindProperty("onEnter"), true, true, true, true);
                }
                if (!collapse)
                {
                    serializedState.Update();
                    HandleReordableList(onStateList, "On State");
                    HandleReordableList(onEnterList, "On Enter");
                    HandleReordableList(onExitList, "On Exit");
                    EditorGUILayout.LabelField("");
                    onStateList.DoLayoutList();
                    EditorGUILayout.LabelField("");
                    onExitList.DoLayoutList();
                    EditorGUILayout.LabelField("");
                    onEnterList.DoLayoutList();
                    serializedState.ApplyModifiedProperties();

                    float standard = 300;
                    standard += (onStateList.count) * 20;
                    windowRect.height = standard;

                }
            }
        }
        void HandleReordableList(ReorderableList l, string targetName)
        {
            l.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, targetName);

            };
            l.drawElementCallback = (Rect r, int index, bool isActive, bool isFocused) =>
                 {
                     var el = l.serializedProperty.GetArrayElementAtIndex(index);
                     EditorGUI.ObjectField(new Rect(r.x, r.y, r.width, EditorGUIUtility.singleLineHeight), el, GUIContent.none);


                 };
        }
        public Transition AddTransition()
        {
            return currentState.AddTransition();
        }
        public void ClearReferences()
        {
            BehaviourEditor.ClearWindowsFromList(depencies);
            depencies.Clear();
        }

    }
}