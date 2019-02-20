using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{

    public string Name;
    private LernController lCont;
    public NewPage newPage;
    public CategoryControler category;
    

    private void Start()
    {
        lCont = (LernController)FindObjectOfType(typeof(LernController));
    }

    private void Update()
    {
        for (int j = 0; j < category.Part.Length; j++)
        {
            if (category.Part[j].Name == Name && category.Part[j].IsView)
            {
                this.GetComponent<Image>().sprite = category.Part[j].Portrait;
            }
        }
    }

    public void Click()
    {
        lCont.PageIndex = 1;
        for (int j = 0; j < category.Part.Length; j++)
        {
            if (category.Part[j].Name == Name && category.Part[j].IsView)
            {
                for (int i = 0; i < lCont.page.Length; i++)
                {
                    newPage.Destroy(lCont.page[i].GetComponent<Transform>());
                }
                newPage.Create();
            }
        }
    }

    
}