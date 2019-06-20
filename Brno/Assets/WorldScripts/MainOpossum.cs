using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class MainOpossum : MonoBehaviour
{
	public static WeatherOpossum WeatherOpossum { get; private set; }
	public static EntityOpossum EntityOpossum { get; private set; }
	public static WorldTimeOpossum WorldTimeOpossum { get; private set; }


	private void Awake()
	{
		WeatherOpossum = FindObjectOfType<WeatherOpossum>();
		EntityOpossum = FindObjectOfType<EntityOpossum>();
		WorldTimeOpossum = FindObjectOfType<WorldTimeOpossum>();
	}

	public static  EWeather GetWeather(CharacterScript character)
	{
		return WeatherOpossum.GetWeatherInBiom(EntityOpossum.GetEntityBiom(character));
	}
	public static TimeSpan GetTime()
	{
		return WorldTimeOpossum.GetTimeAsTimeSpan;
	}
	public static EDays GetDay()
	{
		return WorldTimeOpossum.Day;
	}
	
}
