using UnityEngine;
using System;
using System.Collections.Generic;
[ExecuteAlways]
public class CharacterStats : ScriptableObject
{
	[SerializeField]
	protected float speed,health, stamina, minDamage, maxDamage, maxHealth, maxStamina, influence, talking, luck, fireResistance, waterResistance, coldResistance, lightResistance, poisonResistance, magicResistance;
	[SerializeField]
	protected int strength, agility, intellect, charisma, level;
	public TargetVector TargetVector = new TargetVector();
	public List<Character> Followers = new List<Character>();
	public const int KEDAR = 5;

	public float Health
	{
		get { return health; }
		set { if (value > maxHealth) health = maxHealth; else if (value <= 0) health = 0; else health = value; }
	}
	public float Stamina
	{
		get { return stamina; }
		set { if (value >= maxStamina) stamina = maxStamina; else if (value <= 0) stamina = 0; else stamina = value; }
	}
	public float MinDamage
	{
		get { return minDamage; }
		set { minDamage = value >= maxDamage ? maxDamage : minDamage; }
	}
	public float MaxDamage
	{
		get { return maxDamage; }
		set { maxDamage = value <= minDamage ? minDamage : value; }
	}

	public float MaxHealth
	{
		get { return maxHealth; }
		set { maxHealth = value >= health ? value : health; }
	}

	public float MaxStamina
	{
		get { return maxStamina; }
		set { maxStamina = value >= stamina ? value : stamina; }
	}
	public float Influence
	{
		get { return influence; }
		set { influence = value <= 60 ? value : 60; }
	}
	public float Talking
	{
		get { return talking; }
		set { talking = value <= 60 ? value : 60; }
	}
	public float Luck
	{
		get { return luck; }
		set { luck = value <= 50 ? value : 50; }
	}
	public float FireResistance
	{
		get { return fireResistance; }
		set { fireResistance = value <= 60 ? value : 60; }
	}
	public float WaterResistance
	{
		get { return waterResistance; }
		set { waterResistance = value <= 60 ? value : 60; }
	}
	public float ColdResistance
	{
		get { return coldResistance; }
		set { coldResistance = value <= 60 ? value : 60; }
	}

	public float LightResistance
	{
		get { return lightResistance; }
		set { lightResistance = value <= 60 ? value : 60; }
	}
	public float PoisonResistance
	{
		get { return poisonResistance; }
		set { poisonResistance = value <= 60 ? value : 60; }
	}
	public float MagicResistance
	{
		get { return magicResistance; }
		set { magicResistance = value <= 60 ? value : 60; }
	}

	public float Damage
	{
		get { return (float)Math.Round(UnityEngine.Random.Range(minDamage, maxDamage), 2); }
	}
	public int Strength
	{
		get { return strength; }
		set { strength = value > 0 ? value : 0; }
	}
	public int Agility
	{
		get { return agility; }
		set { agility = value > 0 ? value : 0; }
	}
	public int Intellect
	{
		get { return intellect; }
		set { intellect = value > 0 ? value : 0; }
	}
	public int Charisma
	{
		get { return charisma; }
		set { charisma = value > 0 ? value : 0; }
	}
	public int Level
	{
		get { return level; }
		set { level = value; }
	}
	public float Speed { get { return speed; } set { speed = value; } }

	public bool IsAlive { get { return health > 0; } }




}