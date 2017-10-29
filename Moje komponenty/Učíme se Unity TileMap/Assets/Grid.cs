using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Grid : MonoBehaviour {


    public float grid = 0.5f;
    float x = 0f, y = 0f;


    void Update()
    {
        if (grid > 0f)
        {
            float reciprocalgrid = 1f / grid;

            x = Mathf.Round(transform.position.x * reciprocalgrid) / reciprocalgrid;
            y = Mathf.Round(transform.position.y * reciprocalgrid) / reciprocalgrid;

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
