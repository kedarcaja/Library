using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviourTreeEditor
{
    public class BehaviourEditor : EditorWindow
    {
        #region Variables
        Vector3 mousePosition;
        bool makeTransition;
        bool clickedOnWindow;
        int selectedIndex;
        BaseNode selectedNode;

        public static EditorSettings settings;

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
            settings = Resources.Load("EditorSettings") as EditorSettings;

        }
        private void RightClick(Event e)
        {
            selectedIndex = -1;
            clickedOnWindow = false;
            for (int i = 0; i < settings.currentGraph.windows.Count; i++)
            {
                if (settings.currentGraph.windows[i].windowRect.Contains(e.mousePosition))
                {
                    clickedOnWindow = true;
                    selectedNode = settings.currentGraph.windows[i];
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
                    BaseNode baseNode = new BaseNode();
                    baseNode.windowRect.width = 200;
                    baseNode.windowRect.height = 100;
                    baseNode.drawNode = settings.stateNode;
                    baseNode.windowTitle = "State Node";
                    settings.currentGraph.windows.Add(baseNode);
                    break;
                case UserActions.addTransitionNode:
                    break;
                case UserActions.deleteNode:
                    break;
                case UserActions.commentNode:
                    break;

            }
            EditorUtility.SetDirty(settings);
        }
        private void LeftClick(Event e)
        {

        }
        private void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddSeparator("");
            if (settings.currentGraph != null)
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
            //GenericMenu menu = new GenericMenu();

            //if (selectedNode is StateNode)
            //{
            //    StateNode stateNode = (StateNode)selectedNode;
            //    if (stateNode.currentState != null)
            //    {
            //        menu.AddItem(new GUIContent("Add Transition"), false, ContextCallback, UserActions.addTransitionNode);

            //    }
            //    else
            //    {
            //        menu.AddDisabledItem(new GUIContent("Add Transition"));
            //    }
            //    menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);

            //}
            //if (selectedNode is TransitionNode)
            //{
            //    menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);

            //}
            //if (selectedNode is CommentNode)
            //{
            //    menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);

            //}

            //menu.ShowAsContext();
            //e.Use();
        }
        private void UserInput(Event e)
        {
            if (settings.currentGraph == null)
                return;
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
                    for (int i = 0; i < settings.currentGraph.windows.Count; i++)
                    {
                        if (settings.currentGraph.windows[i].windowRect.Contains(e.mousePosition))
                        {
                            //if (settings.currentGraph != null)
                            //    settings.currentGraph.SetNode(settings.currentGraph.windows[i]);
                            //break;
                        }
                    }
                }
            }
        }
        private void DrawWindows()
        {
            BeginWindows();
            EditorGUILayout.LabelField("", GUILayout.Width(100));
            EditorGUILayout.LabelField("Assign Graph: ", GUILayout.Width(100));
            settings.currentGraph = (BehaviourGraph)EditorGUILayout.ObjectField(settings.currentGraph, typeof(BehaviourGraph), false, GUILayout.Width(200));
            if (settings.currentGraph != null)
            {
                foreach (BaseNode n in settings.currentGraph.windows)
                {
                    n.DrawCurve();
                }
                for (int i = 0; i < settings.currentGraph.windows.Count; i++)
                {
                    settings.currentGraph.windows[i].windowRect = GUI.Window(i, settings.currentGraph.windows[i].windowRect, DrawNodeWindow, settings.currentGraph.windows[i].windowTitle);
                }
            }
            EndWindows();
        }
        private void DrawNodeWindow(int id)
        {
            settings.currentGraph.windows[id].DrawWindow();
            GUI.DragWindow();
        }
        //public static StateNode AddStateNode(Vector2 pos)
        //{
        ////    StateNode stateNode = CreateInstance<StateNode>();

        ////    stateNode.windowRect = new Rect(pos.x, pos.y, 200, 300);
        ////    stateNode.windowTitle = "State";

        ////    settings.currentGraph.windows.Add(stateNode);
        ////    //  settings.currentGraph.SetStateNode(stateNode);
        ////    return stateNode;
        ////}
        ////public static CommentNode AddCommentNode(Vector2 pos)
        ////{
        ////    CommentNode st = CreateInstance<CommentNode>();
        ////    st.windowRect = new Rect(pos.x, pos.y, 200, 100);
        ////    st.windowTitle = "Comment";
        ////    settings.currentGraph.windows.Add(st);
        ////    return st;
        ////}
        ////public static TransitionNode AddTransitionNode(int index, Transition trans, StateNode from)
        ////{
        ////    Rect fromRect = from.windowRect;
        ////    fromRect.x += 50;
        ////    float targetY = fromRect.y - fromRect.height;
        ////    if (from.currentState != null)
        ////    {
        ////        targetY = (index * 100);
        ////    }
        ////    fromRect.y = targetY;
        ////    fromRect.x += +100 + 200;
        ////    fromRect.y += (fromRect.height * 0.7f);

        ////    return AddTransitionNode(new Vector2(fromRect.x, fromRect.y), trans, from);
        //}
        //public static TransitionNode AddTransitionNode(Vector2 pos, Transition trans, StateNode from)
        //{
        //    TransitionNode node = CreateInstance<TransitionNode>();
        //    node.Init(from, trans);
        //    node.windowRect = new Rect(pos.x, pos.y, 200, 80);
        //    node.windowTitle = "Condition Check";
        //    settings.currentGraph.windows.Add(node);
        //    from.depencies.Add(node);
        //    return node;
        //}
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
                if (settings.currentGraph.windows.Contains(l[i]))
                {
                    settings.currentGraph.windows.Remove(l[i]);
                }
            }
        }
    }
}