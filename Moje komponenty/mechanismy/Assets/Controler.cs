using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controler : MonoBehaviour
{

    public Global Global;
    public Text GoldValue;

    void Start()
    {
        //myChangeMeScript.AmIChanged = 36;
    }

    // Update is called once per frame
    void Update()
    {
        GoldValue.text = Global.Gold.ToString();
    }
}