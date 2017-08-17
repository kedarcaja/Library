using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour {
    int max, min, guess;
    int maxGuessesAllowed = 10;
    public Text text;
    
    
	void Start () {
        StartGame();
	}
  
   
    void StartGame() {
        max = 1001;
        min = 1;
        NextGueess();
        max = max + 1;
    }
    public void GuessHigher(){
        min = guess;
        NextGueess();
    }
    public void GuessLower(){
        max = guess;
        NextGueess();
    }
    void NextGueess(){
        guess = Random.Range(min,max+1);
        text.text = guess.ToString();
        maxGuessesAllowed = maxGuessesAllowed - 1;
        if (maxGuessesAllowed <= 0){
            Application.LoadLevel("Win");
        }
    }
    void Update () {
if (Input.GetKeyDown(KeyCode.UpArrow)) {

    GuessHigher();
}
else if (Input.GetKeyDown(KeyCode.DownArrow))
{
    GuessLower();
}

}
}