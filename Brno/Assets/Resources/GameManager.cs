using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	void Start()
    {
        
    }


    void Update()
    {
		if (Input.GetKeyDown(KeyCode.F12))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
    }
}
