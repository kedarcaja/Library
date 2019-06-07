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
}
