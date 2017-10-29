using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicUp : MonoBehaviour
{
    

[Header("Pick up Systém")]
    [Tooltip("False = samozběr \n" +
             "True = Pick Up               ")]
    public bool PickUp;
    public int CoinValue;
    int ItemValue;
   
    private bool Pick;
    public Global Global;
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Pick == true)
        {
            Pick = false;
            DestroyGameObject();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PickUp == false)
            {
                Global.Gold += 1;
                DestroyGameObject();
            }
            else { Pick = true; }

        }
    }


    void DestroyGameObject()
    {
        
        Destroy(gameObject);
    }




}
