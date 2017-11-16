using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DialogControlScript : MonoBehaviour
{
    //public dropdown
    public enum DialogList
    { None, D_MS01_01, D_MS01_02, D_MS02_01 }
    public DialogList SelectedDialoge = DialogList.None;

    public GameObject dialog,Volba, Ikona;
    private bool ActiveTimer, choice;
    public Text text, DialogováVolba1, DialogováVolba2, DialogováVolba3, DialogováVolba4;
    private int Step, DialogValue, C1, C2, C3, C4, waitTime;
    public float timer;
    int Part;

    
   
    private void Start()
    {
        Volba.SetActive(false);

             if (SelectedDialoge == DialogList.D_MS01_01) { DialogValue = 1; }
        else if (SelectedDialoge == DialogList.D_MS01_02) { DialogValue = 2; }
        else if (SelectedDialoge == DialogList.D_MS02_01) { DialogValue = 3; }
        else if (SelectedDialoge == DialogList.None) { Debug.LogError("<color=Red><b>ERROR: </b> Zapoměl jsi vybrat o jaký dialog se jedná </color>"); }

        
    }
    private void Update()
    {
        if (timer >= waitTime)
            {
                Part = Step; Dialog();

            }
        else { timer += UnityEngine.Time.deltaTime; }

        if (choice == true)
        {
            Volba.SetActive(true);
            timer = 0;
            
                 if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Part = C1; Dialog();
                choice = false;
                Volba.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Part = C2; Dialog();
                choice = false;
                Volba.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Part = C3; Dialog();
                choice = false;
                Volba.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Part = C4; Dialog();
                choice = false;
                Volba.SetActive(false);
            }
        }

    }

    void Dialog()
    {
        if (DialogValue == 1)
        {

             if (Part == 1)
        {
            timer = 0;
            text.text = "<color=brown><b>Leonard: </b></color> Ahoj Samatho, jak se dneska máš ??";
            waitTime = 1;
            Step = 2;
            }
        else if (Part == 2)
        {
            timer = 0;
            text.text = "<color=yellow><b>Samantha: </b></color> Je mi 200 let, co myslíš ??";
            waitTime = 1;
            Step = 3;
        }
        else if (Part == 3)
        {
            timer = 0;
            text.text = "<color=brown><b>Leonard: </b></color> Promiň, nechtěl jsem se tě dotknout";
            choice = true;
                DialogováVolba1.text = "1. Volba 1";
                DialogováVolba2.text = "2. Volba 2";
                DialogováVolba3.text = "3. Volba 3";
                DialogováVolba4.text = "4. Volba 4";
                C1 = 4; C2 = 6; C3 = 8; C4 = 9;

            }
        else if (Part == 4)
        {
            text.text = "<color=yellow><b>Samantha: </b></color> sjsem na 4 ";
            waitTime = 1;
            Step = 5;
        }
        else if (Part == 5)
        {
            text.text = "<color=brown><b>Leonard: </b></color> jsem na 5";
            waitTime = 2;
            Step = 9;
        }
        else if (Part == 6)
        {
            text.text = "<color=brown><b>Leonard: </b></color> jsem na 6";
            waitTime = 1;
            Step = 7;
        }
        else if (Part == 7)
        {
            text.text = "<color=brown><b>Leonard: </b></color> jsem na 7";
            waitTime = 2;
            Step = 9;
        }
        else if (Part == 8)
        {
            text.text = "<color=brown><b>Leonard: </b></color> jsem na 8";
            waitTime = 1;
            Step = 9;
        }
        else if (Part == 9)
        {
            text.text = "<color=brown><b>Leonard: </b></color> čs jdu prič";
            waitTime = 3;
            Step = 10;
        }
        else if (Part == 10)
        {
                timer = 0;
                Volba.SetActive(false);
                dialog.SetActive(false);
                Ikona.SetActive(false);
            }
    }
        if (DialogValue == 2)
        {
            if (Part == 1)
            {
                text.text = "<color=brown><b>Leonard: </b></color> JUP9999 dneska máš ??";
                waitTime = 8;
                Step = 2;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialog.SetActive(true);
            Part = 1; Dialog();
            ActiveTimer = true;
        }
    }
}
