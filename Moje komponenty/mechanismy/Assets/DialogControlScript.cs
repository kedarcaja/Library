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
    [Header("Výběr dialogu")]
    [Tooltip("Z menu mužeme vybrat o jaký dialog se jedná")]
    public DialogList VybranýDialog = DialogList.None;
    [Header("Objekty")]
    public GameObject dialog;
    public GameObject Volba, Ikona;
    [Header("Volba")]
    public Text DialogText;
    public Image[] VolbaButton;
    public Text[] VolbaText;
    private bool choice;
    private int DialogValue, C1, C2, C3, C4, Part, Step, choiceNumber;
    private float timer, waitTime;
    public VolbaControler number;
    

    
   
    private void Start()
    {
        Volba.SetActive(false);

             if (VybranýDialog == DialogList.D_MS01_01) { DialogValue = 1; }
        else if (VybranýDialog == DialogList.D_MS01_02) { DialogValue = 2; }
        else if (VybranýDialog == DialogList.D_MS02_01) { DialogValue = 3; }
        else if (VybranýDialog == DialogList.None) { Debug.LogError("<color=Red><b>ERROR: </b> Zapoměl jsi vybrat o jaký dialog se jedná </color>"); }

        
    }
    private void Update()
    {
        choiceNumber = number.ChoiceNumber;
        if (timer >= waitTime)
            {
                Part = Step; Dialog();

            }
        else { timer += UnityEngine.Time.deltaTime; }

        if (choice == true)
        {
            Volba.SetActive(true);
            timer = 0;
            
                 if (Input.GetKeyDown(KeyCode.Alpha1) || choiceNumber == 1)
            {
                Part = C1; Dialog();
                choice = false;
                Volba.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || choiceNumber == 2)
            {
                Part = C2; Dialog();
                choice = false;
                Volba.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || choiceNumber == 3)
            {
                Part = C3; Dialog();
                choice = false;
                Volba.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || choiceNumber == 4)
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
            DialogText.text = "<color=brown><b>Leonard: </b></color> Ahoj Samatho, jak se dneska máš ??";
            waitTime = 1f;
            Step = 2;
            }
        else if (Part == 2)
        {
            timer = 0;
            DialogText.text = "<color=yellow><b>Samantha: </b></color> Je mi 200 let, co myslíš ??";
            waitTime = 1f;
            Step = 3;
        }
        else if (Part == 3)
        {
            timer = 0;
            DialogText.text = "<color=brown><b>Leonard: </b></color> Promiň, nechtěl jsem se tě dotknout";
            choice = true;
                VolbaText[0].text = "1. Volba 1";
                VolbaText[1].text = "2. Volba 2";
                VolbaText[2].text = "3. Volba 3";
                VolbaText[3].text = "4. Volba 4";
                C1 = 4; C2 = 6; C3 = 8; C4 = 9;

            }
        else if (Part == 4)
        {
            DialogText.text = "<color=yellow><b>Samantha: </b></color> sjsem na 4 ";
            waitTime = 1f;
            Step = 5;
        }
        else if (Part == 5)
        {
            DialogText.text = "<color=brown><b>Leonard: </b></color> jsem na 5";
            waitTime = 2f;
            Step = 9;
        }
        else if (Part == 6)
        {
            DialogText.text = "<color=brown><b>Leonard: </b></color> jsem na 6";
            waitTime = 1f;
            Step = 7;
        }
        else if (Part == 7)
        {
            DialogText.text = "<color=brown><b>Leonard: </b></color> jsem na 7";
            waitTime = 2f;
            Step = 9;
        }
        else if (Part == 8)
        {
            DialogText.text = "<color=brown><b>Leonard: </b></color> jsem na 8";
            waitTime = 1f;
            Step = 9;
        }
        else if (Part == 9)
        {
            DialogText.text = "<color=brown><b>Leonard: </b></color> čs jdu prič";
            waitTime = 3f;
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
                DialogText.text = "<color=brown><b>Leonard: </b></color> JUP9999 dneska máš ??";
                waitTime = 8f;
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
            
        }
    }
}
