using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloppyController : MonoBehaviour
{
    public Text scoreText;
    public Canvas looseScreen;
    public Transform wing;
    public Rigidbody2D rigidbody;
    public float flySpeed = 1f;
    public float flyDuration = 0.25f;
    public float fallSpeed = 1f;
    public bool dead;

    bool fly;
    float flyTime;
    float elapsedTime;

    int score;

	void Update ()
    {
        if (!dead)
        {            
            rigidbody.gravityScale = 0f;
            elapsedTime += Time.deltaTime;

            if (fly)
            {
                // Animace Kridla
                wing.localScale = new Vector3(1.4f, -1.4f, 1.4f);
                transform.position += Vector3.up * flySpeed * Time.deltaTime;

                if (flyTime < flyDuration)
                {
                    flyTime += Time.deltaTime;
                }
                else
                {
                    flyTime = 0f;
                    fly = false;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    fly = true;
                }

                // Animace Kridla
                wing.localScale = new Vector3(1.4f, 1.4f, 1.4f);

                // Gravitace
                transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            }
        }  else
        {
            rigidbody.gravityScale = 1f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        dead = true;
        looseScreen.gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        score++;
        scoreText.text = score.ToString();
    }
}
