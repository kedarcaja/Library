using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class CharacterCustoEditorAtributte : CustomEditor
{
    public CharacterCustoEditorAtributte(Type inspectedType) : base(inspectedType)
    {
    }
}
