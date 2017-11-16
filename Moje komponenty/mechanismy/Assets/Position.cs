using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public float explosionRadius = 0.5F;
    void OnDrawGizmos()
    {
        //UnityEngine.Gizmos.color = Color.red;
        //UnityEngine.Gizmos.DrawWireSphere(transform.position, explosionRadius);
        UnityEngine.Gizmos.DrawIcon(transform.position, "plkm.png");//Assets/Gizmos/*.png

        
    }
}

