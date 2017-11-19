using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;



public class QuestCotrol : MonoBehaviour {

    public GameObject Quest;
    bool QuestShow;
   
   

    public void SetActive()
    {
        if (QuestShow == false) { Quest.SetActive(true); QuestShow = true; }
        else { Quest.SetActive(false); QuestShow = false; }
    }
}
