using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LernImagePart
{
    [SerializeField]
    private bool isDiscovered;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private string page;
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private Sprite image;

    public GameObject Prefab
    {
        get
        {
            return prefab;
        }

        set
        {
            prefab = value;
        }
    }
    public Vector3 Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }
    public Vector2 Size
    {
        get
        {
            return size;
        }

        set
        {
            size = value;
        }
    }
    public Sprite Image
    {
        get
        {
            return image;
        }

        set
        {
            image = value;
        }
    }

    public bool IsDiscovered
    {
        get
        {
            return isDiscovered;
        }

        set
        {
            isDiscovered = value;
        }
    }

    public string Page
    {
        get
        {
            return page;
        }

        set
        {
            page = value;
        }
    }
}
