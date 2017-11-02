using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialog : MonoBehaviour
{
   int Part = 1;
    public Text text;
    public GameObject DialBtn1, DialBtn2, DialBtn3,DialBtn4;
   
    public Text DialogováVolba1, DialogováVolba2, DialogováVolba3, DialogováVolba4;

   



    public GameObject dialog;
    public GameObject Ikona;
    private int Step;
    public float time;     //Pozor Time začíná od 0 takže value času bude vždy o 1 menší než Part
    public int counter = 0;
   
    private Animator animator;
    // Detects keys pressed and prints their keycode
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        Dialog01();

        DialBtn1.SetActive(false);
        DialBtn2.SetActive(false);
        DialBtn3.SetActive(false);
        DialBtn4.SetActive(false);


    }

    void Update()
    {
       

        if (time >= counter) { Part = Step; Dialog01(); }

    }
    void Dialog01()
    {
        if (Part == 1)
        {
            text.text = "<color=brown><b>Leonard: </b></color> Ahoj Samatho, jak se dneska máš ??";
            counter = 1;
            Step = 2;
        }

        if (Part == 2)
        {
            text.text = "<color=yellow><b>Samantha: </b></color> Je mi 200 let, co myslíš ??";
            counter = 2;
            Step = 3;
        }
        if (Part == 3)
        {
            text.text = "<color=brown><b>Leonard: </b></color> Promiň, nechtěl jsem se tě dotknout";
            counter = 3;
            Step = 4;
        }

        if (Part == 4)
        {
            animator.SetBool("Play", false);
            time = 3;
            DialBtn1.SetActive(true);
            DialBtn2.SetActive(true);
            DialBtn3.SetActive(true);
            DialBtn4.SetActive(true);

            DialogováVolba1.text = "1. Volba1";
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                animator.SetInteger("Press", 1);
            }   
       

            

            DialogováVolba1.text = "2. Volba2";

            DialogováVolba1.text = "3. Volba3";

            DialogováVolba1.text = "4. Volba4";
        }
        if (Part == 4)
        {
            text.text = "<color=yellow><b>Samantha: </b></color> super jak to jde ";
            counter = 4;
            Step = 5;
        }
        if (Part == 5)
        {
            text.text = "<color=brown><b>Leonard: </b></color> Promiň, nechtěl jsem se tě dotknout";
            counter = 5;
            Step = 6;
        }
        if (Part == 6)
        {
            text.text = "<color=brown><b>Leonard: </b></color> Promiň, nechtěl jsem se tě dotknout";
            counter = 6;
            Step = 7;
        }
        if (Part == 7)
        {
            text.text = "<color=brown><b>Leonard: </b></color> čs jdu prič";
            counter = 5;
            Step = 4;
        }

        if (Part == 8)
        {
            text.text = "<color=brown><b>Leonard: </b></color> čs jdu prič";
            counter = 3;
            Step = 4;
        }
        // dialog.SetActive(false);
        // Ikona.SetActive(false);

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialog.SetActive(true);
            // Time.timeScale = 0;
            Part = 1;
            animator.SetBool("Play", true);

        }
    }

   

}
