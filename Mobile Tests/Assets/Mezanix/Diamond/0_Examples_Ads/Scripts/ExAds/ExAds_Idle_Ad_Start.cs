using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAds_Idle_Ad_Start 
	{
		ExAds_Idle_Ad exAds_Idle_Ad;

		public bool boolValue = false;

		public int [] intValues = new int[3];

		public ExAds_Idle_Ad_Start (ExAds_Idle_Ad setExAds_Idle_Ad) 
		{
			exAds_Idle_Ad = setExAds_Idle_Ad;

			exAds_Idle_Ad.IAmHere ();

			boolValue = true;

			intValues [0] = 2;
			intValues [1] = 1;
			intValues [2] = 0;

			intValues [0] = 0;
		}

		public void LogicNodeUpdate ()
		{
			if (intValues [0] < intValues [1]+1)
			{
				intValues [0]++;
				boolValue = true;
			}

			if (intValues [0] >= intValues [1]+1)
			{
				boolValue = false;
			}

		}

	}
}
