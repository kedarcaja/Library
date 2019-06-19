using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biom : MonoBehaviour
{
    public bool SunyDay { get; private set; }
    [SerializeField]
    private float sunyProbability, radius, weatherChange;
    [SerializeField]
    private Color gizmosColor;
    public TimeSpan lastCheck { get; private set; } = new TimeSpan();
    private void Start()
    {
        CheckWeather();
    }
    private void Update()
    {
        if (SunyDay)
        {

            Debug.Log("Is sunny day...");

        }
        else
        {
            Debug.Log("Is raining...");

        }
        
    }
    public void CheckWeather()
    {

        if (WorldTime.Instance.Seconds >= weatherChange+sunyProbability)
        {
            lastCheck = WorldTime.Instance.GetTimeAsTimeSpan;
            SunyDay = !SunyDay;

            Debug.Log("biom weather has been changed...");
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
