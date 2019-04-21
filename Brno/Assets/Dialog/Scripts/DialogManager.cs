using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI subtitleArea;
    public TextMeshProUGUI SubtitleArea { get => subtitleArea; }
    public AudioSource AudioPlayer { get { return GetComponent<AudioSource>(); } }

    public static DialogManager Instance { get { return FindObjectOfType<DialogManager>(); } }

}
