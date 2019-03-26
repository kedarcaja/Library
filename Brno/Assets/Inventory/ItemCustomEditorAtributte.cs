using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[AttributeUsage(AttributeTargets.Class,AllowMultiple = false,Inherited = true)]
public class ItemCustomEditorAtributte : CustomEditor
{
	public ItemCustomEditorAtributte(Type inspectedType, bool editorForChildClasses) : base(inspectedType, editorForChildClasses)
	{
	}


}
