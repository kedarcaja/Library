using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreKeeper : MonoBehaviour {

    public static int           score;
    private Text                myText;

    void Start() {
       myText =                 GetComponent<Text>();
       Reset();
    }
    public void Score(int points) {
        Debug.Log("Scored points");
        score += points;
        myText.text = score.ToString();
    }
    public static void Reset() {
        score = 0;
    }
}
