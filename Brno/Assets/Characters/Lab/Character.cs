﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ECharacterClass {Warior,Archer }
public enum EWork {Lamberjack,Miner,Baker,InnKeeper }
public enum EMood {Angry,Scared,Sad,Happy }
[CreateAssetMenu(menuName ="Test/charData")]
public class Character : ScriptableObject
{
    [HideInInspector]
    public ECharacterClass CharacterClass;
    [HideInInspector]
    public EWork Work;
    [HideInInspector]
    public EMood Mood;
    [HideInInspector]
    public Sprite Portait;
    [HideInInspector]
    public Color Color;
    public int Level;




    #region Stats
    [SerializeField]
    protected float health, maxHealth, stamina, maxStamina, minDamage, maxDamage;

    protected float damage;
   
    [SerializeField]
    [Range(0, 100)]
    [Header("Staty [%]")]
    protected float fireResistance, waterResistance, coldResistance, lightResistance, poisonResistance, magicResistance, influence, talking, luck;

    [SerializeField]
    protected int strength, agility, intellect, charisma, runSpeed;
    public TargetVector TargetVector = new TargetVector();
    public List<CharacterScript> Followers = new List<CharacterScript>();
    public const int KEDAR = 5;
    [SerializeField]
    protected int walkSpeed;
    public int WalkSpeed { get { return walkSpeed; } }
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
    public float Damage
    {
        get { return damage; }
        set { damage += value; }
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
   
    public int RunSpeed { get { return runSpeed; } set { runSpeed = value; } }

    public bool IsAlive { get { return health > 0; } }
    #endregion

}
