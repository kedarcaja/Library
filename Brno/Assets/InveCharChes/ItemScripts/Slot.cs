using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public delegate void SlotHandler(Item item);
public class Slot : MonoBehaviour, ICollection, ITarget
{
	#region Variables
	protected SlotStack<Item> items = new SlotStack<Item>();
	public SlotStack<Item> Items { get { return items; } }
	public Item CurrentItem { get { if (Filled) return items.Peek(); return null; } }
	public bool Filled { get { return Items.Count > 0; } }
	public BagScript Bag { get; set; }
	protected ItemContainer container;
	public bool IsPlayerSlot { get { return Bag is CharBag; } }
	[SerializeField]
	private Slot remoteSlot;
	[SerializeField]
	protected ItemType canContain;
	public bool Full { get { return Filled && CurrentItem.MaxCount == items.Count; } }
	protected Sprite backg;
	public event Action  OnRemove, OnClear;
	public event SlotHandler OnAdd;
	[SerializeField]
	private Transform fyzicPlacementInUse,fyzicPlacementInNotUse;
	public ItemContainer Container
	{
		get
		{
			return container;
		}
	}
	public ItemType _CanContain
	{
		get
		{
			return canContain;
		}

		set
		{
			if (canContain == 0)
				canContain = value;
		}
	}

	public Transform FyzicPlacementInUse
	{
		get
		{
			return fyzicPlacementInUse;
		}

	
	}

	public Transform FyzicPlacementInNotUse
	{
		get
		{
			return fyzicPlacementInNotUse;
		}

		
	}
	#endregion
	protected virtual void Awake()
	{
		container = transform.GetComponentInChildren<ItemContainer>();
		items.OnClear += new SlotStackHandler(container.RemoveAll);
		items.OnPop += new SlotStackHandler(container.Remove);
		if (GetComponent<Image>() != null)
		{
			backg = GetComponent<Image>().sprite;
		}

		if (remoteSlot&&remoteSlot.container)
		{

			remoteSlot.container.OnUse += Remove;
			container.OnUse += remoteSlot.Remove;
		}
	}
	public void Add(Item item)
	{
		if (OnAdd != null)
			{
				OnAdd(item);
			}
		if (this.Bag is CharBag)
		{
			GetComponent<Image>().sprite = InventoryManager.Instance.DefaultBG;
		}
		if (remoteSlot)
		{
			remoteSlot.Add(item);
		}
		Items.Push(item);
		container.Add(item);
	}
	public virtual void Remove()
	{
		if (OnRemove != null)
		{
			OnRemove();
		}
		if (Filled)
		{
			if (GetComponent<Image>() != null)
			{
				GetComponent<Image>().sprite = InventoryManager.Instance.DefaultBG;
			}

			Items.Pop();
		}
		if (!Filled)
		{
			if (GetComponent<Image>() != null)
			{
				GetComponent<Image>().sprite = backg;
				if (remoteSlot != null)
				{
					remoteSlot.Remove();


				}
			}
		}
	}
	public virtual void RemoveAll()
	{
		if (GetComponent<Image>() != null)
			GetComponent<Image>().sprite = backg;
		if (remoteSlot)
		{
			remoteSlot.RemoveAll();

		}
		if (OnClear != null)
		{
			OnClear();
		}
		Items.Clear();
	}
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		if (!Filled && GetComponent<Outline>() != null)
		{

			GetComponent<Outline>().enabled = true;
		}
	}
	public virtual void OnPointerExit(PointerEventData eventData)
	{
		if (!Filled && GetComponent<Outline>() != null)
		{
			GetComponent<Outline>().enabled = false;
		}
	}
	public virtual void OnPointerClick(PointerEventData eventData)
	{
		if (InventoryManager.Instance.IsDraging && eventData.button == PointerEventData.InputButton.Left)
		{
			if (CanContain(InventoryManager.Instance.MovingSlot.GetComponent<Slot>().CurrentItem))
			{
				Bag.AddFromTo(InventoryManager.Instance.MovingSlot.GetComponent<Slot>(), this);
				InventoryManager.Instance.ClearMovingSlot();
				return;
			}
			Inventory.Instance.PullItemBack();
		}
	}
	public virtual bool CanContain(Item item)
	{
		
		if (canContain == ItemType.Generic)
		{
			return true;
		}
		if (item.ItemType == canContain)
		{
			return true;
		}

		if (this.canContain == item.ItemType && item is Equipment && Bag.Bag.Type == ItemType.CHAR)
		{
			return true;
		}
		if (canContain == item.MainCategory)
		{
			return true;
		}


		return false;
	}
}
