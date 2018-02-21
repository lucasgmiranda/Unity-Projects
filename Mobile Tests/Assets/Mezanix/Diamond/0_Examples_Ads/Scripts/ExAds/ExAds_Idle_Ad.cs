using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAds_Idle_Ad 
	{
		public ExAds_Idle_Ad_Start exAds_Idle_Ad_Start;
		public ExAds_Idle_Ad_Init_ads exAds_Idle_Ad_Init_ads;
		public ExAds_Idle_Ad_Button exAds_Idle_Ad_Button;
		public ExAds_Idle_Ad_Show exAds_Idle_Ad_Show;
		public ExAds_Idle_Ad_IsReady exAds_Idle_Ad_IsReady;
		public ExAds_Idle_Ad_IsReadyToString exAds_Idle_Ad_IsReadyToString;
		public ExAds_Idle_Ad_12_26_2017_11_31_43_AM_1612 exAds_Idle_Ad_12_26_2017_11_31_43_AM_1612;
		public ExAds_Idle_Ad_IsReadyUiText exAds_Idle_Ad_IsReadyUiText;
		
		public ExAds_Idle exAds_Idle;

		public ExAds_Idle_Ad (ExAds_Idle setExAds_Idle)
		{
			exAds_Idle = setExAds_Idle;

			exAds_Idle_Ad_Start = new ExAds_Idle_Ad_Start (this);
			exAds_Idle_Ad_Init_ads = new ExAds_Idle_Ad_Init_ads (this);
			exAds_Idle_Ad_Button = new ExAds_Idle_Ad_Button (this);
			exAds_Idle_Ad_Show = new ExAds_Idle_Ad_Show (this);
			exAds_Idle_Ad_IsReady = new ExAds_Idle_Ad_IsReady (this);
			exAds_Idle_Ad_IsReadyToString = new ExAds_Idle_Ad_IsReadyToString (this);
			exAds_Idle_Ad_12_26_2017_11_31_43_AM_1612 = new ExAds_Idle_Ad_12_26_2017_11_31_43_AM_1612 (this);
			exAds_Idle_Ad_IsReadyUiText = new ExAds_Idle_Ad_IsReadyUiText (this);
		}

		public void LogicUpdate ()
		{
			exAds_Idle_Ad_Start.LogicNodeUpdate ();
			exAds_Idle_Ad_Init_ads.LogicNodeUpdate ();
			exAds_Idle_Ad_Button.LogicNodeUpdate ();
			exAds_Idle_Ad_Show.LogicNodeUpdate ();
			exAds_Idle_Ad_IsReady.LogicNodeUpdate ();
			exAds_Idle_Ad_IsReadyToString.LogicNodeUpdate ();
			exAds_Idle_Ad_12_26_2017_11_31_43_AM_1612.LogicNodeUpdate ();
			exAds_Idle_Ad_IsReadyUiText.LogicNodeUpdate ();
		}

		public void IAmHere ()
		{
		}
	}
}
