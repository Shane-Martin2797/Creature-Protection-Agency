﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class Scenes 
{
	public static string level = "Creature Controller";
	public static string logo = "LogoSplash";
	public static string Title = "Title";
	public static string Main = "Main";
	public static string HUD = "HUD";
	public static string StartingCinematic = "StartingCinematic";
	public static string Tutorial = "Tutorial";
}

public class LogoController : MonoBehaviour 
{
	Image imageComp;
	public float logoTime;

	float initialLogoTime;

	void Awake () 
	{
		imageComp = GetComponent<Image> ();

		tempColor = imageComp.color;

		initialLogoTime = logoTime;
	}

	Color tempColor;
	void Update () 
	{
		tempColor.a = Mathf.Sin (logoTime/initialLogoTime * Mathf.PI);

		imageComp.color = tempColor;

		logoTime -= Time.deltaTime;

		if (logoTime < 0) 
		{
			Application.LoadLevel(Scenes.Title);
		}
	}
}
