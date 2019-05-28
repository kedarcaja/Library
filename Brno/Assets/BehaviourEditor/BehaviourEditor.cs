using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviourTreeEditor
{
    public class BehaviourEditor : EditorWindow
    {

        Vector2 scrollPos;
        Vector2 scrollStartPos;
        Vector3 mousePosition;
        bool clickedOnWindow;
        public static BaseNode selectedNode;
        public static CharacterGraph currentCharacter;
        public static bool isMakingTransition = false;
        public EditorSettings settings;
        Rect all = new Rect(-5, -5, 10000, 10000); // window 

        public GUIStyle style;
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

            if (isMakingTransition)
            {
                DrawNodeCurve(selectedNode.WindowRect, new Rect(mousePosition.x, mousePosition.y, 20, 20), true, Color.black, "", Color.black, Vector3.zero);
                Repaint();
            }

        }
        private void OnEnable()
        {
            ResetScroll();
            settings = Resources.Load("Editor/Settings", typeof(EditorSettings)) as EditorSettings;
            style = settings.skin.GetStyle("window");
            titleContent.text = "BehaviourEditor";

        }
        void ResetScroll()
        {
            for (int i = 0; i < currentCharacter?.nodes.Count; i++)
            {
                BaseNode b = currentCharacter.nodes[i];
                b.WindowRect.x -= scrollPos.x;
                b.WindowRect.y -= scrollPos.y;
            }

            scrollPos = Vector2.zero;
        }
        void HandlePanning(Event e)
        {
            Vector2 diff = e.mousePosition - scrollStartPos;
            diff *= .6f;
            scrollStartPos = e.mousePosition;
            scrollPos += diff;

            for (int i = 0; i < currentCharacter.nodes.Count; i++)
            {
                BaseNode b = currentCharacter.nodes[i];
                b.WindowRect.x += diff.x;
                b.WindowRect.y += diff.y;
            }
        }
        private void ModifyNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);
            if (!(selectedNode is ConditionNode))
                menu.AddItem(new GUIContent("Make Transition"), false, ContextCallback, UserActions.makeDefaultTransition);

            if (selectedNode is ConditionNode)
            {
                ConditionNode node = (ConditionNode)selectedNode;

                if (!node.T_drawed && node.Condition != null)
                    menu.AddItem(new GUIContent("Add True"), false, ContextCallback, UserActions.makeTrueTransition);
                else
                    if (node.T_drawed)
                    menu.AddItem(new GUIContent("Remove True"), false, ContextCallback, UserActions.removeTrueTransition);
                if (!node.F_drawed && node.Condition != null)
                    menu.AddItem(new GUIContent("Add False"), false, ContextCallback, UserActions.makeFalseTransition);
                else
                    if (node.F_drawed)
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
                        Transition ct = new Transition(start, end, start.TransitionsIds);


                        if (start.IsTransitionDuplicateOrSelve(ct) || (start is ConditionNode && (start as ConditionNode).IsTransitionInSameState(ct))||!start.CanBeConnectedTo(end))
                        {
                            isMakingTransition = false;
                            if (start is ConditionNode)
                            {
                                ConditionNode c = start as ConditionNode; c.CreatingFalseTransition = false;
                                c.CreatingTrueTransition = false;
                            }
                            return;
                        } // checks if condition exists or is connected to its self or if start is condition checks if true and false are not on same state


                        start.TransitionsIds++;
                        currentCharacter.InitTransitions.Add(ct);

                        if (start is ConditionNode)
                        {
                            ConditionNode c = (ConditionNode)start;
                            if (c.CreatingTrueTransition)
                            {
                                c.TrueTransition = ct;
                                c.TrueTransition.ID = ct.ID;
                                c.T_drawed = true;
                            }
                            else if (c.CreatingFalseTransition)
                            {
                                c.FalseTransition = ct;
                                c.FalseTransition.ID = ct.ID;
                                c.F_drawed = true;
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

            if (e.button == 2 && !clickedOnWindow)
            {
                if (e.type == EventType.MouseDown)
                {
                    scrollStartPos = e.mousePosition;
                }
                else if (e.type == EventType.MouseDrag)
                {
                    HandlePanning(e);
                    Repaint();
                }
                else if (e.type == EventType.MouseUp)
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
                    c.FalseTransition = null;
                    c.TrueTransition = null;



                    //if (selectedNode != null)
                    //{
                    //    c.MainTransition = new Transition(selectedNode, c, selectedNode.TransitionsIds);
                    //    selectedNode.TransitionsIds++;
                    //    c.MainTransition.ReadyToDraw = true;
                    //}
                    break;


                case UserActions.deleteNode:
                    selectedNode.Destroy();
                    break;

                case UserActions.makeDefaultTransition:
                    Transition t = new Transition(selectedNode, null, selectedNode.TransitionsIds);
                    selectedNode.TransitionsIds++;
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

                    ConditionNode s = (selectedNode as ConditionNode);
                    s.FalseTransition.ReadyToDraw = false;
                    s.FalseTransition = null;
                    s.F_drawed = false;

                    currentCharacter?.AddInitiTransitionToRemove(s.FalseTransition.ID);
                    break;
                case UserActions.removeTrueTransition:
                    ConditionNode p = (selectedNode as ConditionNode);
                    p.TrueTransition.ReadyToDraw = false;
                    p.T_drawed = false;
                    p.TrueTransition = null;
                    currentCharacter?.AddInitiTransitionToRemove(p.TrueTransition.ID);
                    break;
            }
            EditorUtility.SetDirty(currentCharacter);

        }
        public void DrawWindows()
        {
           
            GUILayout.BeginArea(all, style);
            BeginWindows();

            EditorGUI.LabelField(new Rect(10, 30, 200, 50), "Character:");
            GUILayout.BeginArea(new Rect(10, 50, 200, 100));
            currentCharacter = (CharacterGraph)EditorGUILayout.ObjectField(currentCharacter, typeof(CharacterGraph), false, GUILayout.Width(200)); // field to choose graph
            GUILayout.EndArea();


            if (currentCharacter == null)
            {

                GUIStyle s = new GUIStyle();
                s.normal.textColor = Color.red;
                s.richText = true;
                EditorGUI.LabelField(new Rect(10, 70, 200, 50), "No Character Assign!", s);
                goto end; // cannot be return because Windows and GL.Area must be closed
            }

            else
            {
                currentCharacter?.RemoveNodeSelectedNodes();

                foreach (BaseNode n in currentCharacter.nodes)
                {
                    n.DrawCurve(); // drawing transitions
                }

                GUI.DrawTexture(new Rect(210, 20, 70, 70), currentCharacter.Character.Portait.texture); // drawing portait of character
            }

            for (int i = 0; i < currentCharacter.nodes.Count; i++)
            {
                currentCharacter.nodes[i].WindowRect = GUI.Window(i, currentCharacter.nodes[i].WindowRect, DrawNodeWindow, currentCharacter.nodes[i].WindowTitle); // setting up nodes as windows
            }
            currentCharacter?.RemoveInitTransitions();

            end:
            EndWindows();
            GUILayout.EndArea();
        }
        void DrawNodeWindow(int id)
        {

            currentCharacter?.nodes[id].DrawWindow();

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
            GUI.Label(new Rect(left ? startPos.x + 20 : startTan.x, startPos.y, 40, 20), lable, style);
            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos, startTan, endTan, curveColor, null, 4);
            }
            Handles.DrawBezier(startPos, endPos, startTan, endTan, curveColor, null, 3);
        }

    }

}