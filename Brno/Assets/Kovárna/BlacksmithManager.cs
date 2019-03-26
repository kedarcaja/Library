using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
public enum EBSState { SelectingFuel, SelectingOre, Melting }

public class BlacksmithManager : MonoBehaviour
{
	private EBSState state = EBSState.SelectingFuel;

	[SerializeField]
	private List<CanvasGroup> scenes = new List<CanvasGroup>();
	private CanvasGroup currentScene;
	[SerializeField]
	private TextMeshProUGUI totalHeatingText;
	public float TotalHeating { get; private set; }
	public static BlacksmithManager Instance { get; private set; }
	[SerializeField]
	private Slot fuelSlot, oreSlot;


	public EBSState State
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

	public Slot FuelSlot
	{
		get
		{
			return fuelSlot;
		}
	}

	public Slot OreSlot
	{
		get
		{
			return oreSlot;
		}
	}

	[SerializeField]
	private BagScript sellectionBag, furnanceBag;
	private void Awake()
	{
		Instance = FindObjectOfType<BlacksmithManager>();
	}
	public void SetTotalHeating(float heating)
	{
		TotalHeating = heating;
		totalHeatingText.text = "Total heating: " + TotalHeating;
	}
	void Start()
	{

		currentScene = scenes[0];

		state = 0;
		fuelSlot.Bag = furnanceBag;
		oreSlot.Bag = furnanceBag;
		if (!furnanceBag.Bag.Slots.Contains(fuelSlot))
			furnanceBag.Bag.Slots.Add(fuelSlot);
		if (!furnanceBag.Bag.Slots.Contains(oreSlot))
			furnanceBag.Bag.Slots.Add(oreSlot);


		oreSlot.OnAdd += delegate { SelectOreFuel(oreSlot); ReturnItemsToInventory(); state = EBSState.Melting; sellectionBag.Close();};
		fuelSlot.OnAdd += delegate { SelectOreFuel(fuelSlot); ReturnItemsToInventory(); ; state = EBSState.SelectingOre; SetSelectionItems(ItemType.Ore); };

		fuelSlot.OnRemove += delegate { if (!fuelSlot.Filled) DeSelectOreFuel(fuelSlot); };
		oreSlot.OnRemove += delegate { if (!oreSlot.Filled) DeSelectOreFuel(oreSlot); };

		oreSlot.OnClear += delegate { DeSelectOreFuel(oreSlot); };
		fuelSlot.OnClear += delegate { DeSelectOreFuel(fuelSlot); };

	}
	private void Update()
	{

		if (Input.GetKeyDown(KeyCode.E)) // při otevření kovárny 
		{
			SetSelectionItems(ItemType.Fuel);

		}
	}
	private void ReturnItemsToInventory()
	{
		foreach (Slot s in sellectionBag.Bag.Slots.Where(e => e.Filled))
		{
			Inventory.Instance.AddItem(s.CurrentItem, s.Items.Count);
			s.RemoveAll();
		}
	}
	private void SelectOreFuel(Slot CurrentSlot)
	{
		CurrentSlot.GetComponent<Image>().raycastTarget = false;
		Destroy(CurrentSlot.Container.gameObject.GetComponent<EventTrigger>());
	}
	private void DeSelectOreFuel(Slot CurrentSlot)
	{
		CurrentSlot.GetComponent<Image>().raycastTarget = true;
		CurrentSlot.Container.gameObject.AddComponent<EventTrigger>();
	}
	public void SetSelectionItems(ItemType type)
	{
		foreach (Slot s in Inventory.Instance.Bags.Find(b => b.Bag.Type == ItemType.MATERIAL).Bag.Slots)
		{
			if (!s.Filled) continue;
			if (s.CurrentItem.ItemType == type)
			{
				sellectionBag.AddItems(s.CurrentItem, s.Items.Count);
				s.Bag.RemoveItems(s, s.Items.Count);
			}
		}

	}

}

