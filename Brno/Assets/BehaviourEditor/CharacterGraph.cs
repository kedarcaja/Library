using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Character Graph")]
    public class CharacterGraph : ScriptableObject
    {
        public List<BaseNode> nodes = new List<BaseNode>();
        [SerializeField]
        private Character character;

        public Character Character { get => character; }
        private int idsCount;
        public void AddNode<T>(float x, float y, float width, float height, string title) where T : BaseNode
        {

            BaseNode n = (T)Activator.CreateInstance(typeof(T));
            n.WindowRect = new Rect(x, y, width, height);
            n.WindowTitle = title;
            nodes.Add(n);
            idsCount++;
            n.ID = idsCount;


        }


        public void RemoveNode(int id)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (id == nodes[i].ID)
                {
                    nodes.Remove(nodes[i]);
                    return;
                }

            }

        }

    }
}
