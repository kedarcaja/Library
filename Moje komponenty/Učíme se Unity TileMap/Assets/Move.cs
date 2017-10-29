using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            transform.position = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        }
    }
        
}
