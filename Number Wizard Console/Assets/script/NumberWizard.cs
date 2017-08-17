using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour {
    int max, min, guess;
	// Use this for initialization
	void Start () {
        StartGame();
	}

    void StartGame() {
        max = 1000;
        min = 1;
        guess = 500;

         max = max + 1;
        print("==============================================");
        print("welcome to number wizard");
        print("Vyber si jakékoliv číslo ale, neříkej ho nahlas");

        print("nejvyší číslo které si můžete vybrat je" + max);
        print("nejmenší číslo které si můžete vybrak je " + min);

        print("Je vaše číslo menší nebo větší než ?" + guess);
        print("UP = menší, Down = vetší, Enter = rovna se  ");
        
    
    }

   
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            min = guess;
            NextGueess();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            max = guess;
            NextGueess();
        } else if (Input.GetKeyDown(KeyCode.Return)) {
            print("I won");
            StartGame();
        };
        
	}
    void NextGueess()
    {
        guess = (max + min) / 2;
        print("větší nebo menší než je teď" + guess);
        print("UP = menší, Down = vetší, Enter = rovna se  ");
    }
}
