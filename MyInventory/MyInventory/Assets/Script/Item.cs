using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {MANA, HELTH }
public enum Quality { COMON,UNCOMAN,RARE,EPIC,LEGENDARY,ARTEFACT }
public class Item : MonoBehaviour {

    #region Variables
    public ItemType type;
    
    public Quality quality;

    public Sprite spriteNeutral;

    public Sprite spriteHighlighted;

    public int MaxSize;
    public int qualita;
    private static int Qualita;

    public float strength, intellect, agility, stamina;

    public string itemName;

   

    public string description;
    #endregion

    #region Unity Metod
    
    void Start () {
        

    }
	
	void Update () {

       
    }

    public void Qualitint() {
       
        if (quality == Quality.COMON)
        {
            qualita = 0;
        }
        if (quality == Quality.UNCOMAN)
        {
            qualita = 1;
        }
       
    }

    public void Use() {
        switch (type)
        {
            case ItemType.MANA:
                Debug.Log("I just used a mana potion");
                break;
            case ItemType.HELTH:
                Debug.Log("I just used a helth potion");
                break;
            default:
                break;
        }
    }

    public string GetTooltip()
    {
        string stats = string.Empty;
        string color = string.Empty;
        string newLine = string.Empty;

        if (description != string.Empty)
        {
            newLine = "\n";
        }

        switch (quality)
        {
            case Quality.COMON:
                color = "white";
               
                break;
            case Quality.UNCOMAN:
                color = "lime";
               
                break;
            case Quality.RARE:
                color = "navy";
                break;
            case Quality.EPIC:
                color = "magenta";
                break;
            case Quality.LEGENDARY:
                color = "orange";
                break;
            case Quality.ARTEFACT:
                color = "red";
                break;
            default:
                break;
        }

        if (strength >0)
        {
            stats += "\n+" + strength.ToString() + " Strength";
        }
        if (intellect > 0)
        {
            stats += "\n+" + intellect.ToString() + " Intellect";
        }
        if (agility > 0)
        {
            stats += "\n+" + agility.ToString() + " Agility";
        }
        if (stamina > 0)
        {
            stats += "\n+" + stamina.ToString() + " Stamina";
        }
        return string.Format("<color=" + color + "><size=24>{0}</size></color><size=22><i><color=lime>" + newLine + "{1}</color></i>{2}</size>", itemName, description, stats); 
    }
  #endregion  
}

