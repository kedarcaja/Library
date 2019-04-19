using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagScript : MonoBehaviour, IOpenable
{
	#region Variables
	[SerializeField]
	protected Bag bag;
	public Bag Bag { get { return bag; } }
	protected CanvasGroup canvasGroup;
	public Slot EmptySlot { get { if (!bag.Full) if (!bag.AllSlotsFilled) { return bag.Slots.First(s => !s.Filled); } return null; } }
	public bool Full { get { return bag.Full; } }
	[SerializeField]
	private Button categoryButton;
	public event OpenHandler OnOpen;
	public event OpenHandler OnClose;
	[SerializeField]
	private TextMeshProUGUI level;
	
	public Button CategoryButton
	{
		get
		{
			return categoryButton;
		}
	}
	protected bool opened;

	

	public bool Opened
	{
		get
		{
			return opened;
		}
	}
	#endregion
	private void Update()
	{
		if (categoryButton != null)
		{
			categoryButton.interactable = !InventoryManager.Instance.IsDraging;//jiné řešení než update
		}
	}
	private void Awake()
	{
		if(bag.Level!=11)// odebrat
		bag.Level = 1; //odebrat 
		if (bag != null)
		{
			bag.Slots = new List<Slot>();
			bag.BagScript = this;
			if(level!=null)
			level.text = bag.Level.ToString();
			Draw();
		}
		canvasGroup = GetComponent<CanvasGroup>();
	}
	public virtual void Draw()
	{
		if (bag.Level <= bag.Columns)
		{
			for (int i = 0; i < bag.Columns * bag.Level; i++)
			{
				GameObject slo = Instantiate(FindObjectOfType<InventoryManager>().SlotPrefab);
				slo.transform.parent = this.transform;
				slo.GetComponent<Slot>().Bag = this;
				slo.GetComponent<Slot>()._CanContain = bag.Type;
				bag.Slots.Add(slo.GetComponent<Slot>());
				slo.transform.localScale = new Vector3(1, 1, 1);
			}
		}
	}
	public void LevelUp()
	{

		if (bag.Level < bag.Columns)
		{
			bag.Level += 1;
			AddSlotRow();
			level.text = bag.Level.ToString();
			Debug.Log("Bag Level Up");
			return;
		}
		Debug.Log("Bag have max level");
	}
	private void AddSlotRow()
	{

		for (int i = 0; i < bag.Columns; i++)//* level to restore on start
		{
			GameObject slo = Instantiate(FindObjectOfType<InventoryManager>().SlotPrefab);
			slo.transform.parent = this.transform;
			slo.GetComponent<Slot>().Bag = this;
			slo.GetComponent<Slot>()._CanContain = bag.Type;
			bag.Slots.Add(slo.GetComponent<Slot>());
			slo.transform.localScale = new Vector3(1, 1, 1);
		}

	}
	/// <summary>
	/// Only for basic add items to inventory
	/// </summary>
	/// <param name="item"></param>
	/// <param name="count"></param>
	public void AddItems(Item item, int count)
	{


		if (!bag.Full)
		{
			Slot slt = null;
			foreach (Slot s in bag.Slots)
			{
				if (!s.Full && s.CurrentItem == item && s.CanContain(item))
				{
					slt = s;
					break;
				}
			}
			if (slt != null && !bag.Full)
			{
				int spac = count > slt.CurrentItem.MaxCount - slt.Items.Count ? slt.CurrentItem.MaxCount - slt.Items.Count : count;
				for (int i = 0; i < spac; i++)
				{
					slt.Add(item);
				}
				count -= spac;

			}
			if (!bag.Full && count > 0)
			{
				if (bag.AllSlotsFilled && bag.Slots.Exists(s => s.CurrentItem == item && !s.Full))

					AddItems(item, count);


			}



			if (!item.Stackable)
			{
				// places one item to empty slot
				if (bag.AllSlotsFilled) { Inventory.Instance.DropItem(item, count); return; }
				if (count == 0) return;
				EmptySlot.Add(item);
				AddItems(item, count - 1);
				return;
			}
			if (!bag.AllSlotsFilled && !bag.Full)
			{

				// places count of items to empty slot
				Slot sl = EmptySlot;
				int place = count > item.MaxCount ? item.MaxCount : count;
				if (place == 0) return;
				sl.Add(item);
				for (int i = 1; i < place; i++)
				{
					sl.Add(item);
				}

				count -= place;

				if (count > 0)
				{
					if (count == 0) return;
					AddItems(item, count);
					return;
				}
				if (bag.AllSlotsFilled && !bag.Full)
				{

					Slot tmp = null;
					foreach (Slot s in bag.Slots)
					{
						if (!s.Full && s.CurrentItem == item /*&& s.CanContain(item)*/)
						{
							tmp = s;
							break;
						}
					}
					if (tmp != null)
					{

						int spac = count > tmp.CurrentItem.MaxCount - tmp.Items.Count ? tmp.CurrentItem.MaxCount - tmp.Items.Count : count;
						for (int i = 0; i < spac; i++)
						{
							tmp.Add(item);
						}
						count -= spac;

					}
					if (count > 0 && !bag.Full)
					{
						if (bag.AllSlotsFilled && bag.Slots.Exists(s => s.CurrentItem == item && !s.Full))
							AddItems(item, count);
					}
				}
			}
		}
		else if (bag.Full && count > 0)
		{
			Inventory.Instance.DropItem(item, count); return;
		}
	}
	/// <summary>
	/// To swap and merge items with draging
	/// </summary>
	/// <param name="item"></param>
	/// <param name="slot"></param>
	/// <param name="count"></param>
	public void AddItems(Item item, Slot slot, int count)
	{
		if (!slot)
		{
			Inventory.Instance.PullItemBack();
		}

		if (slot.Bag == this)
		{
			if (slot == null || bag.Full && slot != GameObject.Find("HelpSlot").GetComponent<Slot>() || item == null) { Inventory.Instance.DropItem(item, count); return; };
		}

		if (!slot.Filled)
		{
			int place = count > item.MaxCount ? item.MaxCount : count;
			if (place == 0) return;

			slot.Add(item);
			for (int i = 1; i < place; i++)
			{
				slot.Add(item);
			}

			count -= place;

			if (count > 0)
			{
				AddItems(item, count);
				return;
			}
			return;
		}
		if (slot.CanContain(item) && item == slot.CurrentItem)
		{
			if (!slot.Full)
			{
				int spac = count > slot.CurrentItem.MaxCount - slot.Items.Count ? slot.CurrentItem.MaxCount - slot.Items.Count : count;
				for (int i = 0; i < spac; i++)
				{
					slot.Add(item);
				}
				count -= spac;


				if (count > 0 && !bag.Full)
				{
					AddItems(item, count);
				}
				return;
			}
		}
		if (!bag.Full)
			AddItems(item, count);
	}
	public void RemoveItems(Slot slot, int count)
	{
		for (int i = 0; i < count; i++)
		{
			slot.Remove();

		}
	}
	public void AddFromTo(Slot from, Slot to)
	{

		AddItems(from.CurrentItem, to, from.Items.Count);
		InventoryManager.Instance.ClearSlot(from);
		InventoryManager.Instance.UnHighliteSlot(from);
		InventoryManager.Instance.UnHighliteSlot(to);

	}
	public void ClearBag()
	{
		for (int i = 0; i < bag.Slots.Count; i++)
		{
			InventoryManager.Instance.ClearSlot(bag.Slots[i]);
		}
	}

	public void SwapItems(Slot from, Slot to)
	{
		Slot tmp = GameObject.Find("HelpSlot").GetComponent<Slot>();
		AddFromTo(from, tmp);
		AddFromTo(to, from);
		AddFromTo(tmp, to);

	}
	public void DandDSwap(Slot to)
	{


		AddFromTo(to, InventoryManager.Instance.From);
		AddFromTo(InventoryManager.Instance.MovingSlot.GetComponent<Slot>(), to);
		InventoryManager.Instance.ClearMovingSlot();
		if (to.CurrentItem.MainCategory == ItemType.WEAPON && to.CurrentItem.ItemType == ItemType.TwoHand)
		{

			Slot sec = null;
			foreach (Slot slo in FindObjectsOfType<Slot>())
			{
				if (slo._CanContain == ItemType.SecondHand)
				{
					sec = slo;
					break;
				}
			}
			if (sec.Filled)
			{
				(sec.CurrentItem as Equipment).Equiped = false;
				sec.Bag.AddFromTo(sec, Inventory.Instance.Bags.Find(c => c.Bag.Type == sec.CurrentItem.MainCategory).EmptySlot);
			}
		}
		else if (to.CurrentItem.ItemType == ItemType.SecondHand)
		{
			Slot slt = FindObjectsOfType<Slot>().ToList().Find(o => o._CanContain == ItemType.WEAPON && o.Bag is CharBag);
			if (slt.Filled)
			{
				(slt.CurrentItem as Equipment).Equiped = false;
				slt.Bag.AddFromTo(slt, Inventory.Instance.Bags.Find(b => b.Bag.Type == slt.CurrentItem.MainCategory).EmptySlot);
			}
		}

	}

	public void Sort()
	{
		for (int i = 0; i < bag.Slots.Count; i++)
		{
			Slot tmp = bag.Slots[i];
			for (int j = 0; j < bag.Slots.Count; j++)
			{
				Slot cur = bag.Slots[j];
				if (!bag.Full && i != j && cur.Filled && !cur.Full && cur.CurrentItem == tmp.CurrentItem)
				{
					AddFromTo(tmp, cur);
				}
				if (i != j && cur.Filled && tmp.Filled && string.Compare(cur.CurrentItem.name, tmp.CurrentItem.name) == 1)
				{

					SwapItems(tmp, cur);
				}

			}
			if (!bag.AllSlotsFilled)
			{
				Slot e = EmptySlot;
				if (tmp.Filled && bag.Slots.IndexOf(e) < i)
				{
					AddFromTo(tmp, e);
				}
			}
		}
		InventoryManager.Instance.UnHighliteAllSlots();


	}
	public void StartSplit(Slot slot)
	{
		if (!slot.Filled || this is CharBag) return;
		InventoryManager.Instance.StartSplitSlot = slot;
		InventoryManager.Instance.SplitStackValue.text = InventoryManager.Instance.ItemsValue.value.ToString();

		InventoryManager.Instance.Split.SetActive(true);
		int start = slot.Items.Count;
		InventoryManager.Instance.ItemsValue.maxValue = start;
		InventoryManager.Instance.ItemsValue.minValue = 0;
		AddFromTo(slot, InventoryManager.Instance.SplitSlot);


		InventoryManager.Instance.ItemsValue.onValueChanged.AddListener(delegate { HandleSplitCount(); });

	}
	private void HandleSplitCount()
	{
		InventoryManager.Instance.SplitStackValue.text = InventoryManager.Instance.ItemsValue.value.ToString();

	}
	public virtual void Open() { canvasGroup.alpha = 1; canvasGroup.blocksRaycasts = true; Inventory.Instance.CurrentBag = this; opened = true; }
	public virtual void Close() { canvasGroup.alpha = 0; canvasGroup.blocksRaycasts = false; opened = false; }
}
