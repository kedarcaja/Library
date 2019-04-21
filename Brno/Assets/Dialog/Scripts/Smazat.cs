using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smazat : MonoBehaviour
{
    _Timer t;
    void Start()
    {
       t = new _Timer(1, 1, this);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            t.Start();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            t.Pause();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            t.Stop();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            t.Restore();
        }
        Debug.Log(t.ElapsedTimeI);
    }
}
