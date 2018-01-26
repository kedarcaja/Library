using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {

    #region Variables
    public int Part;
    public Text text;
   #endregion
     
   #region Unity Metod
   
	void Start () {
		
	}
	
	void Update () {
        if (Part==0)
        {
            text.text = "ahoj tohle je první čast";
        }
        if (Part == 1)
        {
            text.text = "ahoj tohle je druhá čast";
        }
    }
  #endregion  
}
