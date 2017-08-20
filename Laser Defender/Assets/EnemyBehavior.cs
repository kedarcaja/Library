using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public float projectileSpeed = 10;
    public float health = 150;
    public GameObject Projectile;
    public float shotsPerSecond = 0.5f;
    void Update() {
        float probability = Time.deltaTime * shotsPerSecond;

        if (Random.value < probability) {
        Fire();
        }
    }
    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject missile = Instantiate(Projectile, startPosition, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }                                                  
    void OnTriggerEnter2D(Collider2D collider) {
       Projectile missile = collider.gameObject.GetComponent<Projectile>();
       if (missile) {
           health -= missile.GetDamage();
           missile.Hit();
           if (health <=0)
           { 
               Destroy(gameObject); }
            }
    }

	}

