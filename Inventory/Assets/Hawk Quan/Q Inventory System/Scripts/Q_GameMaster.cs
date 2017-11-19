using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_GameMaster : MonoBehaviour
{
    public static Q_GameMaster Instance;

    [Header("Inventory")]
    public InventoryManager inventoryManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

#region 这里是用户用来get和change属性值用的
    public float GetAttributeCurrentValue(string attributeName)
    {
        return Instance.inventoryManager.playerInventoryManager.FindPlayerAttributeCurrentValueByName(attributeName);
    }

    public float GetAttributeMaxValue(string attributeName)
    {
        return Instance.inventoryManager.playerInventoryManager.FindPlayerAttributeMaxValueByName(attributeName);
    }

    public float GetAttributeMinValue(string attributeName)
    {
        return Instance.inventoryManager.playerInventoryManager.FindPlayerAttributeMinValueByName(attributeName);
    }

    public void ChangeAttributeValue(string attributeName,float amount,Effect effect)
    {
        Instance.inventoryManager.playerInventoryManager.ChangePlayerAttributeByName(attributeName, amount, effect);
    }
#endregion
}
