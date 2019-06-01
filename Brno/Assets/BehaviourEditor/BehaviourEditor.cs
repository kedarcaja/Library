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
    public enum EWindowCurvePlacement { LeftTop, LeftBottom, CenterBottom, CenterTop, RightTop, RightBottom, RightCenter, LeftCenter, Center }
    public class BehaviourEditor : EditorWindow
    {


        Vector3 mousePosition;
        static bool clickedOnWindow;
        public static BaseNode selectedNode;
        public static BehaviourGraph currentGraph;
        public static bool isMakingTransition = false;
        public EditorSettings settings;
        Rect all = new Rect(0, 0, 10000, 10000); // window 
        bool showSettings = false;
        static Transition selectedTransition;
        #region Window Handle Variables
        private const float kZoomMin = 0.3f;
        private const float kZoomMax = 1f;
        private readonly Rect _zoomArea = new Rect(0, 0, 10000, 10000);
        private float _zoom = 1.0f;
        private Vector2 _zoomCoordsOrigin = Vector2.zero;
        Vector2 scrollPos;
        Vector2 scrollStartPos;
        public GUIStyle style;

        #endregion
        public enum UserActions
        {
            deleteNode, commentNode, stateNode, makeTransition, conditionNode
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
            HandleZoom(e);
            UserInput(e);
            DrawWindows();
            if (GUI.changed)
            {

                Repaint();
            }

            if (isMakingTransition)
            {
                DrawNodeCurve(null, selectedNode.WindowRect, new Rect(mousePosition.x, mousePosition.y, 20, 20), EWindowCurvePlacement.Center, EWindowCurvePlacement.Center, Color.black, false);
                Repaint();
            }

        }
        private void OnEnable()
        {
            settings = Resources.Load("Editor/Settings", typeof(EditorSettings)) as EditorSettings;
            style = settings.skin.GetStyle("window");
            titleContent.text = "BehaviourEditor";
        }



        #region Window Handle Methods
        private Vector2 ConvertScreenCoordsToZoomCoords(Vector2 screenCoords)
        {
            return (screenCoords - _zoomArea.TopLeft()) / _zoom + _zoomCoordsOrigin;
        }
        private void HandleZoom(Event e)
        {
            // Allow adjusting the zoom with the mouse wheel as well. In this case, use the mouse coordinates
            // as the zoom center instead of the top left corner of the zoom area. This is achieved by
            // maintaining an origin that is used as offset when drawing any GUI elements in the zoom area.
            if (e.type == EventType.ScrollWheel)
            {
                Vector2 screenCoordsMousePos = mousePosition;
                Vector2 delta = e.delta;
                Vector2 zoomCoordsMousePos = ConvertScreenCoordsToZoomCoords(screenCoordsMousePos);
                float zoomDelta = -delta.y / 150.0f;
                float oldZoom = _zoom;
                _zoom += zoomDelta;
                _zoom = Mathf.Clamp(_zoom, kZoomMin, kZoomMax);
                _zoomCoordsOrigin += (zoomCoordsMousePos - _zoomCoordsOrigin) - (oldZoom / _zoom) * (zoomCoordsMousePos - _zoomCoordsOrigin);

                e.Use();
            }

            // Allow moving the zoom area's origin by dragging with the middle mouse button or dragging
         
        }
        #endregion

        private void ModifyNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);
            if (selectedNode.condition != null && (selectedNode.drawNode is ConditionNode) && selectedNode.transitions.Count < 2 || !(selectedNode.drawNode is ConditionNode))
                menu.AddItem(new GUIContent("Make Transition"), false, ContextCallback, UserActions.makeTransition);
            menu.ShowAsContext();
            e.Use();
        }
        private void UserInput(Event e)
        {
            if (currentGraph == null) return;
            clickedOnWindow = false;
            BaseNode start = selectedNode;

            if ((e.button == 0 || e.button == 1) && e.type == EventType.MouseDown)
            {
                if (selectedTransition != null && !new Rect(10, 150, 200, 300).Contains(mousePosition))
                {
                    selectedTransition.clicked = false;
                    selectedTransition = null;
                    Repaint();

                }
                for (int i = 0; i < currentGraph.nodes.Count; i++)
                {
                    if (currentGraph.nodes[i].WindowRect.Contains(e.mousePosition))
                    {
                        clickedOnWindow = true;
                        selectedNode = currentGraph.nodes[i];
                        break;
                    }
                }
            }

            // with the left mouse button with Alt pressed.
            if (e.type == EventType.MouseDrag && (e.button == 0 && e.modifiers == EventModifiers.Alt) || e.button == 2)
            {
                Vector2 delta = e.delta;
                delta /= _zoom;
                _zoomCoordsOrigin += delta;

                e.Use();
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
                        MakeTransition(start, end);
                    }
                }

            }
        }


        private void RightClick(Event e)
        {
            if (currentGraph == null) return;

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
                case UserActions.commentNode:
                    currentGraph.AddNode(settings.CommentNode, mousePosition.x, mousePosition.y, 200, 200, "Comment");
                    break;
                case UserActions.stateNode:
                    currentGraph.AddNode(settings.StateNode, mousePosition.x, mousePosition.y, 200, 300, "State");
                    break;
                case UserActions.deleteNode:
                    currentGraph.removeNodesIDs.Add(selectedNode.ID);
                    break;
                case UserActions.makeTransition:
                    isMakingTransition = true;
                    break;
                case UserActions.conditionNode:
                    currentGraph.AddNode(settings.ConditionNode, mousePosition.x, mousePosition.y, 200, 200, "Condition");
                    break;
            }
            EditorUtility.SetDirty(currentGraph);

        }
        public void DrawWindows()
        {
            EditorGUI.LabelField(new Rect(10, 30, 200, 50), "Character: ");
            GUILayout.BeginArea(new Rect(10, 50, 200, 100));
            currentGraph = (BehaviourGraph)EditorGUILayout.ObjectField(currentGraph, typeof(BehaviourGraph), false, GUILayout.Width(200)); // field to choose graph
            GUILayout.EndArea();
            if (currentGraph == null)
            {
                EditorGUI.LabelField(new Rect(10, 70, 200, 50), "No Character Assign!", GetTextStyleColor(Color.red));
                goto end; // cannot be return because Windows and GL.Area must be closed
            }


            EditorZoomArea.Begin(_zoom, _zoomArea);
            GUILayout.BeginArea(all, style);
            BeginWindows();
            if (currentGraph != null)
            {
                currentGraph?.RemoveNodeSelectedNodes();

                foreach (BaseNode n in currentGraph.nodes)
                {
                    n.DrawCurve(); // drawing transitions
                }
            }
            for (int i = 0; i < currentGraph.nodes.Count; i++)
            {
                currentGraph.nodes[i].WindowRect = GUI.Window(i, currentGraph.nodes[i].WindowRect, DrawNodeWindow, currentGraph.nodes[i].WindowTitle); // setting up nodes as windows
            }
            end:
            EndWindows();
            GUILayout.EndArea();
            EditorZoomArea.End();

            currentGraph?.RemoveTransitions();


        }
        void DrawNodeWindow(int id)
        {

            currentGraph?.nodes[id].DrawWindow();

            GUI.DragWindow();

        }
        void AddNewNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add Comment"), false, ContextCallback, UserActions.commentNode);
            menu.AddItem(new GUIContent("Add State"), false, ContextCallback, UserActions.stateNode);
            menu.AddItem(new GUIContent("Add Condition"), false, ContextCallback, UserActions.conditionNode);

            menu.ShowAsContext();
            e.Use();
        }
        public void MakeTransition(BaseNode start, BaseNode end)
        {
            Transition t;
            if (start.drawNode is ConditionNode)
            {
                if (start.transitions.Count < 2)
                {
                    ConditionNode c = start.drawNode as ConditionNode;
                    GenericMenu menu = new GenericMenu();

                    if (!start.transitions.Exists(x => (bool)x.Value == true))
                        menu.AddItem(new GUIContent("True"), false, delegate { t = new Transition(start, end, EWindowCurvePlacement.RightBottom, EWindowCurvePlacement.LeftCenter, Color.red, false); t.Value = true; });
                    if (!start.transitions.Exists(x => (bool)x.Value == false))
                        menu.AddItem(new GUIContent("False"), false, delegate { t = new Transition(start, end, EWindowCurvePlacement.LeftBottom, EWindowCurvePlacement.LeftCenter, Color.blue, false); t.Value = false; });
                    menu.ShowAsContext();
                }
            }
            else
            {
                t = new Transition(start, end, EWindowCurvePlacement.RightCenter, EWindowCurvePlacement.LeftCenter, Color.magenta, false);
            }
            isMakingTransition = false;
        }
        public static void DrawNodeCurve(Transition t, Rect start, Rect end, EWindowCurvePlacement start_, EWindowCurvePlacement end_, Color curveColor, bool disable)
        {
            Vector3 endPos = Vector3.zero;
            Vector3 startPos = Vector3.zero;
            switch (start_)
            {
                case EWindowCurvePlacement.LeftTop:
                    startPos = new Vector3(start.x + 1, start.y + 3, 0);

                    break;
                case EWindowCurvePlacement.LeftBottom:
                    startPos = new Vector3(start.x + 1, start.y + start.height - 3, 0);
                    break;
                case EWindowCurvePlacement.CenterBottom:
                    startPos = new Vector3(start.x + (start.width * 0.5f), start.y + start.height - 2, 0);

                    break;
                case EWindowCurvePlacement.CenterTop:
                    startPos = new Vector3(start.x + (start.width * 0.5f), start.y + 2, 0);
                    break;
                case EWindowCurvePlacement.RightTop:
                    startPos = new Vector3(start.x + start.width, start.y + 3, 0);
                    break;
                case EWindowCurvePlacement.RightBottom:
                    startPos = new Vector3(start.x + start.width, start.y + start.height - 3, 0);
                    break;
                case EWindowCurvePlacement.RightCenter:
                    startPos = new Vector3(start.x + start.width, start.y + (start.height * 0.5f), 0);
                    break;
                case EWindowCurvePlacement.LeftCenter:
                    startPos = new Vector3(start.x, start.y + (start.height * 0.5f), 0);

                    break;
                case EWindowCurvePlacement.Center:
                    startPos = new Vector3(start.x + (start.width * 0.5f), start.y + (start.height * 0.5f), 0);
                    break;

            }
            switch (end_)
            {
                case EWindowCurvePlacement.LeftTop:
                    endPos = new Vector3(end.x + 1, end.y + 3, 0);

                    break;
                case EWindowCurvePlacement.LeftBottom:
                    endPos = new Vector3(end.x + 1, end.y + end.height - 3, 0);
                    break;
                case EWindowCurvePlacement.CenterBottom:
                    endPos = new Vector3(end.x + (end.width * 0.5f), end.y + end.height - 2, 0);

                    break;
                case EWindowCurvePlacement.CenterTop:
                    endPos = new Vector3(end.x + (end.width * 0.5f), end.y + 2, 0);
                    break;
                case EWindowCurvePlacement.RightTop:
                    endPos = new Vector3(end.x + end.width, end.y + 3, 0);
                    break;
                case EWindowCurvePlacement.RightBottom:
                    endPos = new Vector3(end.x + end.width, end.y + end.height - 3, 0);
                    break;
                case EWindowCurvePlacement.RightCenter:
                    endPos = new Vector3(end.x + end.width, end.y + (end.height * 0.5f), 0);
                    break;
                case EWindowCurvePlacement.LeftCenter:
                    endPos = new Vector3(end.x, end.y + (end.height * 0.5f), 0);

                    break;
                case EWindowCurvePlacement.Center:
                    endPos = new Vector3(end.x + (end.width * 0.5f), end.y + (end.height * 0.5f), 0);
                    break;

            }


            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;


            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 4);
            }
            Handles.DrawBezier(startPos, endPos, startTan, endTan, disable ? Color.black : curveColor, null, 3);
            DrawTransitionClickPoint(t, startPos, endPos);
        }
        public static void DrawTransitionClickPoint(Transition t, Vector3 start, Vector3 end)
        {

            if (t == null) return;
            Handles.color = t.Color;

            if (Handles.Button((start + end) * .5f, Quaternion.identity, 4, 8, Handles.DotHandleCap))
            {
                t.clicked = !t.clicked;
            }
            if (t.clicked)
            {
                if (selectedTransition != null && selectedTransition != t)
                {
                    selectedTransition.clicked = false;

                }
                selectedTransition = t;

                EditorGUI.DrawRect(new Rect(10, 150, 200, 300), new Color32(188, 188, 188, 255));

                GUILayout.BeginArea(new Rect(10, 150, 200, 300));

                EditorGUILayout.LabelField("Transition settings: ");
                EditorGUILayout.LabelField("color: ");
                t.Color = EditorGUILayout.ColorField(t.Color);

                EditorGUILayout.LabelField("start position: ");
                t.startPlacement = (EWindowCurvePlacement)EditorGUILayout.EnumPopup(t.startPlacement);

                EditorGUILayout.LabelField("end position: ");
                t.endPlacement = (EWindowCurvePlacement)EditorGUILayout.EnumPopup(t.endPlacement);

                EditorGUILayout.LabelField("Value: " + t?.Value?.ToString());
                GUILayout.EndArea();
                if (GUI.Button(new Rect(10, 300, 80, 20), "Remove"))
                {

                    t.startNode.AddTransitionsToRemove(t.ID);
                }
            }
        }
        public static GUIStyle GetTextStyleColor(Color color)
        {
            GUIStyle s = new GUIStyle();
            s.normal.textColor = color;
            return s;
        }
    }
    public static class RectExtensions
    {
        public static Vector2 TopLeft(this Rect rect)
        {
            return new Vector2(rect.xMin, rect.yMin);
        }
        public static Rect ScaleSizeBy(this Rect rect, float scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }
        public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale;
            result.xMax *= scale;
            result.yMin *= scale;
            result.yMax *= scale;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }
        public static Rect ScaleSizeBy(this Rect rect, Vector2 scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }
        public static Rect ScaleSizeBy(this Rect rect, Vector2 scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale.x;
            result.xMax *= scale.x;
            result.yMin *= scale.y;
            result.yMax *= scale.y;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }
    }
    public class EditorZoomArea
    {
        private const float kEditorWindowTabHeight = 21.0f;
        private static Matrix4x4 _prevGuiMatrix;

        public static Rect Begin(float zoomScale, Rect screenCoordsArea)
        {
            GUI.EndGroup();        // End the group Unity begins automatically for an EditorWindow to clip out the window tab. This allows us to draw outside of the size of the EditorWindow.

            Rect clippedArea = screenCoordsArea.ScaleSizeBy(1.0f / zoomScale, screenCoordsArea.TopLeft());
            clippedArea.y += kEditorWindowTabHeight;
            GUI.BeginGroup(clippedArea);

            _prevGuiMatrix = GUI.matrix;
            Matrix4x4 translation = Matrix4x4.TRS(clippedArea.TopLeft(), Quaternion.identity, Vector3.one);
            Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));
            GUI.matrix = translation * scale * translation.inverse * GUI.matrix;

            return clippedArea;
        }

        public static void End()
        {
            GUI.matrix = _prevGuiMatrix;
            GUI.EndGroup();
            GUI.BeginGroup(new Rect(0.0f, kEditorWindowTabHeight, Screen.width, Screen.height));
        }
    }

}