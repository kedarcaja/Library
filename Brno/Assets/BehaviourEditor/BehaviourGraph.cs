using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
namespace BehaviourTreeEditor
{
	[CreateAssetMenu(menuName = "BehaviourEditor/Character Graph")]
	public class BehaviourGraph : ScriptableObject
	{
		public List<BaseNode> nodes = new List<BaseNode>();
		public List<string> removeNodesIDs = new List<string>();
		public List<SelectionZone> selectionZones = new List<SelectionZone>();
		public CharacterScript character;
		public void RemoveTransitions()
		{
			foreach (BaseNode b in nodes)
			{
				b?.RemoveTransitions();
			}
		}
		/// <summary>
		/// Adds new node to graph
		/// </summary>
		/// <param name="drawNode">template of node design and behaviour</param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="title">title of node</param>
		/// <returns></returns>
		public BaseNode AddNode(DrawNode drawNode, float x, float y, float width, float height, string title)
		{
			BaseNode n = new BaseNode(drawNode, x, y, width, height, title, GenerateNodeId());
			n.savedWindowRect = n.WindowRect;
			n.Graph = this;
			nodes.Add(n);
			return n;
		}
		private void OnEnable()
		{
			InitTransitions();
		}
		/// <summary>
		/// Adds nodes id to list of nodes to remove
		/// </summary>
		public void RemoveNodeSelectedNodes()
		{


			for (int i = 0; i < nodes.Count; i++)
			{
				if (nodes[i] == null) continue;

				if (removeNodesIDs.Contains(nodes[i].ID))
				{
					nodes.Remove(nodes[i]);
				}

			}
			removeNodesIDs.Clear();
		}

		/// <summary>
		/// Creates unique id for node
		/// </summary>
		/// <returns></returns>
		private string GenerateNodeId()
		{
			System.Random r = new System.Random();
			char[] a = { 'A', 'E', 'C', 'G', 'H', 'T', 'J' };
			return DateTime.Now.Second.ToString() + a[r.Next(0, 7)] + nodes.Count.ToString();
		}

		/// <summary>
		/// Initializes transitions end and start window after serialization
		/// </summary>
		public void InitTransitions()
		{

			foreach (BaseNode b in nodes)
			{
				if (b == null) continue;
				foreach (Transition t in b.transitions)
				{
					if (t == null) continue;
					BaseNode start = b;
					BaseNode end = null;
					nodes.ForEach(e => e.depencies.ForEach(d => { if (d.ID == t.ID) { end = e; }; }));
					t.DrawConnection(start, end, t.startPlacement, t.endPlacement, t.Color, t.disabled);
				}
			}
		}
	}
	[Serializable]
	public class SelectionZone
	{
		public Rect Rect;
		public string title;
		public bool collapsed = false;
		public List<BaseNode> selectedNodes = new List<BaseNode>();
		public bool restored = false, hidden = true;
		public Color Color = Color.white;
		public bool Ready = false;
		public SelectionZone(Rect r, string title, Color color)
		{
			Rect = r;
			this.title = title;
			Color = color;
		}
		public void Drag(Vector2 delta)
		{
			Rect.position += delta;
			foreach (BaseNode z in selectedNodes)
			{
				z.WindowRect.position += delta;
			}
		}
		public bool IsEmpty()
		{
			return Ready && selectedNodes.Count == 0;
		}
		public void CheckSelectedNodes(List<BaseNode> l)
		{
			if (collapsed) return;
			Ready = false;
			if (l.All(a => a.WindowRect.size != Vector2.zero && restored))
				selectedNodes.Clear();
			for (int i = 0; i < l.Count; i++)
			{
				BaseNode b = l[i];
				if ((Rect.Overlaps(b.WindowRect) && !BehaviourEditor.currentGraph.selectionZones.Exists(z => z.selectedNodes.Contains(b))))
				{
					selectedNodes.Add(b);
					foreach (Transition t in b.transitions)
					{
						selectedNodes.Add(t.endNode);
					}
					foreach (Transition t in b.depencies)
					{
						selectedNodes.Add(t.startNode);
					}
				}
			}
			Ready = true;
		}
		public void Draw()
		{

			if (!collapsed)
			{
				EditorGUI.DrawRect(Rect, Color);
				title = EditorGUI.TextArea(new Rect(Rect.center.x, Rect.yMin + 5, 80, 20), title, GColor.White);
				Color = EditorGUI.ColorField(new Rect(Rect.xMin + 20, Rect.yMin + 25, 80, 20), Color);
				if (!restored)
				{
					for (int i = 0; i < selectedNodes.Count; i++)
					{
						BaseNode b = selectedNodes[i];

						b.WindowRect.size = b.savedWindowRect.size;
					}
					restored = true;
					hidden = false;
				}
			}
			else
			{
				EditorGUI.DrawRect(new Rect(Rect.xMin, Rect.yMin, 100, 25), new Color(Color.r, Color.g, Color.b, 0.3f));
				 EditorGUI.LabelField(new Rect(Rect.xMin + 20, Rect.yMin + 5, 80, 25), title);
				if (!hidden)
				{
					for (int i = 0; i < selectedNodes.Count; i++)
					{
						BaseNode b = selectedNodes[i];
						b.savedWindowRect = b.WindowRect;
						b.WindowRect.size = Vector2.zero;
					}
					hidden = true;
					restored = false;
				}

			}
			collapsed = GUI.Toggle(new Rect(Rect.xMin, Rect.yMin + 5, 100, 25), collapsed, "");

		}
	}
}
