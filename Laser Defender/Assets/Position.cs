﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

	void OnDrawGizmos()
    {
      //Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, 1);
        //  Gizmos.DrawIcon(transform.position, "plkm.png");        //Assets/Gizmos/*.png
      
    }
		
	}

