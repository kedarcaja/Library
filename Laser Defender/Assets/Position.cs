using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
    public float explosionRadius = 0.5F;  
	void OnDrawGizmos()
    {
      
      //Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        //  Gizmos.DrawIcon(transform.position, "plkm.png");        //Assets/Gizmos/*.png
      
    }
		
	}

