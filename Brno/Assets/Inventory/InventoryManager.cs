using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
	#region Variables
	public SackScript SackNearby { get; set; }
	[SerializeField]
	private List<Sprite> qualityColors = new List<Sprite>();
	[SerializeField]
	private List<EQuality> quality = new List<EQuality>();
	[SerializeField]
	private Sprite emptyItem;
	[SerializeField]
	private GameObject slotPrefab, bagPrefab, movingSlot, bigSlot, sack, sackUI, sackSlotsPrefab, dropSackSlotsParent, dropSacksParent, staticSackSlotsParent, staticSacksParent, sackUIParent, sackTakeAllLable,chest;
	[SerializeField]
	private GameObject book;
	public GameObject Book { get { return book; } }
	
	[SerializeField]
	private Sprite defaultBG;
	public Slot From = null;
	public GameObject SlotPrefab
	{
		get
		{
			return slotPrefab;
		}
	}

	public GameObject BagPrefab
	{
		get
		{
			return bagPrefab;
		}
	}
	public bool IsDraging { get; set; }
	public GameObject MovingSlot
	{
		get { return movingSlot; }
	}

	public Sprite EmptyItem
	{
		get
		{
			return emptyItem;
		}


	}

	public Slider ItemsValue
	{
		get
		{
			return itemsValue;
		}


	}

	public Slot SplitSlot
	{
		get
		{
			return splitSlot;
		}
	}

	public GameObject Split
	{
		get
		{
			return split;
		}

	}

	public Text SplitStackValue
	{
		get
		{
			return splitStackValue;
		}


	}

	public Sprite DefaultBG
	{
		get
		{
			return defaultBG;
		}


	}

	[SerializeField]
	private Text splitStackValue;
	[SerializeField]
	private Slot splitSlot;

	[SerializeField]
	private Slider itemsValue;
	public static InventoryManager Instance;
	[SerializeField]
	private GameObject split;
	public Slot StartSplitSlot { get; set; }

	public List<EQuality> Quality
	{
		get
		{
			return quality;
		}


	}

	public List<Sprite> QualityColors
	{
		get
		{
			return qualityColors;
		}


	}
	private Dictionary<EQuality, Sprite> qualityDic = new Dictionary<EQuality, Sprite>();
	public Dictionary<EQuality, Sprite> QualityDic { get { return qualityDic; } }

	public GameObject BigSlot
	{
		get
		{
			return bigSlot;
		}



	}

	public GameObject Sack
	{
		get
		{
			return sack;
		}


	}

	public GameObject SackUI
	{
		get
		{
			return sackUI;
		}


	}

	public GameObject SackSlotsPrefab
	{
		get
		{
			return sackSlotsPrefab;
		}


	}

	public GameObject DropSackSlotsParent
	{
		get
		{
			return dropSackSlotsParent;
		}


	}
	public GameObject DropSacksParent
	{
		get
		{
			return dropSacksParent;
		}


	}
	public GameObject StaticSacksParent
	{
		get
		{
			return staticSacksParent;
		}


	}
	public GameObject StaticSackSlotsParent
	{
		get
		{
			return staticSackSlotsParent;
		}


	}

	public GameObject SackUIParent
	{
		get
		{
			return sackUIParent;
		}


	}

	public GameObject SackTakeAllLable
	{
		get
		{
			return sackTakeAllLable;
		}


	}

	public GameObject Chest
	{
		get
		{
			return chest;
		}

	
	}
	[SerializeField]
	private List<GameObject> DontDestroy = new List<GameObject>();// přesunot do nějakého scriptu na řízení světa $
	#endregion
	private void Awake()
	{

		for (int i = 0; i < DontDestroy.Count; i++)// $
		{
			DontDestroyOnLoad(DontDestroy[i]);
		}

		//\$
		for (int i = 0; i < quality.Count; i++)
		{
			qualityDic.Add(quality[i], qualityColors[i]);

		}
	}
	private void Start()
	{
		Instance = FindObjectOfType<InventoryManager>();


	}

	public void MoveItem(Slot from)
	{
		for (int i = 0; i < from.Items.Count; i++)
		{
			movingSlot.GetComponent<Slot>().Add(from.CurrentItem);
		}
		MovingSlot.transform.Find("quality").GetComponent<Image>().color = new Color(0, 0, 0, 0);
		from.RemoveAll();
		MovingSlot.transform.SetAsLastSibling();
	
		IsDraging = true;
	}
	private void Update()
	{
		if (IsDraging)
		{
			movingSlot.transform.position = Input.mousePosition;

		}
		if (Input.GetKeyDown(KeyCode.I))
		{
			OpenCloseBook();
		}

	}
	public void ClearSlot(Slot slot)
	{
		slot.RemoveAll();
	}
	public void ClearMovingSlot()
	{
		ClearSlot(MovingSlot.GetComponent<Slot>());
		IsDraging = false;
		From = null;
	}
	public void UnHighliteAllSlots()
	{
		foreach (BagScript b in FindObjectsOfType<BagScript>().Where(b => !(b is CharBag) && !(b is SackScript)&& !(b is Chest)))
		{
			foreach (Slot s in b.Bag.Slots)
			{
				s.GetComponent<Outline>().enabled = false;
			}
		}
	}
	public void UnHighliteSlot(Slot slot)
	{
		if (slot.GetComponent<Outline>() != null)
			slot.GetComponent<Outline>().enabled = false;
	}
	public void OpenInventoryBag(BagScript bag)
	{
		foreach (BagScript b in FindObjectsOfType<Inventory>().ToList().Find(i=>!(i is Chest)).Bags)
		{
			b.Close();
		}
		bag.Open();
	}
	public void OpenCloseBook()
	{
		BagScript def = Inventory.Instance.Bags[1];
		if ((FindObjectOfType<Book>().Opened))
		{
			FindObjectOfType<Book>().Close();
		}
		else
		{
			FindObjectOfType<Book>().Open();

		}
		OpenInventoryBag(def);
	}
	public void SplitItems()
	{

		int r;
		BagScript bag = StartSplitSlot.Bag;
		if (int.Parse(SplitStackValue.text) == 0)
		{
			bag.AddFromTo(SplitSlot, StartSplitSlot);
			Split.SetActive(false);
			ResetSplit();

			return;
		}
		bag.AddItems(SplitSlot.CurrentItem, StartSplitSlot, r = (int)itemsValue.maxValue - int.Parse(SplitStackValue.text));
		for (int i = 0; i < r; i++)
		{
			SplitSlot.Remove();

		}
		MoveItem(splitSlot);
		Split.SetActive(false);
		From = StartSplitSlot;
		ResetSplit();
	}
	private void ResetSplit()
	{
		itemsValue.minValue = 0;
		itemsValue.maxValue = 0;
		itemsValue.value = 0;
		ClearSlot(SplitSlot);
		SplitStackValue.text = 0.ToString();
	}
	private void CountSliderUp()
	{

		if (ItemsValue.value + 1 <= ItemsValue.maxValue)
		{
			ItemsValue.value += 1;

		}
	}
	private void CountSliderDown()
	{

		if (ItemsValue.value - 1 >= ItemsValue.minValue)
		{
			ItemsValue.value -= 1;

		}
	}
	/// <summary>
	/// a´ktualizuje všechny staty po equipnutí itemu
	/// </summary>
	public void UpdateAllStats()
	{
		foreach (Stat s in FindObjectsOfType<Stat>())
		{
			s.UpdateValue();
		}
	}

}

