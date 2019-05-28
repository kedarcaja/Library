using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace BehaviourTreeEditor
{
    [Serializable]
    public class CommentNode : BaseNode
    {
        private string comment = "Type some comment";

        public override void DrawWindow()
        {
            comment = GUILayout.TextArea(comment,200);
        }
    }
}