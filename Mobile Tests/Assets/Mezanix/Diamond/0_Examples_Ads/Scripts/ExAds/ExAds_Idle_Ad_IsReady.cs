using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAds_Idle_Ad_IsReady 
	{
		ExAds_Idle_Ad exAds_Idle_Ad;

		public bool doIT = false;

		public bool boolValue = false;

		public ExAds_Idle_Ad_IsReady (ExAds_Idle_Ad setExAds_Idle_Ad) 
		{
			exAds_Idle_Ad = setExAds_Idle_Ad;

			exAds_Idle_Ad.IAmHere ();

		}

		public void LogicNodeUpdate ()
		{
			doIT = true;

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
			boolValue = UnityEngine.Advertisements.Advertisement.IsReady ();
			#endif

		}
	}
}
