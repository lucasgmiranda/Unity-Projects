using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_zoom_Logic_copy_RightClick 
	{
		ExZoom_zoom_Logic_copy exZoom_zoom_Logic_copy;

		public bool boolValue = false;

		public int [] intValues = new int[3];

		public ExZoom_zoom_Logic_copy_RightClick (ExZoom_zoom_Logic_copy setExZoom_zoom_Logic_copy) 
		{
			exZoom_zoom_Logic_copy = setExZoom_zoom_Logic_copy;

			exZoom_zoom_Logic_copy.IAmHere ();

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
