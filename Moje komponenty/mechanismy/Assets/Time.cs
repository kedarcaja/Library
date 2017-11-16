using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Time : MonoBehaviour
{
 
    public float waitTime = 5f;

    public float timer;
     
    void Update()
    {
        if (timer <= waitTime) {
            timer += UnityEngine.Time.deltaTime;
            
        }  
    }
}
