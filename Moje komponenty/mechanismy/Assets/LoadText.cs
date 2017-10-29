using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadText : MonoBehaviour {
  
    public TextAsset TextFile;
    public Text CreditText;
	
	void Start () {
        
	}

    private void OnGUI()
    {
        CreditText.text = TextFile.text;
      
    }
}
