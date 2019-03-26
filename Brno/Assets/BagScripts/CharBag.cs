using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharBag : BagScript
{
	[SerializeField]
	private Transform slotParent;
	[SerializeField]
	private CharBag actionBag;
	private void Awake()
	{

		bag.Slots = new List<Slot>();
		if(GetComponent<CanvasGroup>())
		canvasGroup = GetComponent<CanvasGroup>();

		bag.BagScript = this;
		if (slotParent != null)
		{
			for (int i = 0; i < slotParent.childCount; i++)
			{
				bag.Slots.Add(slotParent.GetChild(i).GetComponent<Slot>());
				slotParent.GetChild(i).GetComponent<Slot>().Bag = this;

			}

			bag.SlotCount = slotParent.childCount;
		}
	}

}
