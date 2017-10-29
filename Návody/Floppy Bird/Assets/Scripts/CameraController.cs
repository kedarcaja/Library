using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform floppyBird;
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(floppyBird.position.x, 0f, -10f);
	}
}
