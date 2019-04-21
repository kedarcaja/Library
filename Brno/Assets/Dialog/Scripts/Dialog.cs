using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private List<DialogPart> parts;
    public UnityEvent OnStart, OnEnd, OnSubtitleSwap;
    private bool wasPlayed;
    [SerializeField]
    private int repeatLimit;
    private int currentSubtitles = 0;
    public void Play()
    {
        Restore();
        wasPlayed = true;
        DialogManager.Instance.AudioPlayer.clip = clip;

        if (OnStart != null)
        {
            OnStart.Invoke();
        }
        //StartCoroutine();
    }
    public void Skip()
    {
        End();
    }
    /// <summary>
    /// resets subtitles and countsdown repeat limit
    /// </summary>
    public void Restore()
    {
        if (repeatLimit > 0)
        {
            repeatLimit--;
            currentSubtitles = 0;
        }
    }
    private void End()
    {
        DialogManager.Instance.SubtitleArea.text = "";
        DialogManager.Instance.AudioPlayer.clip = null;
        if (OnEnd != null)
        {
            OnEnd.Invoke();
        }

    }
    public void Pause()
    {
        DialogManager.Instance.AudioPlayer.Pause();
        //StopCoroutine();

    }
    public void Continue()
    {
        DialogManager.Instance.AudioPlayer.Play();
        //StartCoroutine();
    }

}
