using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOInfo3D : MonoBehaviour {
    public Item item;
    [HideInInspector]
    public bool isEquipped = false;
    //[HideInInspector]
    public int amount = 1;
    private Q_Inventory inv;

    public void Awake()
    {
        amount = 1;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.01f);
        inv = Q_GameMaster.Instance.inventoryManager.playerInventory;
        Vector3 scale = Q_GameMaster.Instance.inventoryManager.player.transform.localScale;
        if (!isEquipped)
        {
            transform.localScale = new Vector3(scale.x * transform.localScale.x, scale.y * transform.localScale.y, scale.z * transform.localScale.z);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Interact");
        if (collision.transform.tag == "Player" && !isEquipped)
        {
            if (Q_InputManager.Instance.pickUpItem == KeyCode.None)
            {
                if (!Q_GameMaster.Instance.inventoryManager.playerInventory.CheckIsFull(item.ID))
                {
                    AddItemSelf();
                    Q_GameMaster.Instance.inventoryManager.PlayAddItemClip();
                }

                else
                {
                    Q_GameMaster.Instance.inventoryManager.SetInformation(Q_GameMaster.Instance.inventoryManager.infoManager.inventoryFull);
                    Debug.Log("Player Inventory is Full!");
                }
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        Debug.Log("Interact");
        if (collision.transform.tag == "Player" && !isEquipped && Q_InputManager.Instance.pickUpItem != KeyCode.None)
        {
            if (Input.GetKeyDown(Q_InputManager.Instance.pickUpItem))
            {
                if (!Q_GameMaster.Instance.inventoryManager.playerInventory.CheckIsFull(item.ID))
                {
                    AddItemSelf();
                    Q_GameMaster.Instance.inventoryManager.PlayAddItemClip();
                }

                else
                {
                    Q_GameMaster.Instance.inventoryManager.SetInformation(Q_GameMaster.Instance.inventoryManager.infoManager.inventoryFull);
                    Debug.Log("Player Inventory is Full!");
                }
            }
        }
    }

    public void AddItemSelf()
    {
        for (int i = 0; i < amount; i++)
        {
            inv.AddItem(item.ID);
        }
        Destroy(gameObject);
    }
}
