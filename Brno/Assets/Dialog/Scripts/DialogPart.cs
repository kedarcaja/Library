using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this is one part of subtitles in dialog
/// </summary>
[CreateAssetMenu(fileName ="NewDialPart",menuName ="Dialog/Part")]
public class DialogPart : ScriptableObject
{
    [SerializeField]
    private Character speaker;
    [TextArea(10,100)]
    [SerializeField]
    private string subtitles;
    [Range(0.5f,10)]
    [SerializeField]
    private float startDuration;
    public float StartDuration { get => startDuration; }
    public override string ToString()
    {
        return speaker.ToString() + ": " + subtitles;
    }
   
}
