using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI subtitleArea;
    public TextMeshProUGUI SubtitleArea { get => subtitleArea; }
    public AudioSource AudioPlayer { get { return GetComponent<AudioSource>(); } }

    public static DialogManager Instance { get { return FindObjectOfType<DialogManager>(); } }
    public Dialog currentDialog { get; set; }
    public UnityEvent OnStart;
    [SerializeField]
    private DialogPlayer defaultPlayer;
    /// <summary>
    /// Plays specific dialog
    /// </summary>
    /// <param name="d"></param>
    public void PlayDialog(Dialog d)
    {
        if (d)
        {
            d.Play(this);
            Instantiate(defaultPlayer.gameObject);
            defaultPlayer.Dialog = d;
            defaultPlayer.Dialog.destroyOnEnd = true;
            defaultPlayer.Play();
        }
    }
    /// <summary>
    /// Plays dialog on specific dialog player
    /// </summary>
    /// <param name="p">object who is able to play dialog</param>
    public void PlayDialog(DialogPlayer p)
    {
        if (p)
        {
            p.Play();
        }
    }
    /// <summary>
    /// Stops current dialog
    /// </summary>
    public void StopCurrentDialog()
    {
        if (currentDialog)
        {
            currentDialog.Stop();
        }
    }
    /// <summary>
    /// Pauses current dialog
    /// </summary>
    public void PauseCurrentDialog()
    {
        currentDialog.Pause();
    }
    /// <summary>
    /// UnPauses current dialog
    /// </summary>
    public void UnPauseCurrentDialog()
    {
        if (currentDialog)
        {
            currentDialog.UnPause();
        }
    }
    private void Start()
    {
        if (OnStart != null)
        {
            OnStart.Invoke();
        }
    }




}
