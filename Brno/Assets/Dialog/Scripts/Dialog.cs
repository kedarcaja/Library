using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;

[CreateAssetMenu(menuName = "Dialog/Dialog")]
public class Dialog : ScriptableObject
{
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private List<DialogPart> parts = new List<DialogPart>();
    private Queue<DialogPart> partsStack = new Queue<DialogPart>();
    public UnityEvent OnStart, OnEnd, OnPause, OnUnPause;
    [SerializeField]
    private bool wasPlayed = false;
    [SerializeField]
    private bool repeatable;
    [SerializeField]
    private int repeatLimit;
    [SerializeField]
    private bool isPaused = false, isPlaying = false;
    private _Timer timer;
    public bool IsEnable = true;
    public bool destroyOnEnd = false;
    [SerializeField] // vzdy po startu nastavit na false
    private bool isInitialized = false;
    /// <summary>
    /// Method used to restore some variables
    /// </summary>
    public void StartInit()
    {
        isPlaying = false;
        isInitialized = false;
        isPaused = false;
    }

    public void InitParts()
    {
        partsStack.Clear();
        for (int i = 0; i < parts.Count(); i++)
        {
            partsStack.Enqueue(parts[i]);
        }
    }

   
    /// <summary>
    /// Call befor using dialog to initialize values
    /// </summary>
    private void Init(MonoBehaviour starter)
    {
        if (!IsEnable || isInitialized) return;
        InitParts();
        timer = new _Timer(1, partsStack.Peek().StartDuration, starter);

        isInitialized = true;
        timer.OnUpdate += delegate
        {
            if(partsStack.Peek())
            {
                partsStack.Dequeue();
            }

            if (partsStack.Peek())
            {
                Begin(partsStack.Peek());
            }
        };

    }
    /// <summary>
    /// starts dialog
    /// </summary>
    public void Play(MonoBehaviour starter)
    {
        
        Init(starter);

        if ((repeatable && repeatLimit == 0) || (!repeatable && wasPlayed) || isPlaying || isPaused || !IsEnable||!isInitialized) return;
        wasPlayed = true;
        isPaused = false;
        DialogManager.Instance.currentDialog = this;
        DialogManager.Instance.AudioPlayer.clip = clip;
        DialogManager.Instance.AudioPlayer.Play();
        Begin(partsStack.Peek());
        isPlaying = true;

        Debug.Log(isPlaying);

        if (repeatable)
        {
            repeatLimit--;
        }
        timer.Start();
    }
    /// <summary>
    /// Sets subtitles to screen
    /// </summary>
    /// <param name="sub"></param>
    private void SetSubtitles(string sub)
    {
        DialogManager.Instance.SubtitleArea.text = sub;
    }
    /// <summary>
    /// sets subtitles of current part of dialog
    /// </summary>
    /// <param name="part"></param>
    private void Begin(DialogPart part)
    {
        
        if (part == null || !IsEnable) { Stop(); return; };
        timer.Init(1,part.StartDuration);
        SetSubtitles(part.ToString());

    }
    /// <summary>
    /// pauses dialog
    /// </summary>
    public void Pause()
    {
        if (isPaused || !isPlaying) return;
        timer.Pause();
        isPaused = true;
        isPlaying = false;

        DialogManager.Instance.AudioPlayer.Pause();


    }

    /// <summary>
    /// stops dialog
    /// </summary>
    public void Stop()
    {
        if (!isPlaying) return;
        timer.Stop();
        isPlaying = false;
        isPaused = false;
        DialogManager.Instance.AudioPlayer.Stop();
        SetSubtitles("");
        Debug.Log("stopped");
        DialogManager.Instance.currentDialog = null;

    }

    /// <summary>
    /// unpauses dialog if is stopped
    /// </summary>
    public void UnPause()
    {
        if (isPlaying) return;

        timer.Restore();
        isPaused = false;
        isPlaying = true;
        DialogManager.Instance.AudioPlayer.UnPause();

    }

    public void Log(string txt)
    {
        Debug.Log(txt);
    }
}
