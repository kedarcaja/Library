using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Nodes/Portal")]
    public class PortalNode : DrawNode
    {
        public override void DrawCurve(BaseNode b)
        {
        }

        public override void DrawWindow(BaseNode b)
        {

            EditorGUILayout.LabelField("Target node id: ");
            b.portalTargetNodeID = EditorGUILayout.TextField(b.portalTargetNodeID);
        }
    }
}