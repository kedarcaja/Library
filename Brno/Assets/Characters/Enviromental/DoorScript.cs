using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class DoorScript : MonoBehaviour //IOpenable
{
//	[SerializeField]
//	private DoorScript firstDoors;
//	private bool opened = false;
//	public bool Opened { get { return opened; } }

//	public Transform Place
//	{
//		get
//		{
//			return place;
//		}


//	}

//	private Animator anim;
//	private bool open;
//	public event OpenHandler OnOpen;
//	public event OpenHandler OnClose;
//	[SerializeField]
//	private Transform place;
	
//	[SerializeField]
//	private bool changeScene = false;
//	[SerializeField]
//	private bool doorsToWorld = false;
//	[SerializeField]
//	private string sceneName;
//	private void Awake()
//	{
//		anim = GetComponent<Animator>();
//		Close();
//	}

//	public void Close()
//	{
//		opened = false;
//		anim.SetBool("Open", false);
//	}
//	public void GoOut()
//	{
//		opened = true;
//		if (changeScene)
//		{
//		//	SceneManager.LoadScene("World"); odkomentovat a dosadit index scény
//		}
//		anim.SetBool("Open", true);
//	}

//	public void Open(GameObject other)
//	{
//		if (doorsToWorld)
//		{
//			GoOut();
//			return;
//		}
//		opened = true;

//		if (changeScene && other.CompareTag("Player"))
//		{
//            //SceneManager.LoadScene(sceneName); odkomentovat a dosadit index scény
//        }
//        anim.SetBool("Open", true);
//	}
//	private void OnTriggerEnter(Collider other)
//	{

//		Open(other.gameObject);
//		if (!changeScene && Place != null)
//		{
//			// odebral jsem sackTrigger z hráče, musí se nahradit hráčem
//			other.GetComponent<NavMeshAgent>().Warp(firstDoors.Place.position); // vyřešit přesun kamery
//			other.GetComponent<CharacterScript>().SetTarget(null); 
//			other.GetComponent<CharacterScript>().SetDestination(Vector3.zero);//
//			foreach (CharacterScript f in other.GetComponent<CharacterScript>().Stats.Followers.Where(f => !(f is Enemy)))
//			{
//				f.SetTarget(transform);
//			}
//		}

//		Close();
//	}
//	//!!!přidat odkaz na druhé dveře!!!


//	public void Open()
//	{

//	}
//	private void OnMouseUp()
//	{
//		//if (Vector3.Distance(transform.position, PlayerScript.Instance.transform.position) < PlayerScript.Instance.InteractionRadius)
//			PlayerScript.Instance.SetTarget(transform);
//	}
}
