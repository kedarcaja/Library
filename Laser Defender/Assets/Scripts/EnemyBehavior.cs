using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public float            projectileSpeed =           10;
    public float            health =                    150;
    public GameObject       Projectile;
    public float            shotsPerSecond =            0.5f;
    public int              scoreValue =                150;
    public AudioClip        fireSound;
    public AudioClip        deathSound;

    private ScoreKeeper scoreKeeper;
    void Start() {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
    void Update() {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability) {
        Fire();
        }
    }
    void Fire()
    {
        GameObject missile = Instantiate(Projectile, transform.position, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.8f);
    }                                                  
    void OnTriggerEnter2D(Collider2D collider) {
       Projectile missile = collider.gameObject.GetComponent<Projectile>();
       if (missile) {
           health -= missile.GetDamage();
           missile.Hit();
           if (health <=0)
           {
               Die();
           }
           }
           }
    void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.8f);
        scoreKeeper.Score(scoreValue);
    }
	}

