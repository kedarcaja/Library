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
        public static BaseNode selectedNode;
        public static CharacterGraph currentCharacter;
        public static bool isMakingTransition = false;
        public enum UserActions
        {
            stateNode, deleteNode, commentNode, conditionNode, makeDefaultTransition,
            makeTrueTransition, makeFalseTransition, removeDefaultTransition, removeTrueTransition, removeFalseTransition
        }


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
            if (GUI.changed)
            {
                Repaint();
            }

        }




        private void ModifyNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);
            if(!(selectedNode is ConditionNode))
            menu.AddItem(new GUIContent("Make Transition"), false, ContextCallback, UserActions.makeDefaultTransition);

            if (selectedNode is ConditionNode)
            {
                ConditionNode node = (ConditionNode)selectedNode;

                if (node.TrueTransition == null && node.Condition != null)
                    menu.AddItem(new GUIContent("Add True"), false, ContextCallback, UserActions.makeTrueTransition);
                else
                    if (node.TrueTransition != null)
                    menu.AddItem(new GUIContent("Remove True"), false, ContextCallback, UserActions.removeTrueTransition);
                if (node.FalseTransition == null && node.Condition != null)
                    menu.AddItem(new GUIContent("Add False"), false, ContextCallback, UserActions.makeFalseTransition);
                else
                    if (node.FalseTransition != null)
                    menu.AddItem(new GUIContent("Remove False"), false, ContextCallback, UserActions.removeFalseTransition);

            }
            menu.ShowAsContext();
            e.Use();
        }
        private void UserInput(Event e)
        {
            if (currentCharacter == null) return;
            clickedOnWindow = false;
            BaseNode start = selectedNode;

            if ((e.button == 0 || e.button == 1) && e.type == EventType.MouseDown)
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
            }


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
                        BaseNode end = selectedNode;
                        Transition ct = new Transition(start, end, start.WindowRect, end.WindowRect);

                        if (start.IsTransitionDuplicateOrSelve(ct)) return; // checks if condition exists or is connected to its self

                        if (start is ConditionNode)
                        {
                            ConditionNode c = (ConditionNode)start;
                            if (c.CreatingTrueTransition)
                            {
                                c.TrueTransition = ct;

                            }
                            else if (c.CreatingFalseTransition)
                            {
                                c.FalseTransition = ct;

                            }
                            c.CreatingFalseTransition = false;
                            c.CreatingTrueTransition = false;
                        }
                        if (ct != null)
                        {
                            if (!(start is ConditionNode))
                            {
                                start.TransitionsIds++;
                                start.transitions.Add(ct);
                            }
                            end.depencies.Add(ct);
                            ct.ReadyToDraw = true;
                        }

                        isMakingTransition = false;

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

            if (!clickedOnWindow)
            {
                selectedNode = null;
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

                    ConditionNode c = currentCharacter.AddNode<ConditionNode>(mousePosition.x, mousePosition.y, 200, 150, "Condition");
                    if (selectedNode != null)
                    {
                        c.MainTransition = new Transition(selectedNode, c, selectedNode.WindowRect, c.WindowRect);
                        c.MainTransition.ReadyToDraw = true;
                    }
                    break;


                case UserActions.deleteNode:
                  
                    currentCharacter.AddToRemoveNode(selectedNode.ID);
                    break;

                case UserActions.makeDefaultTransition:
                    Transition t = new Transition(selectedNode,null,selectedNode.WindowRect,new Rect());
                    t.ReadyToDraw = true;
                    isMakingTransition = true;
                    break;

                case UserActions.makeTrueTransition:

                    if (!(selectedNode is ConditionNode)) return;

                    ConditionNode d = (ConditionNode)selectedNode;
                    isMakingTransition = true;
                    d.CreatingFalseTransition = false;
                    d.CreatingTrueTransition = true;
                    break;
                case UserActions.makeFalseTransition:


                    if (!(selectedNode is ConditionNode)) return;

                    ConditionNode x = (ConditionNode)selectedNode;
                    isMakingTransition = true;
                    x.CreatingFalseTransition = true;
                    x.CreatingTrueTransition = false;
                    break;

                case UserActions.removeFalseTransition:
                    if (selectedNode is ConditionNode)
                        (selectedNode as ConditionNode).FalseTransition.ReadyToDraw = false;
                    (selectedNode as ConditionNode).FalseTransition = null;
                    break;
                case UserActions.removeTrueTransition:
                    (selectedNode as ConditionNode).TrueTransition.ReadyToDraw = false;
                    (selectedNode as ConditionNode).TrueTransition = null;
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
                GUI.contentColor = Color.red;
                GUILayout.Label("No Character Assign!", GUILayout.Width(150));
                return;
            }
            else
            {
                currentCharacter.RetypeNodes();
                currentCharacter.RemoveNodeSelectedNodes();
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

        public static void DrawNodeCurve(Rect start, Rect end, bool left, Color curveColor, string lable, Color lableTextColor, Vector3 dir)
        {
            Vector3 startPos = new Vector3(left ? start.x + start.width : start.x, start.y + (start.height * 0.5f), 0);
            Vector3 endPos = new Vector3(end.x + (end.width * 0.5f), end.y + (end.height * 0.5f), 0);
            Vector3 startTan = startPos + dir * 50;
            Vector3 endTan = endPos + Vector3.left * 50;

            GUIStyle style = new GUIStyle();
            style.normal.textColor = lableTextColor;
            GUI.Label(new Rect(left ? startPos.x +20: startTan.x, startPos.y, 40, 20), lable, style);
            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos, startTan, endTan, curveColor, null, 4);
            }
            Handles.DrawBezier(startPos, endPos, startTan, endTan, curveColor, null, 3);

        }

      


    }
    
}