using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAds_Idle_Ad_Show 
	{
		ExAds_Idle_Ad exAds_Idle_Ad;

		public bool doIT = false;

		public string stringValue;

		public bool [] boolValues = new bool[2];

		public ExAds_Idle_Ad_Show (ExAds_Idle_Ad setExAds_Idle_Ad) 
		{
			exAds_Idle_Ad = setExAds_Idle_Ad;

			exAds_Idle_Ad.IAmHere ();

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			doIT = exAds_Idle_Ad.exAds_Idle_Ad_Button.boolValue;

			ComputeAds ();
		}

		void ShowAd ()
		{
			#if UNITY_IOS || UNITY_ANDROID
			if (UnityEngine.Advertisements.Advertisement.IsReady ())
			{			
				if (boolValues [0])
				{
					UnityEngine.Advertisements.Advertisement.Show ("rewardedVideo", 
					new UnityEngine.Advertisements.ShowOptions (){resultCallback = HandleAdResult});
				}
				else
				{
					UnityEngine.Advertisements.Advertisement.Show ();
				}
			}
			#endif
		}

		#if UNITY_IOS || UNITY_ANDROID
		void HandleAdResult (UnityEngine.Advertisements.ShowResult result)
		{
			switch (result)
			{
			case UnityEngine.Advertisements.ShowResult.Failed:
				DiamodAds.showResult = "Failed";
				break;

			case UnityEngine.Advertisements.ShowResult.Finished:
				DiamodAds.showResult = "Finished";
				break;

			case UnityEngine.Advertisements.ShowResult.Skipped:
				DiamodAds.showResult = "Skipped";
				break;
			}

		}
		#endif
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
			ShowAd ();

		}
	}
}
