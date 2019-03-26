using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class BigItemContainer : ItemContainer
{
	private TMPro.TextMeshProUGUI _name, type_quality, level, count;
	protected override void Awake()
	{
		base.Awake();
		_name = transform.Find("Item name").GetComponent<TMPro.TextMeshProUGUI>();
		type_quality = transform.Find("Item type_quality").GetComponent<TMPro.TextMeshProUGUI>();
		level = transform.Find("Item level").GetComponent<TMPro.TextMeshProUGUI>();
		count = transform.Find("Item count").GetComponent<TMPro.TextMeshProUGUI>();
	}
	public override void Add(Item item)
	{
		image.sprite = item.Sprite;
		count.text = slot.Items.Count > 1 ? slot.Items.Count.ToString() : string.Empty;
		slot.gameObject.transform.Find("Quality").GetComponent<Image>().sprite = item.QualityColor;
		_name.text = item.name;
		level.text = "Level: " + item.Level.ToString();
	// 	type_quality.text = item.ItemType.ToString() + "/<color=#" + ColorUtility.ToHtmlStringRGB(item.QualityColor) + ">" + item.Quality + "</color>"; 
	}
	public override void Remove()
	{

		// zničí se slot 

	}
	public override void RemoveAll()
	{
		// zničí se slot


	}

}
