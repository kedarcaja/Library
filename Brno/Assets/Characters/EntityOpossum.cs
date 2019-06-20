using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityOpossum : MonoBehaviour
{

	public Biom GetEntityBiom(CharacterScript character)
	{
		foreach (Biom b in MainOpossum.WeatherOpossum.bioms)
		{
			if (b.IsInBiom(character.transform))
			{
				return b;
			}
		}
		return null;
	}
}
