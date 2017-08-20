using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    public float speed = 15.0f;
    public float padding = 1f;
    float xmin, xmax;
    public GameObject Projectile;
    public float ProjectalSpeed ;
    public float firingRate = 0.7f;
    public float health = 250;

    
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
	}
	
	// Update is called once per frame
    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 1, 0);
        GameObject beam = Instantiate(Projectile, startPosition, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, ProjectalSpeed, 0);
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f,firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        if (Input.GetKey(KeyCode.LeftArrow)) { transform.position +=  Vector3.left * speed * Time.deltaTime;}
        else if (Input.GetKey(KeyCode.RightArrow)) { transform.position += Vector3.right * speed * Time.deltaTime; }

        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX,transform.position.y,transform.position.z);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

