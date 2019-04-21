using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{


	public static DialogManager Instance { get; private set; }
	public AudioSource AudioPlayer { get; private set; }
	[SerializeField]
	private TextMeshProUGUI subtitleArea;
	public TextMeshProUGUI SubtitleArea { get { return subtitleArea; } }
	[SerializeField]
	private List<Button> decisionButtons = new List<Button>();

	[SerializeField]
	private TextMeshProUGUI skipAtention;
	public TextMeshProUGUI SkipAtention { get { return skipAtention; } }
	public GameObject defaultInterPref = null;
	public DialogInterpret DefaultInterpret
	{
		get;set;
	
	}

	[SerializeField]
	private int decisionIndex = 0;
	bool isInDecision = false;
	private void Awake()
	{
		Instance = FindObjectOfType<DialogManager>();
		AudioPlayer = GetComponent<AudioSource>();
	}

	public void ShowDecision(Decision dec)
	{
		SetDecisions(dec);
		decisionButtons[0].GetComponentInParent<CanvasGroup>().alpha = 1;
		decisionButtons[0].GetComponentInParent<CanvasGroup>().blocksRaycasts = true;
		isInDecision = true;
	}
	private void Update()
	{
		

		if (isInDecision)
		{
			if (decisionButtons.Exists(d => d.GetComponentInChildren<TMProTextHighliter>().IsHighlited))
			decisionIndex = decisionButtons.IndexOf(decisionButtons.ToList().Find(d => d.GetComponentInChildren<TMProTextHighliter>().IsHighlited));
			if (decisionButtons[decisionIndex].IsActive() && decisionButtons[decisionIndex].GetComponentInChildren<TextMeshProUGUI>().color != decisionButtons[decisionIndex].GetComponentInChildren<TMProTextHighliter>().Color)
			{
				decisionButtons[decisionIndex].GetComponentInChildren<TMProTextHighliter>().ChangeColor(decisionButtons[decisionIndex].GetComponentInChildren<TMProTextHighliter>().Color);
			}
			if (decisionButtons.Where(b => b.IsActive()).Count() - 1 > decisionIndex && Input.GetKeyDown(KeyCode.DownArrow))
			{
				decisionIndex++;
				decisionButtons[decisionIndex - 1].GetComponentInChildren<TMProTextHighliter>().ChangeColor(Color.black);

			}
			if (Input.GetKeyDown(KeyCode.UpArrow) && decisionIndex > 0)
			{
				decisionIndex--;
				decisionButtons[decisionIndex+1].GetComponentInChildren<TMProTextHighliter>().ChangeColor(Color.black);
			}
			if (Input.GetKeyDown(KeyCode.Backspace))
			{
				decisionButtons[decisionIndex].onClick.Invoke();
			}


			else if (Input.GetKeyDown(KeyCode.Alpha0)&&decisionButtons[0].IsActive())
			{
				decisionButtons[0].onClick.Invoke();
			}


			else if (Input.GetKeyDown(KeyCode.Alpha1) && decisionButtons[1].IsActive())
			{
				decisionButtons[1].onClick.Invoke();
			}


			else if (Input.GetKeyDown(KeyCode.Alpha2) && decisionButtons[2].IsActive())
			{
				decisionButtons[2].onClick.Invoke();
			}


			else if (Input.GetKeyDown(KeyCode.Alpha3) && decisionButtons[3].IsActive())
			{
				decisionButtons[3].onClick.Invoke();
			}
		}
	}
	private void HideDecisions()
	{
		ClearDecsions();
		decisionButtons[0].GetComponentInParent<CanvasGroup>().alpha = 0;
		decisionButtons[0].GetComponentInParent<CanvasGroup>().blocksRaycasts = false;
		isInDecision = false;



	}
	private void SetDecisions(Decision dec)
	{
		decisionButtons.ToList().ForEach(b => b.gameObject.SetActive(false));
		if (DefaultInterpret)
		{
			Destroy(DefaultInterpret.gameObject);
		}
		DefaultInterpret = Instantiate(defaultInterPref).GetComponent<DialogInterpret>();
		for (int i = 0; i < dec.Values.Count; ++i)
		{
			


			decisionButtons[i].gameObject.SetActive(true);
			int n = i;
			decisionButtons[n].GetComponentInChildren<TextMeshProUGUI>().text = dec.Values[n].Value;
			decisionButtons[n].onClick.AddListener(() =>
			{
				dec.Selected = dec.Values[n];
				if (dec.Values[n].NextDialog) {
					DefaultInterpret.dialog = dec.Selected.NextDialog;
					DefaultInterpret.Init();
					DefaultInterpret.dialog.OnStart.Invoke();
				}
				if(dec.Values[n].NextDialog)
				DefaultInterpret.dialog.OnEnd.AddListener(delegate { if (dec.NextDecision != null) ShowDecision(dec.NextDecision);  });




				


				HideDecisions();
			});

			

		}

	}
	private void ClearDecsions()
	{
		for (int i = 0; i < decisionButtons.Count; i++)
		{
			decisionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
			decisionButtons[i].onClick.RemoveAllListeners();
		}

	}

}

