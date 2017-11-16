using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Samples : MonoBehaviour
{
	[Header("Dynamic List")]
	public Transform listParent; 
	public GameObject listItemPrefab;
    public Text text;

	[Header("Modal")]
	public CanvasGroup modal;
	public float speed = 1f;
    int cislo;



	public void AddListItem()
	{
		Instantiate(listItemPrefab, listParent, false);
        text.text = "prvek" + cislo;
        cislo += 1;
	}

	public void ShowModal()
	{
		StartCoroutine("FadeModalIn");
	}

	public void HideModal()
	{
		StartCoroutine("FadeModalOut");
	}

	private IEnumerator FadeModalIn()
	{
		modal.blocksRaycasts = true;
		modal.interactable   = true;
		
		while(modal.alpha < 1) {
			//modal.alpha += Time.deltaTime * speed;
			yield return null;
		}
	}

	private IEnumerator FadeModalOut()
	{
		while(modal.alpha > 0){
			//modal.alpha -= Time.deltaTime * speed;
			yield return null;
		}

		modal.blocksRaycasts = false;
		modal.interactable   = false;
	}
}