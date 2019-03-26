using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chest : BagScript, IOpenable
{
	private Animator anim;
	Transform player;
	private bool trigger;
	private float Range = 2;
	public new event OpenHandler OnOpen, OnClose;
	private bool isOpen;
	public new bool Opened { get { return isOpen; } }
	private GameObject slotParent;
	public GameObject SlotParent
	{
		get
		{
			return slotParent;
		}


	}
	private bool clicked = false;
	private List<GameObject> slots;


	void Awake()
	{
		anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		slotParent = transform.Find("Slots").gameObject;
		slots = new List<GameObject>();
		bag.Slots = new List<Slot>();
		bag.BagScript = this;
		canvasGroup = GetComponent<CanvasGroup>();
		Draw();
		Close();
		FindObjectOfType<InventoryManager>().Book.GetComponent<Book>().OnClose += new OpenHandler(Close);
		OnOpen += new OpenHandler(FindObjectOfType<InventoryManager>().Book.GetComponent<Book>().Open);
		OnClose += new OpenHandler(FindObjectOfType<InventoryManager>().Book.GetComponent<Book>().Close);

	}




	public override void Open()
	{
		if (!isOpen)
		{
			anim.SetBool("Open", true);
			isOpen = true;
			clicked = false;

			foreach (Chest c in FindObjectsOfType<Chest>())
			{
				if (c != this)
				{
					c.Close();
				}
			}
			FindObjectOfType<CharacterPanel>().Close();

			slots.ForEach(a => { a.transform.parent = InventoryManager.Instance.Chest.transform;a.transform.localScale = new Vector3(1, 1, 1); });
			InventoryManager.Instance.Chest.GetComponent<CanvasGroup>().alpha = 1;
			InventoryManager.Instance.Chest.GetComponent<CanvasGroup>().blocksRaycasts = true;
			InventoryManager.Instance.Chest.GetComponent<GridLayoutGroup>().constraintCount = bag.Columns;
			if (OnOpen != null)
			{
				OnOpen();
			}
		}
	}

	public override void Close()
	{

		if (isOpen)
		{
			anim.SetBool("Open", false);
			isOpen = false;
			InventoryManager.Instance.Chest.GetComponent<CanvasGroup>().alpha = 0;
			InventoryManager.Instance.Chest.GetComponent<CanvasGroup>().blocksRaycasts = false;

			for (int i = 0; i < slots.Count; i++)
			{
				slots[i].transform.parent = slotParent.transform;
			}
			
			FindObjectOfType<CharacterPanel>().Open();

			if (OnClose != null)
			{
				OnClose();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (isOpen)
		{
			Close();
		}
	}
	private void OnTriggerStay(Collider other)
	{
		RaycastHit hit;
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && other.transform.gameObject.name == "SackTrigger")
		{
			if (hit.collider.gameObject == gameObject && !Opened)
			{
				Open();
			}
			clicked = false;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (clicked && !Opened)
		{
			Open();
		}
	}
	public override void Draw()
	{

		slotParent.GetComponent<GridLayoutGroup>().constraintCount = bag.Columns;
		for (int i = 0; i < bag.SlotCount; i++)
		{
			GameObject slo = Instantiate(FindObjectOfType<InventoryManager>().SlotPrefab);
			slo.transform.parent = slotParent.transform;
			slo.GetComponent<Slot>().Bag = this;
			slo.GetComponent<Slot>()._CanContain = bag.Type;
			bag.Slots.Add(slo.GetComponent<Slot>());
			slots.Add(slo);

		}
	}
	private void OnMouseUp()
	{
		clicked = true;
	}
}
