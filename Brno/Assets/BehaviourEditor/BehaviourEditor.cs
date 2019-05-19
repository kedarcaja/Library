using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    public class BehaviourEditor : EditorWindow
    {
        #region Variables
        static List<BaseNode> windows = new List<BaseNode>();
        Vector3 mousePosition;
        bool makeTransition;
        bool clickedOnWindow;
        int selectedIndex;
        BaseNode selectedNode;
        public static BehaviourGraph currentGraph;
        static GraphNode graphNode;
        public enum UserActions { addState, addTransitionNode, deleteNode, commentNode }
        #endregion


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
        private void OnEnable()
        {
            if (graphNode == null)
            {
                graphNode = CreateInstance<GraphNode>();
                graphNode.windowRect = new Rect(10, position.height * 0.7f, 200, 100);
                graphNode.windowTitle = "Graph";
            }
            windows.Clear();
            windows.Add(graphNode);
            LoadGraph();

        }
        private void RightClick(Event e)
        {
            selectedIndex = -1;
            clickedOnWindow = false;
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].windowRect.Contains(e.mousePosition))
                {
                    clickedOnWindow = true;
                    selectedNode = windows[i];
                    selectedIndex = i;
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
                    AddStateNode(mousePosition);
                    break;
                case UserActions.addTransitionNode:
                    if (selectedNode is StateNode)
                    {
                        StateNode from = (StateNode)selectedNode;
                        Transition t = from.AddTransition();
                        AddTransitionNode(from.currentState.transitions.Count, t, from);
                    }
                    break;

                case UserActions.deleteNode:
                    if (selectedNode is StateNode)
                    {
                        StateNode target = (StateNode)selectedNode;
                        target.ClearReferences();
                        windows.Remove(target);
                    }
                    if (selectedNode is TransitionNode)
                    {
                        TransitionNode target = (TransitionNode)selectedNode;
                        windows.Remove(target);
                        if (target.enterState.currentState.transitions.Contains(target.targetTransition))
                        {
                            target.enterState.currentState.transitions.Remove(target.targetTransition);
                        }

                    }
                    if (selectedNode is CommentNode)
                    {
                        windows.Remove(selectedNode);
                    }


                    break;
                case UserActions.commentNode:
                    AddCommentNode(mousePosition);
                    break;

            }
        }
        private void LeftClick(Event e)
        {

        }
        private void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddSeparator("");
            if (currentGraph != null)
            {


                menu.AddItem(new GUIContent("Add state"), false, ContextCallback, UserActions.addState);
                menu.AddItem(new GUIContent("Add Comment"), false, ContextCallback, UserActions.commentNode);
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Add State"));
                menu.AddDisabledItem(new GUIContent("Add Comment"));
            }
            menu.ShowAsContext();
            e.Use();
        }
        private void ModifyNode(Event e)
        {
            GenericMenu menu = new GenericMenu();

            if (selectedNode is StateNode)
            {
                StateNode stateNode = (StateNode)selectedNode;
                if (stateNode.currentState != null)
                {
                    menu.AddItem(new GUIContent("Add Transition"), false, ContextCallback, UserActions.addTransitionNode);

                }
                else
                {
                    menu.AddDisabledItem(new GUIContent("Add Transition"));
                }
                menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);

            }
            if (selectedNode is TransitionNode)
            {
                menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);

            }
            if (selectedNode is CommentNode)
            {
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
                    for (int i = 0; i < windows.Count; i++)
                    {
                        if (windows[i].windowRect.Contains(e.mousePosition))
                        {
                            if (currentGraph != null)
                                currentGraph.SetNode(windows[i]);
                            break;
                        }
                    }
                }
            }
        }
        private void DrawWindows()
        {
            BeginWindows();
            foreach (BaseNode n in windows)
            {
                n.DrawCurve();
            }
            for (int i = 0; i < windows.Count; i++)
            {
                windows[i].windowRect = GUI.Window(i, windows[i].windowRect, DrawNodeWindow, windows[i].windowTitle);
            }
            EndWindows();
        }
        private void DrawNodeWindow(int id)
        {
            windows[id].DrawWindow();
            GUI.DragWindow();
        }
        public static StateNode AddStateNode(Vector2 pos)
        {
            StateNode stateNode = CreateInstance<StateNode>();

            stateNode.windowRect = new Rect(pos.x, pos.y, 200, 300);
            stateNode.windowTitle = "State";

            windows.Add(stateNode);
            currentGraph.SetStateNode(stateNode);
            return stateNode;
        }
        public static CommentNode AddCommentNode(Vector2 pos)
        {
            CommentNode st = CreateInstance<CommentNode>();
            st.windowRect = new Rect(pos.x, pos.y, 200, 100);
            st.windowTitle = "Comment";
            windows.Add(st);
            return st;
        }
        public static TransitionNode AddTransitionNode(int index, Transition trans, StateNode from)
        {
            Rect fromRect = from.windowRect;
            fromRect.x += 50;
            float targetY = fromRect.y - fromRect.height;
            if (from.currentState != null)
            {
                targetY = (index * 100);
            }
            fromRect.y = targetY;
            fromRect.x += +100 + 200;
            fromRect.y += (fromRect.height * 0.7f);

            return AddTransitionNode(new Vector2(fromRect.x, fromRect.y), trans, from);
        }
        public static TransitionNode AddTransitionNode(Vector2 pos, Transition trans, StateNode from)
        {
            TransitionNode node = CreateInstance<TransitionNode>();
            node.Init(from, trans);
            node.windowRect = new Rect(pos.x, pos.y, 200, 80);
            node.windowTitle = "Condition Check";
            windows.Add(node);
            from.depencies.Add(node);
            return node;
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
        public static void ClearWindowsFromList(List<BaseNode> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (windows.Contains(l[i]))
                {
                    windows.Remove(l[i]);
                }
            }
        }
        public static void LoadGraph()
        {
            windows.Clear();
            windows.Add(graphNode);
            if (currentGraph == null)
                return;
            currentGraph.Init();
            List<Saved_StateNode> l = new List<Saved_StateNode>();
            l.AddRange(currentGraph.savedStateNodes);
            currentGraph.savedStateNodes.Clear();
            for (int i = l.Count - 1; i >= 0; i--)
            {
                StateNode s = AddStateNode(l[i].position);
                s.currentState = l[i].state;
                s.collapse = l[i].isCollepsed;
                currentGraph.SetStateNode(s);
                // load transitions

            }

        }
    }
}