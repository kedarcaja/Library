using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public enum EMaterialType { Fuel, Ore, Crafting, Herbs }
[CreateAssetMenu(menuName = "Inventory/Material", fileName = "NewMaterial")]
public class _Material : Item
{

	[SerializeField]
	private float meltingTime, heating;
	public float MeltingTime
	{
		get
		{
			return meltingTime;
		}
	}

	public float Heating
	{
		get
		{
			return heating;
		}
	}

	public override void Use()
	{

	}
	public override void GetTooltip()
	{
		Tooltip.Instance.SetTooltip(name, ItemType.ToString(), heating.ToString(), "heating", "melting time", meltingTime.ToString(), quality.ToString(), Sprite, "", description, SellGold.ToString(), SellSilver.ToString(), SellCopper.ToString(), QualityColor);
	}
}
