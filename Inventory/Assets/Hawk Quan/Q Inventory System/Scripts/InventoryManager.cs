using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    [Header("Inventory Scripts")]
    public PlayerInventoryManager playerInventoryManager;
    public Q_Inventory playerInventory;
    public EquipmentManager equipmentManager;
    public Tooltip toolTip;
    public EquipmentInventory equipmentInventory;
    public SkillBar skillBar;
    public Q_InfoManager infoManager;
    public ItemSortingManager itemSortingManager;

    [Header("Data Base")]
    public ItemList itemDataBase;

    [Header("Audio")]
    public AudioSource m_AudioSource = null;
    [Space(5)]
    [SerializeField]
    private AudioClip m_AddItemClip = null;
    [SerializeField]
    private AudioClip m_WearItemClip = null;
    [SerializeField]
    private AudioClip m_ChangeMoneyClip = null;
    [SerializeField]
    private AudioClip m_OpenPanelClip = null;

    [Header("Player")]
    public GameObject player;
    public CharacterController controller;
    public DropType dropType;
    public PickUp pickUp;
    public float playerWidth;
    public float playerHeight;
    public float dropForce;

    [Header("BaseUI")]
    public GameObject Canvas;

    [Header("Inventory UI")]
    public GameObject m_InventorySlot;
    public GameObject m_InventoryItem;

    [Header("Storage UI")]
    public GameObject m_StorageSlot;
    public GameObject m_StorageItem;

    [Header("Vendor UI")]
    public GameObject m_VendorSlot;
    public GameObject m_VendorItem;

    [Header("Crafting UI")]
    public GameObject m_CraftSlot;
    public GameObject m_CraftItem;

    [Header("Equipment UI")]
    public GameObject m_EquipmentItem;

    [Header("SkillBar UI")]
    public GameObject m_SkillBarSlot;
    public GameObject m_SkillBarItem;

    [Header("Currency Drop Item")]
    public GameObject m_CurrencyDropItem;

    [Header("Information")]
    public GameObject informationPanel;
    public Text informationText;

    public void PlayAddItemClip()
    {
        m_AudioSource.PlayOneShot(m_AddItemClip);
    }

    public void PlayWearClip()
    {
        m_AudioSource.PlayOneShot(m_WearItemClip);
    }

    public void PlayMoneyChangeClip()
    {
        m_AudioSource.PlayOneShot(m_ChangeMoneyClip);
    }

    public void PlayOpenPanelClip()
    {
        m_AudioSource.PlayOneShot(m_OpenPanelClip);
    }

    public void SetInformation(string info)
    {
        informationText.text = info;
        informationPanel.gameObject.SetActive(true);
    }
}
