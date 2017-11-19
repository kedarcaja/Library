using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyInfo : MonoBehaviour {
    public float health = 100f;
    [SerializeField]
    private GameObject deathParticle = null;
    private DropItem dropItem;

    private void Start()
    {
        dropItem = GetComponent<DropItem>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            Death();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Weapon")
        {
            Attacked();
        }
    }

    public void Attacked()
    {
        health -= Q_GameMaster.Instance.inventoryManager.playerInventoryManager.FindPlayerAttributeMaxValueByName("Damage");
        Debug.Log("Enemy Health: " + health);
    }

    void Death()
    {
        dropItem.DropItems();
        GameObject _deathParticle = Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(_deathParticle, 2f);
    }
}
