using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMover : MonoBehaviour
{
    public float speed = 1f;
    public float startPosition = 0;
    public float endPosition = 1;

    // Update is called once per frame
    void Update ()
    {
        if (transform.position.x < endPosition)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        } else
        {
            transform.position = new Vector3(startPosition, 0f, 0f);
        }
	}
}
