using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    public class BaseNode : ScriptableObject
    {
        public int ID { get; set; }
        public Rect WindowRect { get; set; }
        public string WindowTitle { get; set; }
        public BaseNode(float x, float y, float width, float height, string title) { }

        public BaseNode(Rect windowRect, string title) { }
        public BaseNode(Vector2 position, Vector2 size, string title) { }


        public virtual void DrawWindow()
        {

        }
    }
}