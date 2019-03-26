using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dialog/Decision")]
public class Decision : ScriptableObject
{
	
	[SerializeField]
	private List<DialogDecision> values = new List<DialogDecision>();
	public DialogDecision Selected { get; set; }
	public List<DialogDecision> Values
	{
		get
		{
			return values;
		}

	
	}
	[SerializeField]
	private Decision nextDecision;
	public Decision NextDecision
	{
		get
		{
			return nextDecision;
		}

		
	}

	
}
