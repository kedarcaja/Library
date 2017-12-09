using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

    #region Variables
    public enum Quality {COMON,RARE }
    public Quality quality;
    public Image image;
    public Sprite[] sprites;
    #endregion

    #region Unity Metod

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void clik() {
        Event();
    }
    public void Event()
    {
        if (quality==Quality.COMON)
        {
            image.sprite = sprites[0];
        }
        if (quality == Quality.RARE)
        {
            image.sprite = sprites[1];
        }
    }
    #endregion
}
