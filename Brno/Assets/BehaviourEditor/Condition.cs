using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Condition")]
    public  class Condition : ScriptableObject
    {
        public bool IsChecked() => true;
    }
}