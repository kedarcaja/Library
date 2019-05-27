using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Character Graph")]
    public class CharacterGraph : ScriptableObject
    {




        public List<BaseNode> nodes = new List<BaseNode>();
        [SerializeField]
        private Character character;
        public List<int> removeNodesIDs = new List<int>();
        public Character Character { get => character; }
        [SerializeField]
        private int nodeIDS = 0;


        #region Saving/Loading


        public List<ConditionNode> conds = new List<ConditionNode>();
        public List<StateNode> states = new List<StateNode>();
        public List<CommentNode> coms = new List<CommentNode>();
        public List<Transition> InitTransitions = new List<Transition>();
        public List<string> InitTransitionsToRemove = new List<string>();
        public bool SaveListsAreNotEmpty()
        {
            return coms.Count > 0 || conds.Count > 0 || states.Count > 0;
        }

        private void OnEnable()
        {
            LoadNodes();
            InitializeTransitions();
        }
        public void LoadNodes()
        {
            if (!SaveListsAreNotEmpty()) return;

            nodes.Clear();

            if (coms.Count > 0)
                nodes.AddRange(coms);
            if (conds.Count > 0)
                nodes.AddRange(conds);
            if (states.Count > 0)
                nodes.AddRange(states);
        }

        #endregion
        public T AddNode<T>(float x, float y, float width, float height, string title) where T : BaseNode
        {

            BaseNode n = (T)Activator.CreateInstance(typeof(T));
            n.WindowRect = new Rect(x, y, width, height);
            n.WindowTitle = title;
            nodes.Add(n);
            n.CharacterGraph = this;
            n.ID = nodeIDS;
            nodeIDS++;
            n.normalHeight = n.WindowRect.height;


            if (typeof(T) == typeof(ConditionNode))
            {
                ConditionNode c = n as ConditionNode;
                conds.Add(c);
            }
            if (typeof(T) == typeof(CommentNode))
            {
                CommentNode c = n as CommentNode;
                coms.Add(c);
            }
            if (typeof(T) == typeof(StateNode))
            {
                StateNode c = n as StateNode;
                states.Add(c);
            }

            return n as T;
        }

        public void RemoveNodeSelectedNodes()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] == null) continue;

                if (removeNodesIDs.Contains(nodes[i].ID))
                {
                    nodes.Remove(nodes[i]);
                }

            }
            removeNodesIDs.Clear();
        }
        public void AddInitiTransitionToRemove(string id)
        {
            InitTransitionsToRemove.Add(id);

        }
        public void RemoveInitTransitions()
        {

            for (int i = 0;i < InitTransitions.Count;i++)
            {
                if (InitTransitionsToRemove.Contains(InitTransitions[i].ID))
                {
                    InitTransitions.Remove(InitTransitions[i]);
                }
            }

            InitTransitionsToRemove.Clear();
        }


        public void InitializeTransitions()
        {
            foreach (Transition t in InitTransitions)
            {
                t.graph = this;
                t.Init();
            }
        }
    }
}
