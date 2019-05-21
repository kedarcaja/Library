using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Character Graph")]
    public class CharacterGraph : ScriptableObject
    {
        [SerializeField]
        public List<BaseNode> nodes = new List<BaseNode>();
        public BaseNode AddNode<T>(float x, float y, float width, float height, string title) where T : BaseNode
        {
            BaseNode n = CreateInstance<T>();
            n.WindowRect = new Rect(x, y, width, height);
            n.WindowTitle = title;
            nodes.Add(n);
            n.ID = nodes.Count;
            return n;
        }
        public BaseNode AddNode<T>(Vector2 pos, Vector2 size, string title) where T : BaseNode
        {
            BaseNode n = CreateInstance<T>();
            n.WindowRect = new Rect(pos.x, pos.y, size.x, size.y);
            n.WindowTitle = title;
            nodes.Add(n);
            n.ID = nodes.Count;
            return n;

        }
        public BaseNode AddNode<T>(Rect rect, string title) where T : BaseNode
        {
            BaseNode n = CreateInstance<T>();
            n.WindowRect = rect;
            n.WindowTitle = title;
            nodes.Add(n);
            n.ID = nodes.Count;
            return n;
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
