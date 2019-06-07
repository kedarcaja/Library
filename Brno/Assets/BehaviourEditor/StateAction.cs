using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTreeEditor
{
    public abstract class StateAction : ScriptableObject
    {
            public abstract void Execute(CharacterScript states);
    }
}
