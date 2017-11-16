using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestControl : MonoBehaviour
{
    public Scrollbar scrollbar;
    public GameObject Panel;
    bool Q1 = true;
    void Start()
    {
        scrollbar.value = 1;
    }

    public void quest() {
        if (!Q1 == false)
        {
            Panel.SetActive(true);
            scrollbar.value = 1;
            Q1 = false;
        }
        else { Panel.SetActive(false); Q1 = true; } 
    }
}
