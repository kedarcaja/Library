using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    #region Variables
   public GameObject childObj;   // Set this to the child you want to give a home
   public GameObject parent;  // Set this to the parent


    #endregion

    #region Unity Metod

    void Start () {
        Draw();

    }
	
	void Update () {
        
          
        
	}

    public void Draw()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject newSlotBG = (GameObject)Instantiate(childObj);
            // newSlotBG.transform.parent = parent.transform;
            newSlotBG.transform.SetParent(parent.transform);
            RectTransform slotRectBG = newSlotBG.GetComponent<RectTransform>();
            newSlotBG.name = "Slot";


        }
    }
    #endregion
}
