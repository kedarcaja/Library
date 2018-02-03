using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class NewGrid : MonoBehaviour {
    public float snapInterval = 0.7f;
    private void Update()
    {
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        
        for (int i = 0; i < children.Length; i++)
        {
            children[i].transform.position = RoundVector(children[i].transform.position,snapInterval);
        }
    }

    float Round(float number, float interval) {
        return(float)((int)(number/interval))* interval;
    }

    Vector3 RoundVector(Vector3 vector, float interval)
    {
        return new Vector3(Round(vector.x, interval), Round(vector.y, interval), Round(vector.z, interval));
        
    }

   
}
