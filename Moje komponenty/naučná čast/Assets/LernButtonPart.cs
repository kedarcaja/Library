using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LernButtonPart 
{
    [SerializeField]
    private string name;
    [SerializeField]
    private bool isView;
    [SerializeField]
    private Sprite portrait;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public bool IsView
    {
        get
        {
            return isView;
        }

        set
        {
            isView = value;
        }
    }

    public Sprite Portrait
    {
        get
        {
            return portrait;
        }

        set
        {
            portrait = value;
        }
    }
}
