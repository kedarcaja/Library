using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public NewPage newPage;
    public CategoryControler category;
    public Transform[] page;

    private void Update()
    {
        for (int j = 0; j < category.Part.Length; j++)
        {
            if (category.Part[j].IsView)
            {
                this.GetComponent<Image>().sprite = category.Part[j].Portrait;
            }
        }
        }

    public void Click()
    {
        for (int j = 0; j < category.Part.Length; j++)
        {
            if (category.Part[j].IsView)
            {
                for (int i = 0; i < page.Length; i++)
                {
                    newPage.Destroy(page[i]);
                }
                newPage.Create();
            }
        }
    }
}