using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/EditorSettings")]
    public class EditorSettings : ScriptableObject
    {
        public GUISkin skin;
    }
}