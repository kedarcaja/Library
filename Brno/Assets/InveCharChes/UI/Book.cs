using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
[Serializable]

public class Book : MonoBehaviour, IPointerClickHandler, IOpenable
{
	public UnityEvent EventClick, OnCloseEvent,OnOpenEvent;

	public bool Opened { get; private set; }

	public event OpenHandler OnOpen;
	public event OpenHandler OnClose;
	[SerializeField]
	private List<CanvasGroup> bookmarks = new List<CanvasGroup>();
    [SerializeField]
    private List<CanvasGroup> statsMarks = new List<CanvasGroup>();
    private CanvasGroup currentBookmark = null;
	private void Start()
	{
		Inventory.Instance.OnClose += () =>
		{
			CharacterPanel.Instance.Close();
		};
		Inventory.Instance.OnOpen += () =>
		{
			CharacterPanel.Instance.Open();
		};
	}
	public void OpenBookmark(CanvasGroup bm)
	{
		foreach (CanvasGroup c in bookmarks)
		{
			if (c.GetComponent<IOpenable>() != null)
			{
				c.GetComponent<IOpenable>().Close();
				continue;
			}
			c.alpha = 0;
			c.blocksRaycasts = false;

		}
		if (bm.GetComponent<IOpenable>() != null)
		{
			bm.GetComponent<IOpenable>().Open();
			return;
		}

		bm.alpha = 1;
		bm.blocksRaycasts = true;
	}
    public void OpenStatMark(CanvasGroup bm)
    {
        foreach (CanvasGroup c in statsMarks)
        {           
            c.alpha = 0;
            c.blocksRaycasts = false;
        }       
        bm.alpha = 1;
        bm.blocksRaycasts = true;
    }
    public void Close()
	{
		//PlayerScript.Instance.RestoreAgent();
		MouseManager.Instance.CanClick = true;

		if (OnClose != null)
		{
			OnClose();
		}
		if (OnCloseEvent != null)
		{
			OnCloseEvent.Invoke();
		}
        UIManager.Instance.ShowUI();

        GetComponent<CanvasGroup>().alpha = 0;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		Opened = false;
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		EventClick.Invoke();

	}

	public void Open()
	{
		//PlayerScript.Instance.DisableAgent();
		MouseManager.Instance.CanClick = false;

		if (OnOpen != null)
		{
			OnOpen();
		}
		if (OnOpenEvent != null)
		{
			OnOpenEvent.Invoke();
		}
        UIManager.Instance.HideUI();

		GetComponent<CanvasGroup>().alpha = 1;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		Opened = true;
	}
}
