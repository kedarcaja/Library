using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviourTreeEditor
{
    public class BehaviourEditor : EditorWindow
    {

        Vector3 mousePosition;
        bool makeTransition;
        bool clickedOnWindow;
        BaseNode selectedNode;
        public static CharacterGraph currentCharacter;

        public enum UserActions { addState, addTransitionNode, deleteNode, commentNode }


        [MenuItem("Behaviour Editor/Editor")]
        static void ShowEditor()
        {
            BehaviourEditor editor = EditorWindow.GetWindow<BehaviourEditor>();
            editor.minSize = new Vector2(800, 600);
        }

        private void OnGUI()
        {
            Event e = Event.current;
            mousePosition = e.mousePosition;
            UserInput(e);
            DrawWindows();

        }





        private void ModifyNode(Event e)
        {
            GenericMenu menu = new GenericMenu();

            if (selectedNode is StateNode)
            {
                StateNode stateNode = (StateNode)selectedNode;
                if (stateNode != null)
                {
                    menu.AddItem(new GUIContent("Add Transition"), false, ContextCallback, UserActions.addTransitionNode);

                }
                else
                {
                    menu.AddDisabledItem(new GUIContent("Add Transition"));
                }
                menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);

            }


            menu.ShowAsContext();
            e.Use();
        }
        private void UserInput(Event e)
        {

            if (e.button == 1 && !makeTransition)
            {
                if (e.type == EventType.MouseDown)
                {
                    RightClick(e);
                }
            }
            if (e.button == 0 && !makeTransition)
            {
                if (e.type == EventType.MouseDown)
                {
                }
                if (e.type == EventType.MouseDrag)
                {

                }
            }
        }
        private void RightClick(Event e)
        {
            if (currentCharacter == null) return;
            selectedNode = null;
            clickedOnWindow = false;

            for (int i = 0; i < currentCharacter.nodes.Count; i++)
            {
                if (currentCharacter.nodes[i].WindowRect.Contains(e.mousePosition))
                {
                    clickedOnWindow = true;
                    selectedNode = currentCharacter.nodes[i];
                    break;
                }
            }
            if (!clickedOnWindow)
            {
                AddNewNode(e);
            }
            else
            {
                ModifyNode(e);
            }
        }
        private void ContextCallback(object o)
        {
            UserActions a = (UserActions)o;

            switch (a)
            {
                case UserActions.addState:
                    StateNode s = currentCharacter.AddNode<StateNode>(mousePosition.x, mousePosition.y, 100, 200, "State") as StateNode;
                    break;

                case UserActions.addTransitionNode:
                    break;

                case UserActions.commentNode:
                    break;

                case UserActions.deleteNode:
                    currentCharacter.RemoveNode(selectedNode.ID);
                    break;


            }
              EditorUtility.SetDirty(currentCharacter);
        }
        public void DrawWindows()
        {
            BeginWindows();
            EditorGUILayout.LabelField(" ", GUILayout.Width(100));
            EditorGUILayout.LabelField("Assign Character:", GUILayout.Width(100));
           currentCharacter = (CharacterGraph)EditorGUILayout.ObjectField(currentCharacter, typeof(CharacterGraph), false, GUILayout.Width(200));
            if (currentCharacter == null)
            {
                GUILayout.Label("No Character Assign!", GUILayout.Width(150));
                return;
            }
            for (int i = 0; i < currentCharacter.nodes.Count; i++)
            {
                currentCharacter.nodes[i].WindowRect = GUI.Window(i, currentCharacter.nodes[i].WindowRect, DrawNodeWindow, currentCharacter.nodes[i].WindowTitle);
            }
            EndWindows();
        }
        void DrawNodeWindow(int id)
        {
            if (currentCharacter != null)
            {
                currentCharacter.nodes[id].DrawWindow();
                GUI.DragWindow();
            }
        }
        void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add State"), false, ContextCallback, UserActions.addState);
            menu.ShowAsContext();
            e.Use();
        }

        public static void DrawNodeCurve(Rect start, Rect end, bool left, Color curveColor)
        {
            Vector3 startPos = new Vector3(left ? start.x + start.width : start.x, start.y + (start.height * 0.5f), 0);
            Vector3 endPos = new Vector3(end.x + (end.width * 0.5f), end.y + (end.height * 0.5f), 0);
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;
            Color shadow = new Color(0, 0, 0, 0.6f);
            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos, startTan, endTan, shadow, null, (i + 1) * 0.5f);
            }
            Handles.DrawBezier(startPos, endPos, startTan, endTan, curveColor, null, 1);

        }

    }

}