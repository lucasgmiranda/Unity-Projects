using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAds_Idle_Ad_Init_ads 
	{
		ExAds_Idle_Ad exAds_Idle_Ad;

		public bool doIT = false;

		public string [] stringValues = new string[2];

		public bool [] boolValues = new bool[2];

		public ExAds_Idle_Ad_Init_ads (ExAds_Idle_Ad setExAds_Idle_Ad) 
		{
			exAds_Idle_Ad = setExAds_Idle_Ad;

			exAds_Idle_Ad.IAmHere ();

			stringValues [0] = "1649299";
			stringValues [1] = "";
			boolValues [0] = true;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			doIT = exAds_Idle_Ad.exAds_Idle_Ad_Start.boolValue;

			ComputeAds ();
		}

		void ComputeAds ()
		{
			if ( ! doIT)
			{
				return;
			}

			if ( ! Application.isPlaying)
			{
				return;
			}
			#if UNITY_IOS || UNITY_ANDROID
			UnityEngine.Advertisements.Advertisement.Initialize (stringValues [0], boolValues [0]);
			#endif

		}
	}
}
