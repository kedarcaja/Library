using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public CanvasGroup canvas;
    bool isOpen;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Open();
        }
    }

    public void Open()
    {
        if (!isOpen)
        {
            canvas.alpha = 1;
            canvas.blocksRaycasts = true;
            isOpen = true;
        }
        else
        {
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
            isOpen = false;
        }
    }
}
