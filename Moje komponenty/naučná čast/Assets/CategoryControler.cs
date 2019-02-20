using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCategoryCotroler", menuName = "Book/CategoryCotroler", order = 2)]
public class CategoryControler : ScriptableObject
{
    [SerializeField]
    private LernButtonPart[] part;

    public LernButtonPart[] Part
    {
        get
        {
            return part;
        }

        set
        {
            part = value;
        }
    }
}
