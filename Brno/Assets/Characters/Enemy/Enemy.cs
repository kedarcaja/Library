using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;
[CreateAssetMenu(menuName ="CharacterData/Enemy")]
public class Enemy : Entity
{
    [SerializeField]
    private bool randomMove = false;


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
}
