using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
	[SerializeField]
	private bool isFocused;
	public UnityEvent OnFocus,OnDefocus;
	public void GetFocus()
	{
		FindObjectsOfType<Target>().ToList().ForEach(s => s.DeFocus());
		GetComponent<Renderer>().material.color = Color.green;
		if (OnFocus != null)
		{
			OnFocus.Invoke();
		}
	}
	public void DeFocus()
	{
		GetComponent<Renderer>().material.color = Color.red;
		if (OnDefocus != null)
		{
			OnDefocus.Invoke();
		}
	}
	private void OnMouseUp()
	{
		GetFocus();
	}
}
