using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    #region Variables
    private static InventoryManager instance;

    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
            }
            return instance;
        }

       
    }
    public GameObject slotPrefab;
    public GameObject BackgroundSlotPrefab;
    public GameObject IconPrefab;
    private GameObject hoverObject;
    public GameObject HoverObject
    {
        get
        {
            return hoverObject;
        }

        set
        {
            hoverObject = value;
        }
    }
    public GameObject Mana;
    public GameObject Health;
    public GameObject Weapon;
    public GameObject dropItem;
    public GameObject tooltipObject;
    public Text sizeTextObject;
    public Text visialTextObject;
    public Canvas canvas;
    private Slot from;
    public Slot From
    {
        get
        {
            return from;
        }

        set
        {
            from = value;
        }
    }
    private Slot to;
    public Slot To
    {
        get
        {
            return to;
        }

        set
        {
            to = value;
        }
    }
    private GameObject clicked;
    public GameObject Clicked
    {
        get
        {
            return clicked;
        }

        set
        {
            clicked = value;
        }
    }
    public Text StackText;
    public GameObject selectStackSize;
    public EventSystem eventSystem;
    private int spliteAmount;
    public int SpliteAmount
    {
        get
        {
            return spliteAmount;
        }

        set
        {
            spliteAmount = value;
        }
    }
    private int maxStackCount;
    public int MaxStackCount
    {
        get
        {
            return maxStackCount;
        }

        set
        {
            maxStackCount = value;
        }
    }

    public Slot MovingSlot
    {
        get
        {
            return movingSlot;
        }

        set
        {
            movingSlot = value;
        }
    }

    private Slot movingSlot;
    #endregion

    #region Unity Metod

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void SetStackInfo(int MaxStackCount) //Nastavení informací o stacku
    {
        selectStackSize.SetActive(true);
        tooltipObject.SetActive(false);
        SpliteAmount = 0;
        this.MaxStackCount = MaxStackCount;
        StackText.text = SpliteAmount.ToString();
    }

    public void Save()
    {
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        foreach (GameObject inventory in inventories)
        {
            inventory.GetComponent<Inventory>().SaveInventory();
        }
    }
    public void Load()
    {
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        foreach (GameObject inventory in inventories)
        {
            inventory.GetComponent<Inventory>().LoadInventory();
        }
    }
    #endregion
}
