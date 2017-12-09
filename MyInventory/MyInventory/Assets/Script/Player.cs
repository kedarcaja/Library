using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    #region Variables
    public float speed = 5f;
    public Inventory inventory;
   #endregion
     
   #region Unity Metod
   
	void Start () {
		
	}
	
	void Update () {
       
        transform.Translate(Input.GetAxis("Horizontal")*speed * UnityEngine.Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * UnityEngine.Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Item")
        {
            inventory.AddItem(other.GetComponent<Item>());
            
        }
    }
    #endregion
}
