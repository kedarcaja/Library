using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/RandomMove")]
    public class RandomMoveNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode node)
        {
        }

        public override void DrawWindow(BaseNode b)
        {
            BehaviourEditor.GetEGLLable("area: ", GUIStyle.none);
            b.randomMoveArea = EditorGUILayout.TextField(b.randomMoveArea);

        }

        public override void Execute(BaseNode b)
        {
            Transform t = BehaviourEditor.GetTransformFromName(b.randomMoveArea);
            if (t != null && t.GetComponent<RandomMoveArea>() != null&& !b.randomSet)
            {
                b.Graph.character.RandomMove(t.GetComponent<RandomMoveArea>());
                b.randomSet = true;
            }
            if(b.randomSet&& b.Graph.character.AgentReachedTarget())
            {
                b.nodeCompleted = true;
                b.randomSet = false;

            }
        }
    }
}