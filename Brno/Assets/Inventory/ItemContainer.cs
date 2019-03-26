using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemContainer : MonoBehaviour, ITarget, IPointerClickHandler
{

	protected Image image;
	private Text itemCount;
	protected Slot slot;
	private IncrementTimer timer = new IncrementTimer();
	[SerializeField]
	private bool canUseItem = true;
	[SerializeField]
	private bool showUI = true;
	public Action OnUse;

    

    public Action OtherItemUsage;
	private void Start()
	{
		itemCount = transform.GetComponentInChildren<Text>();
		timer.Init(1, 0.3f, this); // timer ovládající čas objevení TT

	}
	protected virtual void Awake()
	{
		timer.OnTimerUpdate += delegate { if (timer.GetTimeFloat() == 2) { timer.Stop(); } };

		image = GetComponent<Image>();
		slot = GetComponentInParent<Slot>();
		itemCount = GetComponentInChildren<Text>();
	}


	public virtual void Add(Item item)
	{
		image.sprite = item.Sprite;
		image.raycastTarget = slot != InventoryManager.Instance.MovingSlot.GetComponent<Slot>() && slot != InventoryManager.Instance.SplitSlot ? true : false;
		itemCount.text = slot.Items.Count > 1 ? slot.Items.Count.ToString() : string.Empty;
		slot.gameObject.transform.Find("quality").GetComponent<Image>().sprite = item.QualityColor;
		slot.gameObject.transform.Find("quality").GetComponent<Image>().color = Color.white;

	}

	public virtual void Remove()
	{
		image.sprite = slot.Filled ? image.sprite : InventoryManager.Instance.EmptyItem;
		itemCount.text = slot.Items.Count > 1 ? slot.Items.Count.ToString() : string.Empty;
		image.raycastTarget = slot.Filled && slot != InventoryManager.Instance.MovingSlot.GetComponent<Slot>() ? true : false;

		if (!slot.Filled)
		{
			slot.transform.Find("quality").GetComponent<Image>().color = new Color(0, 0, 0, 0);
		}
	}

	public virtual void RemoveAll()
	{
		image.sprite = InventoryManager.Instance.EmptyItem;
		itemCount.text = string.Empty;
		image.raycastTarget = false;
		slot.transform.Find("quality").GetComponent<Image>().color = new Color(0, 0, 0, 0);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{

		if (slot == InventoryManager.Instance.MovingSlot.GetComponent<Slot>() || InventoryManager.Instance.IsDraging||!showUI) return;
		if (GetComponent<Outline>() != null)
		{
			GetComponent<Outline>().enabled = true;
		}
		if (slot.CurrentItem == null) return;


		timer.OnTimerEnd += delegate
		{

			slot.CurrentItem.GetTooltip();
			Tooltip.Instance.Open();
		};


		// pozice TT
		if (slot.Bag.Bag.Slots.IndexOf(slot) + 1 < 66 && !slot.Bag is CharBag)
			Tooltip.Instance.transform.position = new Vector3(slot.transform.position.x + 50, slot.transform.position.y);
        else if (slot.Bag is CharBag)
        {
            Tooltip.Instance.transform.position = new Vector3(Screen.width/2-400,Screen.height/2+250);
        }

		//
		timer.Start();

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (slot == InventoryManager.Instance.MovingSlot.GetComponent<Slot>()||!showUI) return;
		if (GetComponent<Outline>() != null)
		{
			GetComponent<Outline>().enabled = false;
		}
		if (slot.CurrentItem == null) return;

		timer.OnTimerEnd -= delegate
		{
			slot.CurrentItem.GetTooltip();
			Tooltip.Instance.Open();
		};
		timer.Stop();
		Tooltip.Instance.Close();

	}

	public void OnPointerClick(PointerEventData eventData)
	{
		timer.OnTimerEnd -= delegate
		{
			slot.CurrentItem.GetTooltip();
			Tooltip.Instance.Open();
		};
		timer.Stop();
		Tooltip.Instance.Close();

		if (!InventoryManager.Instance.IsDraging)
		{
			if (Input.GetKey(KeyCode.LeftShift)&&showUI)
			{
				if (!slot.Filled) return;
				if (eventData.button == PointerEventData.InputButton.Left)
				{
					slot.Bag.StartSplit(slot);
					return;
				}
			}
			if (eventData.button == PointerEventData.InputButton.Left&&showUI)
			{

				InventoryManager.Instance.OpenInventoryBag(Inventory.Instance.Bags.Find(b => b.Bag.Type == slot.CurrentItem.MainCategory));
				InventoryManager.Instance.From = slot;
				InventoryManager.Instance.MoveItem(slot);
				return;

			}
            if (eventData.button == PointerEventData.InputButton.Right && canUseItem)
            {
                slot.CurrentItem.CurrentSlot = slot;

                if (Input.GetKey(KeyCode.LeftShift)/*&&slot.CurrentItem.MainCategory ==ItemType.CONSUMEABLE*/)
                {
                    FindObjectsOfType<CharBag>().ToList().Find(b => b.Bag.name == "ActionBag" && b.name == "BookActionSlots").AddItems(slot.CurrentItem, slot.Items.Count);
                    slot.RemoveAll();
                    return;

                }
                slot.CurrentItem.Use();
                if (OnUse != null)
                {
                    OnUse();
                }
                return;

            }
        }
		if (InventoryManager.Instance.IsDraging)
		{

			Slot man = InventoryManager.Instance.MovingSlot.GetComponent<Slot>();

			if (slot.CanContain(man.CurrentItem) && man.CurrentItem == slot.CurrentItem)
			{

				slot.Bag.AddItems(man.CurrentItem, slot, man.Items.Count);
				InventoryManager.Instance.ClearMovingSlot();
				return;
			}
			if (slot.Bag == InventoryManager.Instance.From.Bag || slot.CurrentItem.MainCategory == InventoryManager.Instance.MovingSlot.GetComponent<Slot>().CurrentItem.MainCategory)
			{
				slot.Bag.DandDSwap(slot);
				return;
			}
			Inventory.Instance.PullItemBack();
		}
	}
}
