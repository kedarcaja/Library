using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyData : MonoBehaviour {
    public List<Price> currencyAmounts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            AddCurrency();
        }
    }

    void AddCurrency()
    {
        PlayerInventoryManager.UpdataPlayerCurrency(currencyAmounts, 1);
        Destroy(gameObject);
    }
}
