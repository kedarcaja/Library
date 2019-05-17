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
		BaseNode selectedNode;
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
			//windows.Clear();
		}
		private void RightClick(Event e)
		{
			selectedNode = null;
			for (int i = 0; i < windows.Count; i++)
			{
				if (windows[i].windowRect.Contains(e.mousePosition))
				{
					clickedOnWindow = true;
					selectedNode = windows[i];
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
					StateNode stateNode = new StateNode()
					{
						windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 300),
						windowTitle = "State",
					};
					windows.Add(stateNode);
					break;
				case UserActions.addTransitionNode:
					if(selectedNode is StateNode)
					{
						StateNode sta = (StateNode)selectedNode;
						Transition t = sta.AddTransition();
					}
					
					break;
				case UserActions.deleteNode:
					if (selectedNode != null)
					{
						windows.Remove(selectedNode);
					}
					break;
				case UserActions.commentNode:
					CommentNode st = new CommentNode()
					{
						windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 100),
						windowTitle = "Comment",
					};
					windows.Add(st);
					break;
				default:
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
			menu.AddItem(new GUIContent("Add state"), false, ContextCallback, UserActions.addState);
			menu.AddItem(new GUIContent("Add Comment"), false, ContextCallback, UserActions.commentNode);
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
					LeftClick(e);
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
			TransitionNode node = CreateInstance<TransitionNode>();
			node.Init(from, trans);
			node.windowRect = new Rect(fromRect.x + 100 + 200, fromRect.y + (fromRect.height * 0.7f), 200, 80);
			node.windowTitle = "Condition Check";
			windows.Add(node);
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
	}
}