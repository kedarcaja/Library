using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMProTextHighliter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	private Color color;
	public bool IsHighlited { get; private set; }
	public Color Color
	{
		get
		{
			return color;
		}

	
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		ChangeColor(color);
		IsHighlited = true;


	}

	public void OnPointerExit(PointerEventData eventData)
	{
		ChangeColor(Color.black);
		IsHighlited = false;


	}
	public void ChangeColor(Color col)
	{
		GetComponent<TextMeshProUGUI>().color = col;
		IsHighlited = col == color;
	}
}

