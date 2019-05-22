using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviourTreeEditor
{
    public class BehaviourEditor : EditorWindow
    {

        Vector3 mousePosition;
        bool clickedOnWindow;
        BaseNode selectedNode;
        public static CharacterGraph currentCharacter;
        bool isMakingTransition = false;

        public enum UserActions { stateNode, deleteNode, commentNode, conditionNode, makeTransition }


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
            menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);
            if (selectedNode is StateNode)
            {
                menu.AddItem(new GUIContent("Make Transition"), false, ContextCallback, UserActions.makeTransition);

            }

            menu.ShowAsContext();
            e.Use();
        }
        private void UserInput(Event e)
        {

            if (e.button == 1)
            {
                if (e.type == EventType.MouseDown)
                {
                    RightClick(e);
                }
            }
            if (e.button == 0)
            {
                if (e.type == EventType.MouseDown)
                {
                    if (isMakingTransition)
                    {

                        BaseNode start = selectedNode;
                        if (clickedOnWindow)
                        {
                            for (int i = 0; i < currentCharacter.nodes.Count; i++)
                            {
                                if (currentCharacter.nodes[i].WindowRect.Contains(e.mousePosition))
                                {
                                    clickedOnWindow = true;
                                    selectedNode = currentCharacter.nodes[i];
                                    break;
                                }
                            }
                            isMakingTransition = false;

                            Transition t = new Transition()
                            {
                                Start = start,
                                End = selectedNode,



                            };
                            if (start.transitions.Exists(c => c.Start == t.Start && c.End == t.End) || t.Start == t.End) return;
                            start.transitions.Add(t);

                        }
                    }
                }
                if (e.type == EventType.MouseDrag)
                {

                }
            }
        }
        private void RightClick(Event e)
        {
            if (currentCharacter == null) return;
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
                isMakingTransition = false;

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
                case UserActions.stateNode:
                    currentCharacter.AddNode<StateNode>(mousePosition.x, mousePosition.y, 200, 300, "State");
                    break;
                case UserActions.commentNode:
                    currentCharacter.AddNode<CommentNode>(mousePosition.x, mousePosition.y, 200, 150, "Comment");
                    break;
                case UserActions.conditionNode:
                    currentCharacter.AddNode<ConditionNode>(mousePosition.x, mousePosition.y, 200, 150, "Condition");
                    break;

                case UserActions.deleteNode:
                    selectedNode.transitions.Clear();
                    currentCharacter.RemoveNode(selectedNode.ID);
                    foreach (BaseNode b in currentCharacter.nodes)
                    {
                        foreach (Transition t in b.transitions)
                        {
                            if (t.End == selectedNode)
                            {
                                b.transitions.Remove(t);
                            }
                        }
                    }
                    break;
                case UserActions.makeTransition:
                    isMakingTransition = true;
                    break;


            }
            EditorUtility.SetDirty(currentCharacter);
        }

        public void DrawWindows()
        {

            BeginWindows();
            EditorGUI.DrawRect(new Rect(0, 0, 250, 80), new Color32(47, 50, 56, 255));
            EditorGUI.LabelField(new Rect(0, 0, 200, 50), "Character:");
            currentCharacter = (CharacterGraph)EditorGUILayout.ObjectField(currentCharacter, typeof(CharacterGraph), false, GUILayout.Width(200));

            if (currentCharacter == null)
            {
                GUILayout.Label("No Character Assign!", GUILayout.Width(150));
                return;
            }
            else
            {
                foreach (BaseNode n in currentCharacter.nodes)
                {
                    n.DrawCurve();
                }

                EditorGUI.DrawRect(new Rect(210, 0, 80, 80), new Color32(44, 47, 53, 255));
                GUI.DrawTexture(new Rect(210, 0, 70, 70), currentCharacter.Character.Portait.texture);

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
            }
            GUI.DragWindow();

        }
        void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add State"), false, ContextCallback, UserActions.stateNode);
            menu.AddItem(new GUIContent("Add Comment"), false, ContextCallback, UserActions.commentNode);
            menu.AddItem(new GUIContent("Add Condition"), false, ContextCallback, UserActions.conditionNode);
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