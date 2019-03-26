using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Linq;
// probrat secondary

public class Stat : MonoBehaviour
{
	/// <summary>
	///  musí být pojmenovány stejně jako na itemu
	/// </summary>
	private enum EVariables { Damage, Durrability, Health, Armor, Stamina, Intellect, Strngth,Resistance,Knowledge,
								FireResistance, WaterResistance, MagicResistance, EarthResistance, ColdResistance,MagicEnergy,
		Representativeness,Speed,DiveSpeed,Speaking,Fury,Thirst,Hunger,FoodOver,Poison,Conspicuity,Noisy,Smell,Tired,
	}
	[SerializeField]
	private EVariables variable;
	private Text value;
	private double summ = 0;
	public Text Value
	{
		get
		{
			return value;
		}


	}
	private Button butt;

	private GameObject content;


	void Start()
	{
		//butt.onClick.AddListener(() => ToggleContent());
		butt = transform.Find("Icon").GetComponent<Button>();
		value = transform.Find("Count").GetComponent<Text>();
		//content = transform.Find("Content").gameObject;
		UpdateValue();
		if (transform.Find("+") != null)
		{
			transform.Find("+").GetComponent<Button>().onClick.AddListener(() => UseSkillPoint());

		}
	}


	/// <summary>
	/// zobrazení podrobnějších informací o statu
	/// </summary>
	private void ToggleContent()
	{
		content.SetActive(!content.activeSelf);
	}
	public void UpdateValue()
	{
		value.text = (summ + SummEquipedItemsVariable(variable.ToString())).ToString();
	}
	

	/// <summary>
	/// sečte všechny statové proměnné, které jsou na nasazených itemech
	/// </summary>
	public double SummEquipedItemsVariable(string variableName)
	{
		double variable = 0;
		for (int i = 0; i < (CharacterPanel.Instance.Bags as CharBag[]).Length; i++)
		{
			for (int j = 0; j < CharacterPanel.Instance.Bags[i].Bag.Slots.Count; j++)
			{
				Slot tmp = (CharacterPanel.Instance.Bags[i] as CharBag).Bag.Slots[j];
				if (tmp.CurrentItem == null) continue;
				//properties
				for (int t = 0; t < tmp.CurrentItem.GetType().GetProperties().Length; t++)
				{
					PropertyInfo prop = tmp.CurrentItem.GetType().GetProperties()[t];
					if (prop.Name.ToLower().Trim() == variableName.ToLower().Trim())
					{
						variable += double.Parse(prop.GetValue(tmp.CurrentItem, null).ToString());
					}
				}
				// fields
				for (int t = 0; t < tmp.CurrentItem.GetType().GetFields().Length; t++)
				{
					FieldInfo prop = tmp.CurrentItem.GetType().GetFields()[t];
					if (prop.Name.ToLower().Trim() == variableName.Trim().ToLower())
					{
						variable += double.Parse(prop.GetValue(tmp.CurrentItem).ToString());
					}
				}
			}
		}


		return variable;
		// převést resistence na %
	}

	public void UseSkillPoint()
	{
		if (HeroController.Instance.SkillPoints == 0) { Debug.Log("<color=red>You dont have enough skill points</color>"); return; };
		HeroController.Instance.SkillPoints--;
		summ++;
		Debug.Log("<color=green>" + "Your " + variable.ToString() + " was improved</color>");
		UpdateValue();    // change Player to HeroController

	}
	
}
