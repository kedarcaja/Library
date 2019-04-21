using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Armor", fileName = "Armor")]
public class Armor : Equipment
{

	public int FireResistance, WaterResistance, ColdResistance, DamageResistance, _Armor;

	// porovnání hlavních hodnot dodělat
	public override void GetTooltip()
	{
		Tooltip.Instance.SetTooltip(name, ItemType.ToString(), _Armor.ToString(), "armor", DamageResistance.ToString(),
											"DamageResistance", Quality.ToString(), Sprite, GetStats(), description, SellGold.ToString(),
											 SellSilver.ToString(), SellCopper.ToString(), QualityColor);
	}
	protected override string GetStats()
	{
		string s = "";
		if (Comparable())
		{
			s = base.GetStats() + "\nFireResistance: " + FireResistance + CompareVariable(FireResistance, (OnCharPanel as Armor).FireResistance)
							    + "\nWaterResistance: " + WaterResistance + CompareVariable(WaterResistance, (OnCharPanel as Armor).WaterResistance)
							    + "\nColdResistance: " + ColdResistance + CompareVariable(ColdResistance, (OnCharPanel as Armor).ColdResistance);
			return s;
		}
		s = base.GetStats() + "\nFireResistance: " + FireResistance + "\nWaterResistance: " + WaterResistance + "\nColdResistance: " + FireResistance;
		return s;
	}
}
