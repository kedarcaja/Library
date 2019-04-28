using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory/Equipment")]
public class Equipment : Item
{
	public int Strength, Dexterity, Intellect, Stamina;
	[SerializeField]
	private bool equipable;
	public bool Equiped = false;
	protected bool Equipable
	{
		get
		{
			return equipable;
		}
	}
	[HideInInspector]
	public Equipment OnCharPanel;
	public event Action OnGetTooltip;

	[Header("Fyzical")]

	[SerializeField]
	protected Material mat;
	[SerializeField]
	protected Mesh mesh;

	[SerializeField]
	private GameObject helm;
	public override void Use()
	{
		//if (!PlayerScript.Instance.CanSwapEquipment) return;
		//if (!equipable || PlayerScript.Instance.Stats.Level < level) return;
		if (!Equiped)
		{

			CharBag bag = CharacterPanel.Instance.Bags.ToList().Find(b => b.Bag.Slots.Exists(s => s.CanContain(this)));

			if (bag != null)
			{
				Equiped = true;
				Slot s = null;

				foreach (Slot slot in bag.Bag.Slots)
				{
					if (slot._CanContain == ItemType)
					{
						s = slot;
						FyzicalEquip(s.FyzicPlacementInNotUse);
						break;
					}


				}
				if (s == null)
				{
					foreach (Slot slot in bag.Bag.Slots)
					{
						if (slot._CanContain != ItemType && slot._CanContain == MainCategory)
						{
							s = slot;
							FyzicalEquip(s.FyzicPlacementInNotUse);
							break;
						}
					}
				}
				if (s != null)
				{
					if (s.Filled)
					{
						(s.CurrentItem as Equipment).Equiped = false;
						(s.CurrentItem as Equipment).FyzDeEquip(CurrentSlot.FyzicPlacementInNotUse);
						FyzicalEquip(s.FyzicPlacementInNotUse);
						s.Bag.SwapItems(CurrentSlot, s);
						InventoryManager.Instance.UpdateAllStats();

						return;
					}
					s.Bag.AddFromTo(CurrentSlot, s);

					if (this.MainCategory == ItemType.WEAPON && this.ItemType == ItemType.TwoHand)
					{

						Slot sec = null;
						foreach (Slot slo in bag.Bag.Slots)
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
							(sec.CurrentItem as Equipment).FyzDeEquip(CurrentSlot.FyzicPlacementInNotUse);
							sec.Bag.AddFromTo(sec, Inventory.Instance.Bags.Find(c => c.Bag.Type == sec.CurrentItem.MainCategory).EmptySlot);
						}
					}
					else if (this.ItemType == ItemType.SecondHand)
					{

						Slot slt = FindObjectsOfType<Slot>().ToList().Find(o => o._CanContain == ItemType.WEAPON && o.Bag is CharBag);
						if (slt.Filled && slt.CurrentItem.ItemType == ItemType.TwoHand)
						{

							(slt.CurrentItem as Equipment).Equiped = false;
							(slt.CurrentItem as Equipment).FyzDeEquip(CurrentSlot.FyzicPlacementInNotUse);
							slt.Bag.AddFromTo(slt, Inventory.Instance.Bags.Find(b => b.Bag.Type == slt.CurrentItem.MainCategory).EmptySlot);
						}
					}
					InventoryManager.Instance.UpdateAllStats();


					return;
				}
			}
			InventoryManager.Instance.UpdateAllStats();


			return;
		}

		if (Equiped)
		{
			Inventory inv = Inventory.Instance;
			BagScript ba = inv.Bags.Find(b => b.Bag.Type == MainCategory);
			ba.AddFromTo(CurrentSlot, ba.EmptySlot);
			Equiped = false;
			InventoryManager.Instance.UpdateAllStats();


		}
	}
	protected override string GetStats()
	{
		if (OnGetTooltip != null)
		{
			OnGetTooltip();
		}
		if (Comparable())
		{
			return base.GetStats() + "\nStrenght: " + Strength + CompareVariable(Strength, OnCharPanel.Strength)
													 + "\nDexterity: " + Dexterity + CompareVariable(Dexterity, OnCharPanel.Dexterity)
													 + "\nIntellect: " + Intellect + CompareVariable(Intellect, OnCharPanel.Intellect)
													 + "\nStamina: " + Stamina + CompareVariable(Stamina, OnCharPanel.Stamina);
		}
		return base.GetStats() + "\nStrenght: " + Strength + "\nDexterity: " + Dexterity + "\nIntellect: " + Intellect + "\nStamina: " + Stamina;
	}
	public virtual string CompareVariable(double invVal, double charVal)
	{

		if (!Comparable() || invVal - charVal == 0) return "";


		double diff = invVal - charVal;
		return invVal - charVal > 0 ? string.Format(" <color=green>+" + diff + "</color>") : string.Format(" <color=red>" + diff + "</color>");
	}
	public virtual bool Comparable()
	{
		if (OnCharPanel != null)
			OnCharPanel = CharacterPanel.Instance.Bags[0].Bag.Slots.Find(s => s.CanContain(this)).CurrentItem as Equipment;
		return OnCharPanel != null && OnCharPanel != this;
	}

	protected void FyzicalEquip(Transform t1)
	{
		if (t1 == null) return;
		if (this is Weapon)
		{
			EquipWeapon(t1.gameObject);
			return;
		}
		if (ItemType == ItemType.Helm)
		{
			t1.gameObject.SetActive(true);
		}
		//t1.GetComponent<MeshRenderer>().sharedMaterial = mat;
		//t1.GetComponent<MeshFilter>().mesh = mesh;

	}
	public void EquipWeapon(GameObject w)
	{
		w.SetActive(true);
	}
	public void DrawWeapon(GameObject w_old, GameObject w_new)
	{
		w_old.SetActive(false);
		w_new.SetActive(true);
	}
	public void FyzDeEquip(Transform t1)
	{
		if (t1 == null) return;

		if (this is Weapon)
		{
			t1.gameObject.SetActive(false);
			//PlayerScript.Instance.HideSword();
			return;
		}
		if (ItemType == ItemType.Helm)
		{
			helm.SetActive(false);
		}

	}
}
