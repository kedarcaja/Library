using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName ="CharacterData/NPC",fileName ="NewNPCDATA")]
public class NPC : Character
{


    #region Stats
    [SerializeField]
    private bool randomMove;

    public bool RandomMove
    {
        get
        {
            return randomMove;
        }

        set
        {
            randomMove = value;
        }
    }
    #endregion

}