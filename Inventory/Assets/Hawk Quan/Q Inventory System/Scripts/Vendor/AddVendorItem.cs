using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Q Inventory/Add Vendor Item")]
[RequireComponent(typeof(Vendor))]
public class AddVendorItem : MonoBehaviour {
    public ItemToSell[] itemsToSell;
}

