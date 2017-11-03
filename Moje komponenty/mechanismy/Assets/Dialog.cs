using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
   int Part = 1;
    public Text text;
    public GameObject Volba;
    public Text DialogováVolba1, DialogováVolba2, DialogováVolba3, DialogováVolba4;
    public GameObject dialog, Ikona;
    private int Step, counter = 0, end = 1;
    float time;     
    bool choice;
    private Animator animator;

    private void Start(){
        animator = GetComponent<Animator>();
        Dialog01();
    }

    void Update()
    {
        if (time >= counter) { Part = Step; Dialog01(); }
        if (choice == true){
            Volba.SetActive(true);
            time = 0;
            DialogováVolba1.text = "1. Volba1";
            if (Input.GetKeyDown(KeyCode.Alpha1)){
                animator.SetBool("1", true);
                Part = 5; Dialog01();
                choice = false;
                Volba.SetActive(false);
            }

            DialogováVolba2.text = "2. Volba2";
            if (Input.GetKeyDown(KeyCode.Alpha2)){
                animator.SetBool("2", true);
                Part = 7; Dialog01();
                choice = false;
                Volba.SetActive(false);
            }

            DialogováVolba3.text = "3. Volba3";
            if (Input.GetKeyDown(KeyCode.Alpha3)){
                animator.SetBool("3", true);
                Part = 9; Dialog01();
                choice = false;
                Volba.SetActive(false);
            }

            DialogováVolba4.text = "4. Volba4";
            if (Input.GetKeyDown(KeyCode.Alpha4)){
                animator.SetBool("4", true);
                Part = 10; Dialog01();
                choice = false;
                Volba.SetActive(false);
            }
        }
    }
    void Dialog01(){
        if (Part == 1){
            text.text = "<color=brown><b>Leonard: </b></color> Ahoj Samatho, jak se dneska máš ??";
            counter = 1;
            Step = 2;
        }
        if (Part == 2){
            text.text = "<color=yellow><b>Samantha: </b></color> Je mi 200 let, co myslíš ??";
            counter = 2;
            Step = 3;
        }
        if (Part == 3) {
            text.text = "<color=brown><b>Leonard: </b></color> Promiň, nechtěl jsem se tě dotknout";
            choice = true;
            time = 0;
            animator.SetBool("choice", true);
        }
        if (Part == 5){
            animator.SetInteger("Press", 0);
            text.text = "<color=yellow><b>Samantha: </b></color> sjsem na 5 ";
            counter = 1;
            Step = 6;
        }
        if (Part == 6) {
            text.text = "<color=brown><b>Leonard: </b></color> jsem na 6";
            counter = 2;
            Step = 10;
            end = 3;
        }
        if (Part == 7){
            animator.SetInteger("Press", 0);
            text.text = "<color=brown><b>Leonard: </b></color> jsem na 7";
            counter = 1;
            Step = 8;
        }
        if (Part == 8){
            text.text = "<color=brown><b>Leonard: </b></color> jsem na 8";
            counter = 2;
            Step = 10;
            end = 3;
        }
        if (Part == 9){
            animator.SetInteger("Press", 0);
            text.text = "<color=brown><b>Leonard: </b></color> jsem na 9";
            counter = 1;
            Step = 10;
            end = 2;
        }
        if (Part == 10){
            text.text = "<color=brown><b>Leonard: </b></color> čs jdu prič";
            counter = end;
            Step = 11; 
        }
        if (Part == 11){
            time = 0;
            Volba.SetActive(false);
            dialog.SetActive(false);
            Ikona.SetActive(false); 
        }
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
