using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageCounter : MonoBehaviour
{
    public int PageIndex;
    public GameObject[] pages;
    public GameObject main;
    private void Update()
    {
        if (PageIndex == 0) { main.SetActive(true);}
        else { main.SetActive(false);}
    }
}
