using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public Text text;
    private enum States { cell, sheets_0, sheets_1, lock_0, lock_1,guardian, mirror, cell_mirror, night, freedom };
    private States MyState;
    void Start()
    {
        MyState = States.cell;
    }
    void Update()
    {
        print(MyState);
        if      (MyState == States.cell)            {cell();}
        else if (MyState == States.sheets_0)        {sheets_0();}
        else if (MyState == States.sheets_1)        {sheets_1();}
        else if (MyState == States.lock_0)          {lock_0();}
        else if (MyState == States.lock_1)          {lock_1();}
        else if (MyState == States.cell_mirror)     {cell_mirror();}
        else if (MyState == States.mirror)          {mirror();}
        else if (MyState == States.guardian)        {Guardian();}
        else if (MyState == States.night)           {Night();}
        else if (MyState == States.freedom)         {freedom();}
    }

    #region místnosti

    void cell() {
            text.text = "Momentálně se nacházíš ve své cele"+
                        "Před sebou vidíš svoji velice nepohodlnou postel, zrcadlo na zdi a dveře" +
                        "které jsou uzamčeno zvenčí.\n\n" +       
                        "S - Jdu spát\n"+
                        "G - zkusím vyprovokovat stráže \n"+
                        "L - omrknu zámek";

        if      (Input.GetKeyDown(KeyCode.S))       {MyState = States.sheets_0;}
        else if (Input.GetKeyDown(KeyCode.G))       {MyState = States.guardian;}
        else if (Input.GetKeyDown(KeyCode.L))       { MyState = States.lock_0;}
    }
    void cell_mirror() {
            text.text = "Super jsem zase v té smradlevé díře.\n\n" +
                        "S - Jdu spát\n" +
                        "T - zkusím zda-li můj plán s otiskem bude fungovat";
        if      (Input.GetKeyDown(KeyCode.T))       {MyState = States.lock_1;}
        else if (Input.GetKeyDown(KeyCode.S))       {MyState = States.cell;}
    }
    void sheets_0()
    {
            text.text = "Nemužu uveřit tomu že musím další den svého mizerného životy " +
                        "spát na této podělané posteli\n " +
                        "Myslím, že\n " +
                        "R - bych měl popřemýšlet jak se odsud dostanu";
        if (Input.GetKeyDown(KeyCode.R))            {MyState = States.cell;}
    }
    void sheets_1() {
            text.text = "Holding a mirror in your hand doesn't make the sheets look " +
                        "any better.\n\n" +
                        "Press R to Return to roaming your cell" ;
        if (Input.GetKeyDown(KeyCode.R))            {MyState = States.cell_mirror;}
    }
    void lock_0()
    {
            text.text = "Sakra je to jeden z těch zámku k jehoš otevření potřebuju otis" + 
                        "prstu useknout ruku strátnýmu mi asi moc nepomůže to bych si mohl" +
                        "rovnou pozvat televizi a říct jím svůj plán\n\n " +
                        "R - musím svůj útěk lépe promyslet";
        if (Input.GetKeyDown(KeyCode.R))            {MyState = States.cell;}
    }
    void lock_1()
    {
            text.text = "Vezmu zrcadlo a jdu to otestovat\n\n " +
                        "H - Udělám to hned\n" +
                        "W - počkám až se setmí";
        if      (Input.GetKeyDown(KeyCode.H))       {MyState = States.freedom;}
        else if (Input.GetKeyDown(KeyCode.W))       {MyState = States.night;}
    }
    void mirror()
    {
               text.text = "Když jsme hlídačovi podrazil nohy vrhli se na mě další a odtáhli mě na samotku" +
                           "já jsme si to tam však užíval jelikož jsme věděl že mi stážný při pádu nechtěně nechal " +
                           "na zrcadle malý dáreček v podobě otisku své dlaně\n\n" +
                           "P - počkam až se budu moct vrátit na svoji celu";
        if (Input.GetKeyDown(KeyCode.P))            {MyState = States.cell_mirror;}
        
    }
    void Guardian()
    {
                text.text = "Strážný přbíhá do mé cely" +
                            "Před sebou vidíš svoji velice nepohodlnou postel, plastové zrcadlo na zdi a dveře" +
                            "které jsou uzamčeno zvenčí.\n\n" +
                            "M - stčit do hlídače\n" +
                            "R - nechat rvačky a zvovu promyšlet svůj plán ";
        if      (Input.GetKeyDown(KeyCode.M))       {MyState = States.mirror;}
        else if (Input.GetKeyDown(KeyCode.R))       {MyState = States.cell;}
    }
    void Night()
    {
                text.text = "Sakra já zapoměl že se zámky na noc vypínají a při použití se spustí alarm \n\n" +
                            "Press P to Play again";
        if (Input.GetKeyDown(KeyCode.P))            {MyState = States.cell;}
    }
    void freedom()
    {
                text.text = "Jsem volný konečně!\n\n" +
                            "P - hrát znovu";
        if (Input.GetKeyDown(KeyCode.P))            {MyState = States.cell;}
    }
    #endregion 

    public void OnClick()
    {
        text.text = "haf";
    }
}