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
            BehaviourEditor.GetEGLLable("area: ",GUIStyle.none);
            b.randomMoveArea = EditorGUILayout.TextField(b.randomMoveArea);
        }

        public override void Execute(BaseNode b)
        {
            if(BehaviourEditor.GetTransformFromName(b.randomMoveArea) !=null && BehaviourEditor.GetTransformFromName(b.randomMoveArea).GetComponent<RandomMoveArea>() != null)
                b.Graph.character.RandomMove(BehaviourEditor.GetTransformFromName(b.randomMoveArea).GetComponent<RandomMoveArea>());
        }
    }
}