using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public GameObject obj;
    public float speed = 50.0f;
    private Vector3 worldCenter = new Vector3(0, 0, 0);  //Rotation pivot point
    void Update()
    {
         if (Input.GetKey(KeyCode.A))
        {
            //transform.RotateAround(worldCenter, Vector3.up, -Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            transform.RotateAround(obj.transform.position, Vector3.down, speed * Time.deltaTime);
        }
    }
}
