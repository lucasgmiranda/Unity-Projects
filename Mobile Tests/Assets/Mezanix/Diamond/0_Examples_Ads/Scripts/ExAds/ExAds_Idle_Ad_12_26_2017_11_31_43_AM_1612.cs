using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAds_Idle_Ad_12_26_2017_11_31_43_AM_1612 
	{
		ExAds_Idle_Ad exAds_Idle_Ad;

		public bool doIT = false;

		public string stringValue;

		public string [] stringValues = new string[2];

		public ExAds_Idle_Ad_12_26_2017_11_31_43_AM_1612 (ExAds_Idle_Ad setExAds_Idle_Ad) 
		{
			exAds_Idle_Ad = setExAds_Idle_Ad;

			exAds_Idle_Ad.IAmHere ();

			stringValues [0] = "IsReady?  ";
			stringValues [1] = "False";
		}

		public void LogicNodeUpdate ()
		{
			stringValues [1] = exAds_Idle_Ad.exAds_Idle_Ad_IsReadyToString.stringValue;

			doIT = true;

			ComputeString ();
		}

		void ComputeString ()
		{
			if ( ! doIT)
			{
				return;
			}


			stringValue = "" + stringValues [0] + stringValues [1];

		}
	}
}
