using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Nodes/State Node")]
    public class StateNode : DrawNode
    {

        public override void DrawCurve(BaseNode b)
        {

        }
        public override void DrawWindow(BaseNode b)
        {
            if (b.stateRef.currentState == null)
            {
                EditorGUILayout.LabelField("Add State to modify: ");
            }
            else
            {
                if (!b.stateRef.collapse)
                {
                }
                else
                {
                    b.windowRect.height = 100;

                }
                b.stateRef.collapse = EditorGUILayout.Toggle("collapse: ", b.stateRef.collapse);
            }
            b.stateRef.currentState = (State)EditorGUILayout.ObjectField(b.stateRef.currentState, typeof(State), false);
            if (b.stateRef.previousCollapse != b.stateRef.collapse)
            {
                b.stateRef.previousCollapse = b.stateRef.collapse;
                //BehaviourEditor.currentGraph.SetNode(this);
                //
            }
            if (b.stateRef.previousState != b.stateRef.currentState)
            {
                b.stateRef.serializedState = null;
                b.stateRef.isDuplicate = BehaviourEditor.settings.currentGraph.IsStateNodeDuplicate(this);
                if (!b.stateRef.isDuplicate)
                {
                    //BehaviourEditor.currentGraph.SetNode(this);
                    b.stateRef.previousState = b.stateRef.currentState;

                    for (int i = 0; i < b.stateRef.currentState.transitions.Count; i++)
                    {
                    }
                }

            }
            if (b.stateRef.isDuplicate)
            {
                EditorGUILayout.LabelField("State is a duplicate!");
                b.windowRect.height = 100;
                return;
            }

            if (b.stateRef.currentState != null)
            {
                if (b.stateRef.serializedState == null)
                {
                    b.stateRef.serializedState = new SerializedObject(b.stateRef.currentState);
                    b.stateRef.onStateList = new ReorderableList(b.stateRef.serializedState, b.stateRef.serializedState.FindProperty("onState"), true, true, true, true);
                    b.stateRef.onExitList = new ReorderableList(b.stateRef.serializedState, b.stateRef.serializedState.FindProperty("onExit"), true, true, true, true);
                    b.stateRef.onEnterList = new ReorderableList(b.stateRef.serializedState, b.stateRef.serializedState.FindProperty("onEnter"), true, true, true, true);
                }
                if (!b.stateRef.collapse)
                {
                    b.stateRef.serializedState.Update();
                    HandleReordableList(b.stateRef.onStateList, "On State");
                    HandleReordableList(b.stateRef.onEnterList, "On Enter");
                    HandleReordableList(b.stateRef.onExitList, "On Exit");
                    EditorGUILayout.LabelField("");
                    b.stateRef.onStateList.DoLayoutList();
                    EditorGUILayout.LabelField("");
                    b.stateRef.onExitList.DoLayoutList();
                    EditorGUILayout.LabelField("");
                    b.stateRef.onEnterList.DoLayoutList();
                    b.stateRef.serializedState.ApplyModifiedProperties();

                    float standard = 300;
                    standard += (b.stateRef.onStateList.count) * 20;
                    b.windowRect.height = standard;

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
        //public Transition AddTransition()
        //{
        //   return b.currentState.AddTransition();
        //}
        //public void ClearReferences()
        //{
        //    BehaviourEditor.ClearWindowsFromList(depencies);
        //    depencies.Clear();
        //}


    }
}