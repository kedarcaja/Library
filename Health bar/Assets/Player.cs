using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    #region Variables
    [SerializeField]
    private Stats health;
    [SerializeField]
    private Stats Energy;
    [SerializeField]
    private Stats Shield;
    #endregion

    #region Unity Metod

    private void Awake()
    {
        health.Initialize();
        Energy.Initialize();
        Shield.Initialize();

    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.CurrentVal -= 10;
            Energy.CurrentVal -= 5;
            Shield.CurrentVal -= 20;

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            health.CurrentVal += 10;
            Energy.CurrentVal += 5;
            Shield.CurrentVal += 20;
        }

    }
  #endregion  
}
