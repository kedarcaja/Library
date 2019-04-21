using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class SackScript : BagScript
{
	[SerializeField]
	private bool staticContent = false, canDrop = true;
	[SerializeField]
	private List<ItemReward> items;
	public GameObject Sack;
	private GameObject content;
	public List<BigSlot> Slots
	{
		get
		{
			return slots;
		}



	}

	public bool StaticContent
	{
		get
		{
			return staticContent;
		}

	}

	public bool CanDrop
	{
		get
		{
			return canDrop;
		}
	}

	private List<BigSlot> slots = new List<BigSlot>();
	private void Awake()
	{
		content = gameObject;
		FindObjectOfType<Book>().OnOpen += new OpenHandler(Close);
	}
	private void Start()
	{
		FillStaticContent();

	}
	public void FillStaticContent()
	{
		if (StaticContent)
		{

			for (int i = 0; i < items.Count; i++)
			{
				if(items[i].Item!=null&&items[i].Count>0)
				CreateBigSlot(items[i].Item, items[i].Count);
			}
		}
	}

	public void TakeAll()
	{
		PlayerScript.Instance.Gathering();
		Resize();
		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i] == null) continue;


			slots[i].TakeItem();
		}
		Close();
		Destroy(Sack);

	}
	public void CreateBigSlot(Item item, int count)
	{
		
		BigSlot b = (Instantiate(FindObjectOfType<InventoryManager>().BigSlot, content.transform).GetComponent<BigSlot>());
		for (int i = 0; i < count; i++)
		{
			b.Add(item);
		}
		if (content.transform.childCount > 5)
		{
			Resize();
		}
		b.Bag = this;
		slots.Add(b);
	}
	public override void Open()
	{
		ShowSackUI();
		InventoryManager.Instance.SackUIParent.GetComponent<ScrollRect>().content = GetComponent<RectTransform>();
		transform.position = new Vector3(InventoryManager.Instance.SackUIParent.transform.position.x - 41, InventoryManager.Instance.SackUIParent.transform.position.y);
		transform.parent = InventoryManager.Instance.SackUIParent.transform;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		GetComponent<CanvasGroup>().alpha = 1;
		opened = true;
	}
	public override void Close()
	{
		HideSackUI();
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		GetComponent<CanvasGroup>().alpha = 0;
		opened = false;
		transform.parent = StaticContent ? InventoryManager.Instance.StaticSackSlotsParent.transform : InventoryManager.Instance.DropSackSlotsParent.transform;


	}
	public void HideSackUI()
	{
		GameObject.Find("SackUI").GetComponent<CanvasGroup>().blocksRaycasts = false;
		GameObject.Find("SackUI").GetComponent<CanvasGroup>().alpha = 0;
	}
	public void ShowSackUI()
	{
		GameObject.Find("SackUI").GetComponent<CanvasGroup>().blocksRaycasts = true;
		GameObject.Find("SackUI").GetComponent<CanvasGroup>().alpha = 1;
	}
	private void OnDestroy()
	{
		InventoryManager.Instance.Book.GetComponent<Book>().OnOpen -= new OpenHandler(Close);

	}

	public void Resize()
	{
		content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x,
																48 * (content.GetComponent<RectTransform>().transform.childCount + 1));
	}

}
