using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAds_Idle_Ad_IsReadyToString 
	{
		ExAds_Idle_Ad exAds_Idle_Ad;

		public bool [] boolValues = new bool[2];

		public string stringValue;

		public ExAds_Idle_Ad_IsReadyToString (ExAds_Idle_Ad setExAds_Idle_Ad) 
		{
			exAds_Idle_Ad = setExAds_Idle_Ad;

			exAds_Idle_Ad.IAmHere ();

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			boolValues [0] = exAds_Idle_Ad.exAds_Idle_Ad_IsReady.boolValue;

			stringValue = boolValues [0].ToString ();
		}

	}
}
