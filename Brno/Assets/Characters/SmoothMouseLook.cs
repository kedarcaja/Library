using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMouseLook : MonoBehaviour
{
    [SerializeField]
    private float rSpeed = 5.0f;
    [SerializeField]
    private float x = 0.0f;
    [SerializeField]
    private float y = 0.0f;

    public float X { get => X;  }
    public float Y { get => Y; }

    void Update()
    {
            x += Input.GetAxis("Mouse X") * rSpeed;
            y += Input.GetAxis("Mouse Y") * rSpeed;
            transform.localRotation = Quaternion.AngleAxis(x, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(y, Vector3.left);
        if (Input.GetKeyDown(KeyCode.T))
        {
            Cursor.visible = !Cursor.visible;
        }
    }
}
