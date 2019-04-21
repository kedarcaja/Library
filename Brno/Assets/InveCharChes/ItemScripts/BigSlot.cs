using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.EventSystems;

public class BigSlot : Slot
{

	protected override void Awake()
	{
		base.Awake();
		OnRemove += new Action(() => Destroy(gameObject));
		OnClear += new Action(() => Destroy(gameObject));
	}

	public void TakeItem()
	{
		bool added;
		Inventory.Instance.AddItem(this, out added);
		if (added)
		{
			if ((Bag as SackScript).Slots.Count == 1 && (Bag as SackScript).Slots.Last() == this)
			{
				(Bag as SackScript).Close();
				Destroy((Bag as SackScript).Sack);
				(Bag as SackScript).HideSackUI();
				Destroy((Bag as SackScript).gameObject);

			}

			RemoveAll();
			PlayerScript.Instance.Gathering();
			return;
		}
		RemoveAll(); // item se dropne takže se musí zase odebrat
		Debug.Log("<color=red>Item wasnt added, you dont have enough space</color>");
	}
	public override void OnPointerClick(PointerEventData eventData)
	{

		if (eventData.button == PointerEventData.InputButton.Left)
		{

			TakeItem();
		}
	}
	private void OnDestroy()
	{
		(Bag as SackScript).Resize();
		(Bag as SackScript).Slots.Remove(this);
	}
}
