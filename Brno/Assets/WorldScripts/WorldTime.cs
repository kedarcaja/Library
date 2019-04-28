using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EDays { Monday, Tuesday, Wednesday, Thurstday, Friday, Saturday, Sunday }
public class WorldTime : MonoBehaviour
{
	private int minutes, seconds, hours;
	private EDays days = EDays.Monday;
	public event Action OnTimerStart, OnTimerEnd, OnTimerUpdate, OnDayChange, OnTimeSkip;
	private const float delay = 1.5f;
	public bool IsRunning = true;
	public int Seconds { get; private set; }
	public int Minutes { get; private set; }
	public int Hours { get; private set; }
	public EDays Day { get; private set; }
	public TimeSpan GetTimeAsTimeSpan { get { return new TimeSpan(Hours, Minutes, Seconds); } }
	public static WorldTime Instance;
	private void Awake()
	{
		Instance = FindObjectOfType<WorldTime>();
	}
	private void Start()
	{

		Begin();

	}
	private void Update()
	{
	//	Debug.Log("Day: " + Day.ToString() + " Hours: " + Hours + " Minutes: " + Minutes + " Seconds: " + Seconds);
		if (Input.GetKeyDown(KeyCode.T))
		{
			Skip(3600);
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			Stop();
		}
		if (Input.GetKeyDown(KeyCode.O))
		{
			Begin();
		}
	}

	public void Begin()
	{
		IsRunning = true;
		if (OnTimerStart != null)
		{
			OnTimerStart();
		}

		StartCoroutine("UpdateTime");

	}

	private IEnumerator UpdateTime()
	{

		while (IsRunning)
		{
			yield return new WaitForSecondsRealtime(delay);

			seconds++;

			if (OnTimerUpdate != null)
			{
				OnTimerUpdate();
			}
			if (seconds >= 60)
			{

				minutes += ((seconds - (seconds % 60)) / 60);

				seconds = seconds % 60;


			}
			if (minutes >= 60)
			{
				hours += ((minutes - (minutes % 60)) / 60);

				minutes = minutes % 60;


			}
			if (hours == 24)
			{
				if ((int)days == 7)
				{
					days = 0;
				}
				else
				{
					days++;
				}
				hours = 0;
				if (OnDayChange != null)
				{
					OnDayChange();
				}
			}
			Seconds = seconds;
			Minutes = minutes;
			Hours = hours;
			Day = days;
			Restart();
		}
	}
	public void Stop()
	{
		if (IsRunning)
		{
			if (OnTimerEnd != null)
			{
				OnTimerEnd();
			}
			IsRunning = false;

			StopAllCoroutines();

		}
	}
	private void Restart()
	{
		Stop();
		Begin();
	}
	public void Skip(int seconds)
	{
		Stop();

		this.seconds += seconds;
		if (OnTimeSkip != null)
		{
			OnTimeSkip();
		}
		Begin();
	}


}
[Serializable]
public struct GlobalTimeSpan
{
	[SerializeField]
	private int seconds, minutes, hours;

	public int Seconds
	{
		get
		{
			return seconds;
		}


	}

	public int Minutes
	{
		get
		{
			return minutes;
		}


	}

	public int Hours
	{
		get
		{
			return hours;
		}


	}
	public TimeSpan TimeSpan { get { return new TimeSpan(Hours, Minutes, Seconds); } }
}






