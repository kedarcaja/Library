using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HightScore : MonoBehaviour {
    private Text        myText;
     void Start(){
       myText =         GetComponent<Text>();
       myText.text =    "Moje scoje je:" + ScoreKeeper.score.ToString();
       ScoreKeeper.Reset();
    }
}
