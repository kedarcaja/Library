using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    #region Variables
    public float speed = 5f;
    public Inventory inventory;
    private Inventory Chest;
   #endregion
     
   #region Unity Metod
   
	void Start () {
		
	}
	
	void Update ()
    {
       
        transform.Translate(Input.GetAxis("Horizontal")*speed * UnityEngine.Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * UnityEngine.Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.Open();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Chest!=null)
            {
                Chest.Open();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Item")
        {
            inventory.AddItem(other.GetComponent<Item>()); 
        }
        if (other.tag == "Chest")
        {
            Chest = other.GetComponent<ChestScript>().chestInventory;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Chest")
        {
            if (Chest.IsOpen)
            {
                Chest.Open();
            }
            Chest = null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            inventory.AddItem(collision.gameObject.GetComponent<Item>());
            Destroy(collision.gameObject);
        }
    }
    #endregion
}
