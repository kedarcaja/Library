using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Q Inventory/Add Item")]
[RequireComponent(typeof(Q_Inventory))]
public class AddItem : MonoBehaviour {
    public ItemToAdd[] itemsToAdd;
}


//[System.Serializable]
//public class ItemToAdd
//{
//    public GameObjectData itemsToAdd;
//    public int amount;
//}

