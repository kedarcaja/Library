using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialog : MonoBehaviour
{
    public int Part = 1;
    public Text text;
    
    public GameObject dialog;
    public GameObject Ikona;
    private int Step;
    public float time;
    public float counter = 0;
    public bool timer;

    private void Start()
    {
        Greet();
        timer = false;
    }

    void Update()
    {
        if (timer == true)
        {
            counter += Time.deltaTime;

           
        }
        if (counter >= time)
        {
            Part = Step; Greet();
            counter = 0;
            timer = false;
        }



    }
    void Greet()
    {
        switch (Part)
        {
            case 1:
                Part += 1;
                break;
            case 2:
                timer = true;
                Debug.LogError("1");
                text.text = "<color=yellow><b>Samantha: </b></color> Ahoj Hrdino, jak se dneska máš";
                Step = 2;
                time = 5f;
                break;
            case 3:
                text.text = "<color=maroon><b>Leonard: </b></color> Mám se dobře a co ty";
                break;
            case 4:
                text.text = "<color=yellow><b>Samantha: </b></color> Je mi 200 let, co myslíš";
                break;
            case 5:
                text.text = "<color=maroon><b>Leonard: </b></color> Promiň nechtěl jsme se tě dotknout";
                break;
            case 6:
                text.text = "<color=maroon><b>Leonard: </b></color> Promiň nechtěl jsme se tě dotknout";
                break;
            case 10:
                dialog.SetActive(false);
                Ikona.SetActive(false);
                Time.timeScale = 1;
                break;
            default:
               
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialog.SetActive(true);
            Time.timeScale = 0;
            
        }
    }


   
}