using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace BehaviourTreeEditor
{
    public class CommentNode : BaseNode
    {
        private string comment = "Type some comment";
        public override void DrawWindow()
        {
            GUI.contentColor = Color.green;
            comment = GUILayout.TextArea(comment,200);
        }
    }
}