using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    public  abstract class Condition : ScriptableObject
    {
        public abstract bool IsChecked(CharacterScript character);
    }
}