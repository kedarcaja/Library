using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
[Serializable]
public abstract class Item : ScriptableObject
{
	
	#region Editor Variables
	[HideInInspector]
	[ExecuteAlways]
	public int SelectedCategoryIndex;
	[HideInInspector]
	[ExecuteAlways]
	public int SelectedTypeIndex;
	#endregion
	[HideInInspector]
	[ExecuteAlways]
	public ItemType MainCategory;
	[HideInInspector]
	[ExecuteAlways]
	public ItemType ItemType;
	[SerializeField]
	protected EQuality quality;
	[SerializeField]
	protected int level;
	public int Level { get { return level; } }
	[SerializeField]
	protected bool sellable;
	public bool Sellable { get { return sellable; } }
	public bool Stackable { get { return maxCount > 1; } }
	public bool IsInGame { get; set; }
	public Sprite QualityColor { get { return FindObjectOfType<InventoryManager>().QualityDic[quality]; } }
	[ExecuteAlways]
	public Sprite Sprite;
	[SerializeField]
	protected int maxCount = 1;
	public int MaxCount { get { return maxCount; } }

	[Header("Sell")]
	[Range(0, 1000)]
	public int SellGold;

	[Range(0, 99)]
	public int SellSilver;

	[Range(0, 99)]
	public int SellCopper;
	[Space]
	//testík
	[Header("Buy")]
	[Range(0, 1000)]
	public int BuyGold;

	[Range(0, 99)]
	public int BuySilver;

	[Range(0, 99)]
	public int BuyCopper;


	public Slot CurrentSlot { get; set; }

	public EQuality Quality
	{
		get
		{
			return quality;
		}

	}



	[Space]
	[SerializeField]
	[TextArea(10, 50)]
	protected string description;


	public abstract void Use();
	//public void Use(Action otherUsage)
	//{
	//	otherUsage();
	//}

	public virtual void GetTooltip()
	{

		Tooltip.Instance.SetTooltip(this.name, ItemType.ToString(), "", "", "", "", quality.ToString(),
									Sprite, GetStats()
, description, SellGold.ToString(), SellSilver.ToString(),
									SellCopper.ToString(), QualityColor);

	}
	protected virtual string GetStats()
	{
		return "";
	}
}
public enum ItemType
{
	GEM, CHAR, EQUIPMENT, WEAPON, CONSUMEABLE, MATERIAL, QUEST_OTHER, ALCHEMY, Chest, Helm, MeelWeapon, HealthPotion, Generic, Spaulder, Arm, Belt, Ring0, Ring1,
	Trausers, Necklance, Boots, SecondHand, Bow, Arrow, TwoHand, Food,Ore,Fuel,Shirt,Pickaxe,Shovel,FishingRod,Axe,Sickle,Hood
}
public enum EQuality { Comon, Uncomon, Rare, Epic, Mystic, Mythical, Legendary }