﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/EditorSettings")]
    public class EditorSettings : ScriptableObject
    {
        #region Style
        public GUISkin skin;
        public float gridSpacing = 10,gridOpacity = 0.2f;
        public Color backgroundColor = new Color32(37, 37, 37, 255), gridColor = Color.gray,otherGUIColor = new Color32(50, 50, 50, 255);
        public Texture2D grabCursor, normalCursor;
        #endregion
        public CommentNode CommentNode;
        public StateNode StateNode;
        public ConditionNode ConditionNode;

    
    }
}