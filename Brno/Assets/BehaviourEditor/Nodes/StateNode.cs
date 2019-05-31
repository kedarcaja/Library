using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/State")]
    public class StateNode : DrawNode
    {
        public override void DrawCurve(BaseNode b)
        {
            foreach (Transition t in b.transitions)
            {
                if (t == null) continue;
                BehaviourEditor.DrawNodeCurve(t, t.startNode.WindowRect, t.endNode.WindowRect, t.startPlacement, t.endPlacement, t.Color, t.disabled);
                t.DrawConnection(t.startNode, t.endNode, t.startPlacement, t.endPlacement, t.Color, t.disabled);
            }
        }
        public override void DrawWindow(BaseNode b)
        {
            if (b.stateRef.currentState == null)
            {
                EditorGUILayout.LabelField("Add state to modify:");
            }


            b.stateRef.currentState = (State)EditorGUILayout.ObjectField(b.stateRef.currentState, typeof(State), false);


            if (b.stateRef.currentState != null)
            {
                b.isAssigned = true;

                if (!b.collapse)
                {
                    if (b.stateRef.serializedState == null)
                    {
                        SetupReordableLists(b);

                    }
                    float standard = 150;
                    b.stateRef.serializedState.Update();
                    b.showActions = EditorGUILayout.Toggle("Show Update/Fixed ", b.showActions);
                    if (b.showActions)
                    {
                        
                        EditorGUILayout.LabelField("");
                        b.stateRef.onFixedList.DoLayoutList();
                        EditorGUILayout.LabelField("");
                        b.stateRef.onUpdateList.DoLayoutList();
                      standard += 100 + 40 + (b.stateRef.onUpdateList.count + b.stateRef.onFixedList.count) * 20;
                    }
                    b.showEnterExit = EditorGUILayout.Toggle("Show Enter/Exit ", b.showEnterExit);
                    if (b.showEnterExit)
                    {
                        EditorGUILayout.LabelField("");
                        b.stateRef.onEnterList.DoLayoutList();
                        EditorGUILayout.LabelField("");
                        b.stateRef.onExitList.DoLayoutList();
                        standard  += 100 + 40 + (b.stateRef.onEnterList.count + b.stateRef.onExitList.count) * 20;
                    }

                    b.stateRef.serializedState.ApplyModifiedProperties();
                    b.WindowRect.height = standard;
                }
            }
            else
            {
                b.isAssigned = false;
            }
        }

        void HandleReordableList(ReorderableList list, string targetName)
        {
            list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, targetName);
            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
            };
        }

        void SetupReordableLists(BaseNode b)
        {

            b.stateRef.serializedState = new SerializedObject(b.stateRef.currentState);
            b.stateRef.onFixedList = new ReorderableList(b.stateRef.serializedState, b.stateRef.serializedState.FindProperty("onFixed"), true, true, true, true);
            b.stateRef.onUpdateList = new ReorderableList(b.stateRef.serializedState, b.stateRef.serializedState.FindProperty("onUpdate"), true, true, true, true);
            b.stateRef.onEnterList = new ReorderableList(b.stateRef.serializedState, b.stateRef.serializedState.FindProperty("onEnter"), true, true, true, true);
            b.stateRef.onExitList = new ReorderableList(b.stateRef.serializedState, b.stateRef.serializedState.FindProperty("onExit"), true, true, true, true);

            HandleReordableList(b.stateRef.onFixedList, "On Fixed");
            HandleReordableList(b.stateRef.onUpdateList, "On Update");
            HandleReordableList(b.stateRef.onEnterList, "On Enter");
            HandleReordableList(b.stateRef.onExitList, "On Exit");
        }


    }

}