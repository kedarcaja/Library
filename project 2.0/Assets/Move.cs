using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{
    public GameObject character;
    private float speed = 10f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
        {

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
             }
           else if (Input.GetKey(KeyCode.W))
            {
                transform.position += Vector3.forward * speed * Time.deltaTime;
                transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
            }
           else if (Input.GetKey(KeyCode.S))
            {
                transform.position += Vector3.back * speed * Time.deltaTime;
                transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
            }

        if (Input.GetKey(KeyCode.Space)) // And if the player press Space
        {
            rb.velocity = new Vector3(0f, 5f, 0f); // The sphere will jump           
        }
    }
    }
