using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Weapon", fileName = "Weapon")]
public class Weapon : Equipment
{

	public int Damage, FireAttack, WaterAttack, ColdAttack;
	public double AttackSpeed;
	[SerializeField]
	private float range;
	public float Range { get { return range; } }
	public override void GetTooltip()
	{
		Tooltip.Instance.SetTooltip(name, ItemType.ToString(), Damage.ToString(), "dame", AttackSpeed.ToString(),
											"AttackSpeed", Quality.ToString(), Sprite, GetStats(), description, SellGold.ToString(),
											 SellSilver.ToString(), SellCopper.ToString()/*,Gems.Count>0?Gems[0].Sprite:null*/,QualityColor);
	}
	protected override string GetStats()
	{
		string s = "";
		if (Comparable())
		{
			s = base.GetStats() + "\nFireAttack: " + FireAttack + CompareVariable(FireAttack, (OnCharPanel as Weapon).FireAttack)
									   + "\nWaterAttack: " + WaterAttack + CompareVariable(WaterAttack, (OnCharPanel as Weapon).WaterAttack)
									   + "\nColdAttack: " + ColdAttack + CompareVariable(ColdAttack, (OnCharPanel as Weapon).ColdAttack);
			return s;

		}
		s = base.GetStats() + "\nFireAttack: " + FireAttack + "\nWaterAttack: " + WaterAttack + "\nColdAttack: " + ColdAttack;
		return s;

	}

}
