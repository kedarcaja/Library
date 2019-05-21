using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Data/Condition")]
    public class Condition : ScriptableObject
    {
        [SerializeField]
        private Texture icon;

        public Texture Icon { get => icon; }

        public bool IsChecked()
        {
            return true;
        }
    }
}