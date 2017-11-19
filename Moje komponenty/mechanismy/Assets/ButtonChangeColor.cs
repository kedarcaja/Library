using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeColor : MonoBehaviour {

    public Image Image;
    public Color StartColor;
    public Color Enter;
    public int ChoiceNumbers;
    public VolbaControler Global;
    
    private void Start()
    {
      // Image.color = Color.white;
    }

    public void OnMouseEnter()
    {
        
        Image.color = Enter;
       
        
    }

    public void OnMouseExit()
    {
        Image.color = StartColor;
    }

    public void OnClick() {

        
        Global.ChoiceNumber = ChoiceNumbers;

    }
}
