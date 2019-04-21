using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[CreateAssetMenu(menuName = "Inventory/Consumeable", fileName = "NEWConsumeable")]
public class Consumeable : Item
{
	public override void Use()
	{
		Debug.Log("<color=green>"+name+" was used</color>");
		CurrentSlot.Bag.RemoveItems(CurrentSlot,1);
	}
}
