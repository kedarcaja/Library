using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Dialog/DecisionValue")]
public class DialogDecision : ScriptableObject
{
	[SerializeField]
	private Decision decision;
	[SerializeField]
	private Dialog nextDialog;
	[TextArea(1,5)]
	[SerializeField]
	private string value;

	public string Value
	{
		get
		{
			return value;
		}

		
	}

	public Dialog NextDialog
	{
		get
		{
			return nextDialog;
		}

	}

	public Decision Decision
	{
		get
		{
			return decision;
		}


	}
}
