using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Graph")]
    public class BehaviourGraph : ScriptableObject
    {
        public List<BaseNode> windows = new List<BaseNode>();
        #region Checkers
        public bool IsStateNodeDuplicate(StateNode node)
        {

            return false;
        }
       
  
        #endregion


    }
  
}