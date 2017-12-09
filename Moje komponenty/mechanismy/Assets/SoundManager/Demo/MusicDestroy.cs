using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicDestroy : MonoBehaviour {

    #region Variables
    static MusicDestroy instance = null;
    #endregion

    #region Unity Metod

    void Start () {
        if (instance != null)
        {
            Destroy(gameObject);
            print("Duplicate");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
	
	void Update () {
		
	}
  #endregion  
}
