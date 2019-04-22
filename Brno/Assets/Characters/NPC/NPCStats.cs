using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
//public enum EMood { Angry, Happy, Sad, frightened, Neutral, Hangover, }
public enum EProffesion { LamberJack, Miner, Blacksmith, ShopKeeper, InnKeeper, Herbalis, Guardian, Official, Baker, Librarian, Butcher, Farmer, Peasant, Cook, Alchemist, NONE }
[CreateAssetMenu(menuName = "CharacterScript/Stats/NPCStats", fileName = "NewNPCStats")]
public class NPCStats : CharacterStats
{
	[SerializeField]
	private EMood mood;
	[SerializeField]
	private EProffesion profession;
	public EMood Mood
	{
		get
		{
			return mood;
		}

		set
		{
			mood = value;
		}
	}
	public EProffesion Profession
	{
		get
		{
			return profession;
		}
	}
	[SerializeField]
	private bool randomMove;

	public bool RandomMove
	{
		get
		{
			return randomMove;
		}

		set
		{
			randomMove = value;
		}
	}
}
