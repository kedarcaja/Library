using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup playerUI;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = FindObjectOfType<UIManager>();
    }


    public void HideUI()
    {
        playerUI.alpha = 0;
        playerUI.blocksRaycasts = false;
    }

    public void ShowUI()
    {
        playerUI.alpha = 1;
        playerUI.blocksRaycasts = true;
    }
}
