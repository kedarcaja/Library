using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemToSell
{
    public Item itemToSell;
    public bool moveAfterPurchase = false;
    public bool useDefaultPrice = true;
    public List<Price> prices;
}
