using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SackObject : MonoBehaviour

{
	private bool clicked;

	private int index = 0;
	[SerializeField]
	private SackScript sack;
	private void Start()
	{
		index = transform.GetSiblingIndex();
		if (sack==null)
		{
			sack = InventoryManager.Instance.DropSackSlotsParent.transform.GetChild(index).GetComponent<SackScript>();

		}
		sack.OnOpen += new OpenHandler(() => clicked = false);
		sack.OnClose += new OpenHandler(() => clicked = false);
	}


	private void OnTriggerStay(Collider other)
	{

		if (other.transform.gameObject.name == "Player")
		{
			if (InventoryManager.Instance.SackNearby == null&&!sack.StaticContent&&sack.CanDrop)
				InventoryManager.Instance.SackNearby = sack;

			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				EventSystem m_EventSystem = FindObjectOfType<EventSystem>();
				GraphicRaycaster m_Raycaster = FindObjectOfType<Canvas>().GetComponent<GraphicRaycaster>();
				PointerEventData m_PointerEventData = new PointerEventData(m_EventSystem);
				m_PointerEventData.position = Input.mousePosition;
				List<RaycastResult> results = new List<RaycastResult>();
				m_Raycaster.Raycast(m_PointerEventData, results);

				if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
				{
					if (hit.collider.gameObject == gameObject && !sack.Opened && !InventoryManager.Instance.Book.GetComponent<Book>().Opened)
					{
						sack.Open();

					}
					else if (sack.Opened && !InventoryManager.Instance.Book.GetComponent<Book>().Opened && results.All(r => r.gameObject == null) && hit.collider.gameObject.GetComponent<Terrain>() != null)
					{
						sack.Close();
					}
				}
			}
			if (sack.Opened && Input.GetKeyDown(KeyCode.T))
			{

				sack.TakeAll();
				clicked = false;
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.gameObject.name == "Player")
		{
			if (InventoryManager.Instance.SackNearby == sack)
				InventoryManager.Instance.SackNearby = null;
			if (sack.Opened)
			{
				sack.Close();
			}
		}
	}
	private void OnDestroy()
	{
		if (InventoryManager.Instance.SackNearby == sack)
			InventoryManager.Instance.SackNearby = null;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.gameObject.name == "Player" && clicked && !sack.Opened)
		{
			sack.Open();
		}
	}
	private void OnMouseUp()
	{
		clicked = true;
	}
}
