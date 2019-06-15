using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Timer
{
    public event Action OnStart, OnUpdate, OnStop,OnPause,OnRestore,OnInit,OnReset;
    public int ElapsedTimeI { get; private set; }
    public float ElapsedTimeF { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsStopped { get; private set; }
    private float updateValue;
    private float delay;
    private float currentTime;
    private MonoBehaviour starter;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateValue">value which timer increments per delay</param>
    /// <param name="delay">timer waits for this value</param>
    ///  <param name="starter">set MonoBehaviour to start coroutines</param>
    public _Timer(float updateValue, float delay, MonoBehaviour starter)
    {
        Init(updateValue, delay);
        this.starter = starter;
    }
    /// <summary>
    /// To setup of timer
    /// </summary>
    /// <param name="updateValue"></param>
    /// <param name="delay"></param>
    public void Init(float updateValue, float delay)
    {
        //Reset();
        this.updateValue = updateValue;
        this.delay = delay;

        //IsRunning = false;
        //IsStopped = true;
        if (OnInit != null)
        {
            OnInit();
        }

    }
    /// <summary>
    /// Starts timer counting
    /// </summary>
    public void Start()
    {
        if (!IsRunning)
        {
            Reset();
            IsRunning = true;
            IsStopped = false;
            starter.StartCoroutine(Run());
            if (OnStart != null)
            {
                OnStart();
            }
        }

    }
    /// <summary>
    /// Stops timer counting and resets time
    /// </summary>
    public void Stop()
    {
        if (IsRunning)
        {
            IsRunning = false;
            IsStopped = true;
            starter.StopCoroutine(Run());
            if (OnInit != null)
            {
                OnStop();
            }
            Reset();

        }

    }
    /// <summary>
    /// Pauses timer
    /// time is not restored
    /// </summary>
    public void Pause()
    {
        if (!IsStopped && IsRunning)
        {
            IsRunning = false;
            IsStopped = false;

            if (OnPause != null)
            {
                OnPause();
            }
            starter.StopCoroutine(Run());
        }
    }
    /// <summary>
    /// Restores timer when is paused
    /// </summary>
    public void Restore()
    {
        if (!IsStopped && !IsRunning)
        {
            IsRunning = true;
            IsStopped = false;
            if (OnRestore != null)
            {
                OnRestore();
            }
            starter.StartCoroutine(Run());

        }
    }
    public void Update()
    {
        if (IsStopped || !IsRunning) return;
        currentTime += updateValue;
        ElapsedTimeF = currentTime;
        ElapsedTimeI = (int)currentTime;
        if (OnUpdate != null)
        {
            OnUpdate();
        }
    }
    /// <summary>
    /// Resets timer
    /// </summary>
    /// <param name="continu"></param>
    public void Reset()
    {
        currentTime = 0;
        ElapsedTimeF = 0;
        ElapsedTimeI = 0;
        if (OnReset != null)
        {
            OnReset();
        }
    }
    private IEnumerator Run()
    {
       
        while (!IsStopped && IsRunning)
        {
          

                yield return new WaitForSeconds(delay);
                Update();
            
        }
    }
}
