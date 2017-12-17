using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {MANA, HELTH, WEAPON}
public enum Quality { COMON,UNCOMAN,RARE,EPIC,LEGENDARY,ARTEFACT }
public class Item : MonoBehaviour {

    #region Variables
    public ItemType type;
    
    public Quality quality;

    public Sprite spriteNeutral;

    public Sprite spriteHighlighted;

    public int MaxSize;
    

    public float strength, intellect, agility, stamina;

    public string itemName;

   

    public string description;
    #endregion

    #region Unity Metod

    public void Use() {
        switch (type)
        {
            case ItemType.MANA:
                Debug.Log("I just used a mana potion");
                break;
            case ItemType.HELTH:
                Debug.Log("I just used a helth potion");
                break;
            case ItemType.WEAPON:
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
                color = "teal";
              
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

    public void SetStats(Item item)
    {
        type = item.type;

        quality = item.quality;

        spriteNeutral = item.spriteNeutral;

        spriteHighlighted = item.spriteHighlighted;

        this.MaxSize = item.MaxSize;

        this.strength = item.strength;

        this.intellect = item.intellect;

        this.agility = item.agility;

        this.stamina = item.stamina;

        this.itemName = item.itemName;

        this.description = item.description;

        switch (type)
        {
            case ItemType.MANA:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
            case ItemType.HELTH:
                GetComponent<Renderer>().material.color = Color.red;
                break;
            case ItemType.WEAPON:
                GetComponent<Renderer>().material.color = Color.grey;
                break;


        }
    }

    #endregion
}

