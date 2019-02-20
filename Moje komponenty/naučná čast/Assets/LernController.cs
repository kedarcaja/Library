using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LernController : MonoBehaviour
{
   
    public GameObject[] category;
    public GameObject[] page;
    public GameObject[] button;
    public GameObject main;
    int pageIndex;

    public int PageIndex
    {
        get
        {
            return pageIndex;
        }

        set
        {
            pageIndex = value;
        }
    }  

    private void Update()
    {
        PageChange();
    }

    public void ShowCategory(GameObject obj)
    {
        for (int i = 0; i < category.Length; i++)
        {
            category[i].SetActive(false);
        }
        obj.SetActive(true);
    }

    public void PageChange()
    {
        if (PageIndex == 0)
        {
            button[1].SetActive(false);
            button[0].SetActive(false);
        }
        if (PageIndex == 1)
        {
            ClearPage();
            main.SetActive(true);
            page[0].SetActive(true);
            button[1].SetActive(true);
            button[0].SetActive(false);
        }
        if (PageIndex == 2)
        {
            ClearPage();
            main.SetActive(false);
            page[1].SetActive(true);
            page[2].SetActive(true);
            button[0].SetActive(true);
            button[1].SetActive(false);
        }
    }

    public void ClearPage()
    {
        for (int i = 0; i < page.Length; i++)
        {
            page[i].SetActive(false);
        }
    }

    public void NextPage()
    {
        PageIndex = 2;
    }

    public void PreviousPage()
    {
        PageIndex = 1;
    }
}
