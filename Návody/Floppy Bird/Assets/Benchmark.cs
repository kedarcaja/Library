using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Benchmark : MonoBehaviour
{
    public float duration = 0.1f;    
    float lastTimeUpdate;

	void Update ()
    {
        lastTimeUpdate = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - lastTimeUpdate < duration)
        {
            
        }        
	}
}
