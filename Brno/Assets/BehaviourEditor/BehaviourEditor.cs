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
        #region Variables
        Vector3 mousePosition;
        static bool clickedOnWindow;
        public static BaseNode selectedNode;
        public static BehaviourGraph currentGraph;
        public static bool isMakingTransition = false;
        public static EditorSettings settings;
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
        private Vector2 offset;
        private Vector2 drag;
        #endregion


        public static CharacterScript currentCharacter;
        static State previousState;
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

        #region Unity Methods
        private void OnEnable()
        {
            settings = Resources.Load("Editor/Settings", typeof(EditorSettings)) as EditorSettings;
            style = settings.skin.GetStyle("window");
            titleContent.text = "BehaviourEditor";

        }
        private void OnGUI()
        {
            Event e = Event.current;
            EditorGUI.DrawRect(all, settings.backgroundColor);
            DrawGrid(_zoom * 10 + settings.gridSpacing, settings.gridOpacity, settings.gridColor);
            mousePosition = e.mousePosition;

            UserInput(e);
            DrawWindows();

            EditorGUI.DrawRect(new Rect(mousePosition, Vector2.one), Color.red);
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

        private bool CharacterSelected()
        {
            CharacterScript s = Selection.activeTransform.GetComponent<CharacterScript>();
            return ObjectSelected() && s != null && s.Graph != null && s.Graph;
        }
        private bool ObjectSelected()
        {
            return Selection.activeTransform != null;
        }
        #endregion
        #region Window Handle Methods
        void HandlePanning(Event e)
        {
            Vector2 diff = e.mousePosition - scrollStartPos;
            diff *= .6f;
            scrollStartPos = e.mousePosition;
            scrollPos += diff;

            for (int i = 0; i < currentGraph.nodes.Count; i++)
            {
                BaseNode b = currentGraph.nodes[i];
                b.WindowRect.x += diff.x;
                b.WindowRect.y += diff.y;
            }
            e.Use();
        }
        void ResetScroll()
        {
            for (int i = 0; i < currentGraph.nodes.Count; i++)
            {
                BaseNode b = currentGraph.nodes[i];
                b.WindowRect.x -= scrollPos.x;
                b.WindowRect.y -= scrollPos.y;
            }

            scrollPos = Vector2.zero;
        }
        private Vector2 ConvertScreenCoordsToZoomCoords(Vector2 screenCoords)
        {
            return (screenCoords - _zoomArea.TopLeft()) / _zoom + _zoomCoordsOrigin;
        }
        private void HandleZoom(Event e)
        {

            _zoomCoordsOrigin = e.mousePosition;
            Vector2 screenCoordsMousePos = e.mousePosition;
            Vector2 delta = e.delta;
            Vector2 zoomCoordsMousePos = ConvertScreenCoordsToZoomCoords(screenCoordsMousePos);
            float zoomDelta = -delta.y / 150.0f;
            float oldZoom = _zoom;
            _zoom += zoomDelta;
            _zoom = Mathf.Clamp(_zoom, kZoomMin, kZoomMax);
            _zoomCoordsOrigin += (zoomCoordsMousePos - _zoomCoordsOrigin) - (oldZoom / _zoom) * (zoomCoordsMousePos - _zoomCoordsOrigin);

            e.Use();

        }
        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }
        #endregion
        #region Input Handle Methods
        public void HandleHiearchySelection()
        {
            if (CharacterSelected())
            {
                currentCharacter = Selection.activeTransform.GetComponent<CharacterScript>();
                currentGraph = currentCharacter.Graph;
            }
            else
            {
                currentGraph = null;
            }
        }
        public void HandleGraphSelection(Event e)
        {
            clickedOnWindow = false;
            if (selectedTransition != null && !new Rect(10, 150, 200, 300).Contains(e.mousePosition))
            {
                selectedTransition.clicked = false;
                selectedTransition = null;
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
        private void UserInput(Event e)
        {

            if (ObjectSelected())
            {
                HandleHiearchySelection();
            }

            if (currentGraph == null) return;
        
            BaseNode start = selectedNode;

            if (e.button == 1)
            {
                if (e.type == EventType.MouseDown)
                {
                    HandleGraphSelection(e);

                    RightClick(e);
                }
            }
            if (e.button == 0)
            {
                if (e.type == EventType.MouseDown)
                {
                    HandleGraphSelection(e);

                    if (isMakingTransition)
                    {
                        BaseNode end = selectedNode;
                        MakeTransition(start, end);
                    }
                }

            }

            if (e.button == 2 || e.button == 0 && e.modifiers == EventModifiers.Alt)
            {
                if (e.type == EventType.MouseDown&& !clickedOnWindow)
                {
                    scrollStartPos = e.mousePosition;
                }
                else if (e.type == EventType.MouseDrag&& !clickedOnWindow)
                {
                    HandlePanning(e);
                }
            }
            if (e.Equals(Event.KeyboardEvent("delete")) && selectedNode != null)
            {
                GUI.FocusControl(null); // removes focus from graph gui
                currentGraph.removeNodesIDs.Add(selectedNode.ID);
                Repaint();
            }
            if (e.type == EventType.ScrollWheel)
            {
                HandleZoom(e);
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

        #endregion
        #region Node Methods
        public void DrawWindows()
        {
            if (currentGraph != null)
            {
                EditorZoomArea.Begin(_zoom, all);
                GUILayout.BeginArea(all /*style*/);
                BeginWindows();

                currentGraph?.RemoveNodeSelectedNodes();

                foreach (BaseNode n in currentGraph.nodes)
                {
                    n.DrawCurve(); // drawing transitions
                }

                for (int i = 0; i < currentGraph.nodes.Count; i++)
                {
                    currentGraph.nodes[i].WindowRect = GUI.Window(i, currentGraph.nodes[i].WindowRect, DrawNodeWindow, currentGraph.nodes[i].WindowTitle); // setting up nodes as windows
                }


                EndWindows();
                GUILayout.EndArea();
                EditorZoomArea.End();

                foreach (BaseNode b in currentGraph.nodes)
                {
                    foreach (Transition t in b.transitions)
                    {
                        DrawTransitionSettings(t);
                    }
                }
            }


            //Rect zone = new Rect(0, 0, 200, 100);
            //EditorGUI.DrawRect(zone, settings.otherGUIColor);
            //GUILayout.BeginArea(new Rect(zone.x + 2, zone.y + 2, zone.width, zone.height));
            //GetEGLLable("Character: ", GColor.White);
            //currentGraph = (BehaviourGraph)EditorGUILayout.ObjectField(currentGraph, typeof(BehaviourGraph), false, GUILayout.Width(200)); // field to choose graph



            if (!currentGraph)
            {

                GUILayout.BeginArea(new Rect(150, 300, 1920, 200));
                GUIStyle s = GColor.Red;
                s.fontSize = 150;
                GetEGLLable("No Character Assign!",s);

                GUILayout.EndArea();
            }
            else
            {
                Rect zone = new Rect(0, 0, currentCharacter.name.Length*40, 40);
                EditorGUI.DrawRect(zone, settings.otherGUIColor);
                GUILayout.BeginArea(new Rect(zone.x + 2, zone.y + 2, zone.width, zone.height));
                GUIStyle s = GColor.Magenta;
                s.fontSize = 20;
                GetEGLLable("Character: "+currentCharacter.name, s);
                GUILayout.EndArea();
            }



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
        private void ModifyNode(Event e)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Delete"), false, ContextCallback, UserActions.deleteNode);
            if (selectedNode.condition != null && (selectedNode.drawNode is ConditionNode) && selectedNode.transitions.Count < 2 || !(selectedNode.drawNode is ConditionNode))
                menu.AddItem(new GUIContent("Make Transition"), false, ContextCallback, UserActions.makeTransition);
            menu.ShowAsContext();
            e.Use();
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
        #endregion
        #region Transition Methods
        public static void DrawTransitionClickPoint(Transition t, Vector3 start, Vector3 end)
        {
            if (t == null) return;
            Handles.color = t.Color;

            if (Handles.Button((start + end) * .5f, Quaternion.identity, 4, 8, Handles.DotHandleCap))
            {
                t.clicked = !t.clicked;
            }

        }
        public void DrawTransitionSettings(Transition t)
        {
            if (t.clicked)
            {
                if (selectedTransition != null && selectedTransition != t)
                {
                    selectedTransition.clicked = false;

                }
                selectedTransition = t;

                EditorGUI.DrawRect(new Rect(10, 150, 200, 300), settings.otherGUIColor);

                GUILayout.BeginArea(new Rect(10, 150, 200, 300));
                GUIStyle s = GColor.White;
                GetEGLLable("Transition settings: ", s);
                GetEGLLable("color: ", s);
                t.Color = EditorGUILayout.ColorField(t.Color);

                GetEGLLable("start position: ", s);
                t.startPlacement = (EWindowCurvePlacement)EditorGUILayout.EnumPopup(t.startPlacement);

                GetEGLLable("end position: ", s);
                t.endPlacement = (EWindowCurvePlacement)EditorGUILayout.EnumPopup(t.endPlacement);

                GetEGLLable("Value: " + t?.Value?.ToString(), s);
                GUILayout.EndArea();
                if (GUI.Button(new Rect(10, 300, 80, 20), "Remove"))
                {
                    t.startNode.AddTransitionsToRemove(t.ID);
                }
            }
        }
        public void MakeTransition(BaseNode start, BaseNode end)
        {
            if (start == end) { isMakingTransition = false; return; };
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
        #endregion
        public static void GetEGLLable(string text, GUIStyle style)
        {
            EditorGUILayout.LabelField(text, style);
        }
    }
    #region Extending Classes
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
    public static class GColor
    {
        public static GUIStyle Red { get => GetTextStyleColor(Color.red); }
        public static GUIStyle Green { get => GetTextStyleColor(Color.green); }
        public static GUIStyle Blue { get => GetTextStyleColor(Color.blue); }
        public static GUIStyle Magenta { get => GetTextStyleColor(Color.magenta); }
        public static GUIStyle Grey { get => GetTextStyleColor(Color.grey); }
        public static GUIStyle White { get => GetTextStyleColor(Color.white); }
        public static GUIStyle Yellow { get => GetTextStyleColor(Color.yellow); }
        public static GUIStyle GuiSettings { get => GetTextStyleColor(BehaviourEditor.settings.otherGUIColor); }
        private static GUIStyle GetTextStyleColor(Color color)
        {
            GUIStyle s = new GUIStyle();
            s.normal.textColor = color;
            return s;
        }
    }
    #endregion
}