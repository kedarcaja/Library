using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, IOpenable
{
	[SerializeField]
	private List<BagScript> bags = new List<BagScript>();
	public List<BagScript> Bags { get { return bags; } }
	public BagScript CurrentBag { get; set; }
	private bool opened;

	public event OpenHandler OnOpen;
	public event OpenHandler OnClose;

	public bool Opened
	{
		get
		{

			return opened;
		}
	}

	public static Inventory Instance
	{
		get; private set;
	}



	protected virtual void Start()
	{
       
        Instance = FindObjectOfType<Inventory>();
		// restores equiped bool => only for testing
		foreach (Equipment e in Resources.LoadAll<Equipment>("Items"))
		{
			e.Equiped = false;
		}

		InventoryManager.Instance.MovingSlot.transform.SetAsLastSibling();
		/*foreach (BagScript b in bags)
		{
			foreach (Slot s in b.Bag.Slots)
			{
				if(s.CurrentItem is Equipment&&(s.CurrentItem as Equipment).Equiped)
				{
					s.CurrentItem.Use();
				}
			}
		} RESTORE EQUIPED ITEMS*/

	}
	protected virtual void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
           // AddItemWithName("Boty - všestraný testovací set", 1);
           //// AddItemWithName("Brnění - všestraný testovací set", 1); //není sprite
           // AddItemWithName("Kalhoty - všestraný testovací set", 1);
           // AddItemWithName("Šípy s železným hrotem", 1);
           // //AddItemWithName("Pásek - všestraný testovací set", 1);
           // //AddItemWithName("Rukavice - všestraný testovací set", 1);
           // //AddItemWithName("Helma - všestraný testovací set", 1);
           // //AddItemWithName("Nátepník - všestraný testovací set", 1);
           // //AddItemWithName("Prsten - všestraný testovací se iit", 1);
           // //AddItemWithName("Prsten2 - všestraný testovací set", 1);
           // //AddItemWithName("Náhrdelník - všestraný testovací set", 1);
           // AddItemWithName("Štít - všestraný testovací set", 1);
           // AddItemWithName("Košile", 1);
           // AddItemWithName("Krumpáč", 1);
           // //AddItemWithName("Kápě", 1);//není sprite
           // AddItemWithName("Rybářský prut", 1);
           // AddItemWithName("Lopara", 1);
            AddItemWithName("Jablko", 10); 
            AddItemWithName("Meč", 1);
            
           // AddItemWithName("bagitem", 11);
           
            
          
           // AddItemWithName("Sekera", 1);
           // AddItemWithName("Luk", 1);
           // AddItemWithName("Srp", 1);
           
           // AddItemWithName("Šperhák", 1);



           // AddItemWithName("Ferriom", 4);
           // AddItemWithName("Kvalitní železná ruda", 4);


        }



        if (Input.GetKeyDown(KeyCode.R))
		{
			ItemIsInInventory("Iron");
		}

	}

	private void AddItem(Item i, int count, out bool success)
	{

		Bag ba = Resources.LoadAll<Bag>("Bags").First(b => b.Type == i.MainCategory);
		success = !ba.Full;
		ba.BagScript.AddItems(i, count);
		QuestManager.Instance.UpdateCollectQuestParts(i);


	}
	public void AddItem(Item i, int count)
	{
		Bag ba = Resources.LoadAll<Bag>("Bags").First(b => b.Type == i.MainCategory);
		ba.BagScript.AddItems(i, count);
		QuestManager.Instance.UpdateCollectQuestParts(i);

	}
	public void AddItem(Slot slot, out bool success)
	{
		AddItem(slot.CurrentItem, slot.Items.Count, out success);
	}
	public void AddItemWithName(string name, int count)
	{

		Item i = Resources.Load<Item>("Items/" + name);
		// if (i.isInGame) return; -- enable this to allow item be in game only once
		//i.IsInGame = true;
		if (i != null)
		{
			Bag ba = Resources.LoadAll<Bag>("Bags").First(b => b.Type == i.MainCategory);
			ba.BagScript.AddItems(i, count);
		}
	}
	public void SortBag()
	{
		CurrentBag.Sort();
	}

	public void DropItem(Item item, int count)
	{

		SackScript sack;
		if (InventoryManager.Instance.SackNearby == null)
		{
			sack = Instantiate(InventoryManager.Instance.SackSlotsPrefab, InventoryManager.Instance.DropSackSlotsParent.transform).GetComponent<SackScript>();

			float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);
			Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

			v *= 1;

			sack.Sack = Instantiate(InventoryManager.Instance.Sack, PlayerScript.Instance.transform.position - v, Quaternion.identity);
		}
		else
		{
			sack = InventoryManager.Instance.SackNearby;

		}
		sack.CreateBigSlot(item, count);
		sack.Sack.transform.SetParent(InventoryManager.Instance.DropSacksParent.transform);



	}
	public void DropDragedItem()
	{
		if (!InventoryManager.Instance.IsDraging) return;
		DropItem(InventoryManager.Instance.MovingSlot.GetComponent<Slot>().CurrentItem,
				 InventoryManager.Instance.MovingSlot.GetComponent<Slot>().Items.Count);
		InventoryManager.Instance.ClearMovingSlot();

	}
	public bool ItemIsInInventory(string itemName)
	{
		BagScript[] allBags = FindObjectsOfType<BagScript>().Where(b => !(b is CharBag) && !(b is SackScript) && !(b is Chest)).ToArray();

		foreach (BagScript bag in allBags)
		{
			foreach (Slot s in bag.Bag.Slots)
			{
				if (!s.Filled) continue;
				if (s.CurrentItem.name == itemName)
				{

					return true;
				}
			}

		}

		return false;
	}
	public bool ItemIsInInventory(ItemType itemType)
	{
		BagScript[] allBags = FindObjectsOfType<BagScript>().Where(b => !(b is CharBag) && !(b is SackScript) && !(b is Chest)).ToArray();

		foreach (BagScript bag in allBags)
		{
			foreach (Slot s in bag.Bag.Slots)
			{
				if (!s.Filled) continue;
				if (s.CurrentItem.ItemType == itemType)
				{
					return true;
				}
			}
		}
		return false;
	}
	public List<Slot> GetSlotsOfItem(ItemType itype)
	{
		List<Slot> slots = new List<Slot>();

		if (ItemIsInInventory(itype))
		{
			BagScript[] allBags = FindObjectsOfType<BagScript>().Where(b => !(b is CharBag) && !(b is SackScript) && !(b is Chest)).ToArray();
			foreach (BagScript bag in allBags)
			{
				foreach (Slot s in bag.Bag.Slots)
				{
					if (!s.Filled) continue;
					if (s.CurrentItem.ItemType == itype)
					{
						slots.Add(s);
					}
				}
			}
		}
		return slots.Count > 0 ? slots : null;
	}
	

	public void DropItemFromSlot(Slot slot)
	{

		DropItem(slot.CurrentItem, slot.Items.Count);
		InventoryManager.Instance.ClearSlot(slot);
	}

	public void PullItemBack()
	{
		if (InventoryManager.Instance.IsDraging)
		{
			CurrentBag.AddFromTo(InventoryManager.Instance.MovingSlot.GetComponent<Slot>(), InventoryManager.Instance.From);
			InventoryManager.Instance.ClearMovingSlot();
		}
	}

	public virtual void Open()
	{
		if (OnOpen != null)
		{
			OnOpen();
		}
		opened = true;
		GetComponent<CanvasGroup>().alpha = 1;
		GetComponent<CanvasGroup>().blocksRaycasts = true;

	}

	public virtual void Close()
	{
		if (OnClose != null)
		{
			OnClose();
		}
		opened = false;
		GetComponent<CanvasGroup>().alpha = 0;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}


}

