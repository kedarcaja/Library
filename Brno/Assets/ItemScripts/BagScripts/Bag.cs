using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "bag", menuName = "Inventory/New Bag")]
public class Bag : ScriptableObject
{
	[SerializeField]
	private int level;
  
    [SerializeField]
    private int slotCount, columns;
    public int Columns { get { return columns; } }
    public List<Slot> Slots;
    public bool Full { get { return Slots.All(s => s.Full); } }
    public bool AllSlotsFilled { get { return Slots.All(s => s.Filled); } }
	[SerializeField]
	private bool otherItemUsage;
    [SerializeField]
    private ItemType type;
    public ItemType Type { get { return type; } set { type = value; } }
    public BagScript BagScript { get; set; }

	public int SlotCount
	{
		get
		{
			return slotCount;
		}

		set
		{
			if(slotCount==0)
			slotCount = value;
		}
	}

	public int Level
	{
		get
		{
			return level;
		}

		set
		{
			level = value;
		}
	}

	public bool OtherItemUsage
	{
		get
		{
			return otherItemUsage;
		}

		
	}
}
