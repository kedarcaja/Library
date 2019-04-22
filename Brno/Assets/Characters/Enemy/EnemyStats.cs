
using UnityEngine;

public enum EEnemyState { Attack, Detected, Neutral,Search }
public enum ETemperament{ Neutral,Agresive }
[CreateAssetMenu(menuName = "CharacterScript/Stats/EnemyStats", fileName = "NewEnemyState")]
[ExecuteAlways]
public class EnemyStats : EntityStats
{
	[SerializeField]
	private bool randomMove = false;
	[SerializeField]
	private ETemperament temperament;
	[ExecuteAlways]
	[SerializeField]
	private EEnemyState state;
	[ExecuteAlways]
	public EEnemyState State
	{
		get
		{
			return state;
		}

		set
		{
			state = value;
		}
	}

	public ETemperament Temperament
	{
		get
		{
			return temperament;
		}
	}
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
