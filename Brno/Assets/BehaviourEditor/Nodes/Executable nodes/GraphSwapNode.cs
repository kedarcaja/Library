using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Nodes/Graph Swap")]
    public class GraphSwapNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode node)
        {
        }

        public override void DrawWindow(BaseNode b)
        {
            b.swapGraph = EditorGUILayout.ObjectField(b.swapGraph, typeof(BehaviourGraph), false) as BehaviourGraph;
        }

        public override void Execute(BaseNode b)
        {
            b.Graph.character.currentGraph = b.swapGraph;
            b.Graph.character.InitGraph();
            b.nodeCompleted = true;
        }
    }
}