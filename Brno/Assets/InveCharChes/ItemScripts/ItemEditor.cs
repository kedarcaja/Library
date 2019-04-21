using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;
#if UNITY_EDITOR
[ItemCustomEditorAtributte(typeof(Item), true),CanEditMultipleObjects]
public class ItemEditor : Editor
{
	private ItemType mainCategory;
	private Item item;

	private List<string> categories = new List<string>() { "EQUIPMENT", "WEAPON", "GEM", "CONSUMEABLE", "MATERIAL", "QUEST_OTHER", "ALCHEMY" };
	private void OnEnable()
	{
		item = target as Item;

	}
	public override void OnInspectorGUI()
	{

		//start of Image field
		GUILayout.BeginVertical();
		var style = new GUIStyle(GUI.skin.label);
		style.alignment = TextAnchor.UpperCenter;
		style.fixedWidth = 70;
		GUILayout.Label(name, style);
	item.Sprite = (Sprite)EditorGUILayout.ObjectField(item.Sprite, typeof(Sprite), false, GUILayout.Width(150), GUILayout.Height(150));
	
		GUILayout.EndVertical();
		//end of Image field
		item.SelectedCategoryIndex = EditorGUILayout.Popup(item.SelectedCategoryIndex, categories.ToArray(), GUILayout.Width(200));
		item.SelectedTypeIndex = EditorGUILayout.Popup(item.SelectedTypeIndex, ItemTypeList().ToArray(), GUILayout.Width(200));
		item.MainCategory = (ItemType)Enum.Parse(typeof(ItemType), categories[item.SelectedCategoryIndex]);
		item.ItemType = (ItemType)Enum.Parse(typeof(ItemType), ItemTypeList()[item.SelectedTypeIndex]);

		serializedObject.ApplyModifiedProperties();
		serializedObject.Update();
		EditorUtility.SetDirty(target);
		DrawDefaultInspector();

		


	}


	

	public List<string> ItemTypeList()
	{
		mainCategory = item.MainCategory;
		// fill all ItemTypes
		List<string> list = new List<string>();
		switch (mainCategory)
		{
			case ItemType.GEM:


				list.Add("GEM");

				break;

			case ItemType.EQUIPMENT:
				list.AddRange(new string[] { "Chest", "Helm", "Arm", "Trausers","Spaulder","Belt","Ring0", "Ring1", "Necklance", "Boots","Shirt", "Pickaxe", "Shovel", "FishingRod", "Axe", "Sickle","Hood" });


                break;
			case ItemType.WEAPON:
				list.AddRange(new string[] { "MeelWeapon","Bow","Arrow", "SecondHand", });

				break;
			case ItemType.CONSUMEABLE:
				list.AddRange(new string[] { "HealthPotion", "Food" });

				break;
			case ItemType.MATERIAL:
				list.AddRange(new string[] { "Fuel", "Ore" });

				break;
			case ItemType.QUEST_OTHER:
				list.Add("QUEST_OTHER");

				break;
			case ItemType.ALCHEMY:{
				list.Add("ALCHEMY");

			}	break;

		}
		return list;
	}
}
#endif
