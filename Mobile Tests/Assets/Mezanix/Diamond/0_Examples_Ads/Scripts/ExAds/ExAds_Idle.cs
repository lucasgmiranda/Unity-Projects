using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAds_Idle : IExAds 
	{
		public ExAds_Idle_Ad exAds_Idle_Ad;
		
		public ExAds exAds; 

		public ExAds_Idle (ExAds setExAds) 
		{
			exAds = setExAds;

			exAds_Idle_Ad = new ExAds_Idle_Ad (this);
		}

		public void StateUpdate () 
		{
			exAds_Idle_Ad.LogicUpdate ();
		}

		public void ToIdle ()
		{
			exAds.currentState = exAds.idle;

		}

	}
}
