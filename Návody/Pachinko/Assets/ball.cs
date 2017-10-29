using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour {
    public Rigidbody2D SolidItem;
    float rychlost = 0.2f;
    bool fall = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

        if (Input.GetKeyDown("space"))
        {
            fall = true;
        }

        if (fall){SolidItem.bodyType = RigidbodyType2D.Dynamic;}
        else {
            SolidItem.bodyType = RigidbodyType2D.Kinematic;  
            transform.position += new Vector3(rychlost, 0f);

        /*if (transform.position.x > 10f)  {rychlost *= -1;}
   else if (transform.position.x < -10f) {; }      */


        }   
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        rychlost *= -1;
    }
}

