using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Editor Settings")]
    public class EditorSettings : ScriptableObject
    {
        public BehaviourGraph currentGraph;
        public StateNode stateNode;
        public TransitionNode transitionNode;
        public CommentNode commentNode;

    }
}
