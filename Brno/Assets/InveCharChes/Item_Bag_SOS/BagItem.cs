using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/BagItem", fileName = "NewBagitem")]
public class BagItem : Item
{
	[SerializeField]
	private Bag bag;
	public override void Use()
	{
		bag.BagScript.LevelUp();
		bag.BagScript.RemoveItems(CurrentSlot,1);
	}
	
}
