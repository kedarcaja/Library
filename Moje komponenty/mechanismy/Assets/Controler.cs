using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;



public class Controler : MonoBehaviour
{

    public Global Global;
    public Text GoldValue;
    public GameObject Quest;
    bool QuestBookShow;
    void Start()
    {
        //myChangeMeScript.AmIChanged = 36;
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestBookShow == true) { Quest.SetActive(true); }
        else { Quest.SetActive(false); }
        GoldValue.text = Global.Gold.ToString();

        if (Input.GetKeyDown(KeyCode.Q)) {
           
            if (QuestBookShow == false) { QuestBookShow = true; }
            else { QuestBookShow = false; }
        }

    }

      
    
}
