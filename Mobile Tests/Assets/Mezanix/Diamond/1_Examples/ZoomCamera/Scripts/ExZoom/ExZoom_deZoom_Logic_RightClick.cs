using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_deZoom_Logic_RightClick 
	{
		ExZoom_deZoom_Logic exZoom_deZoom_Logic;

		public bool boolValue = false;

		public int [] intValues = new int[3];

		public ExZoom_deZoom_Logic_RightClick (ExZoom_deZoom_Logic setExZoom_deZoom_Logic) 
		{
			exZoom_deZoom_Logic = setExZoom_deZoom_Logic;

			exZoom_deZoom_Logic.IAmHere ();

			intValues [0] = 1;
			intValues [1] = 0;
			intValues [2] = 0;

		}

		public void LogicNodeUpdate ()
		{
			ComputeUnityInputClassAndCrossPlatform ();
		}

		void ComputeUnityInputClassAndCrossPlatform ()
		{

			boolValue = Input.GetMouseButtonDown (intValues [0]);

		}
	}
}
