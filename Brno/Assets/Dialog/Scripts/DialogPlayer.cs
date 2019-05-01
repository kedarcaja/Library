using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogPlayer : MonoBehaviour
{
    [SerializeField]
    private Dialog dialog;
    public Dialog Dialog { get => dialog; set => dialog = value; }

    public void Play()
    {
        if (Dialog)
        {
            Dialog.Play(this);
            if (Dialog.OnStart != null)
            {
                Dialog.OnStart.Invoke();
            }
        }
    }
    public void Stop()
    {
        if (Dialog)
        {
            Dialog.Stop();
            if (Dialog.OnEnd != null)
            {
                Dialog.OnEnd.Invoke();
            }
        }
    }
    public void Pause()
    {
        if (Dialog)
        {
            Dialog.Pause();
            if (Dialog.OnPause != null)
            {
                Dialog.OnPause.Invoke();
            }
        }
    }
    public void UnPause()
    {
        if (Dialog)
        {
            Dialog.UnPause();
            if (Dialog.OnUnPause != null)
            {
                Dialog.OnUnPause.Invoke();
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Play();
        }


    }


    private void OnApplicationQuit()
    {
        if(dialog)
        {
            dialog.Stop();
        }
    }
  
}


