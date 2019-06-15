using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/SetDestination")]
    public class SetDestinationNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode node)
        {
        }

        public override void DrawWindow(BaseNode b)
        {
#if UNITY_EDITOR

            BehaviourEditor.GetEGLLable("target: ", GUIStyle.none);


            b.destinationTargetName = EditorGUILayout.TextField(b.destinationTargetName);
#endif
        }

        public override void Execute(BaseNode b)
        {
            if (b.destinationTarget == null)
            {
#if UNITY_EDITOR

                b.destinationTarget = BehaviourEditor.GetTransformFromName(b.destinationTargetName);
#endif
            }
            else
            {
                b.Graph.character?.SetTarget(b.destinationTarget);
                b.nodeCompleted = b.Graph.character.AgentReachedTarget();
            }
        }
    }
}