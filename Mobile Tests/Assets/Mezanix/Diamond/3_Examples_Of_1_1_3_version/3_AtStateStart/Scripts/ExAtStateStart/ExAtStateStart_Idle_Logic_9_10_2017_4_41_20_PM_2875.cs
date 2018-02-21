using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Idle_Logic_9_10_2017_4_41_20_PM_2875 
	{
		ExAtStateStart_Idle_Logic exAtStateStart_Idle_Logic;

		public bool boolValue = false;

		public KeyCode keyCode;

		public ExAtStateStart_Idle_Logic_9_10_2017_4_41_20_PM_2875 (ExAtStateStart_Idle_Logic setExAtStateStart_Idle_Logic) 
		{
			exAtStateStart_Idle_Logic = setExAtStateStart_Idle_Logic;

			exAtStateStart_Idle_Logic.IAmHere ();

			keyCode = KeyCode.C;
		}

		public void LogicNodeUpdate ()
		{
			ComputeUnityInputClassAndCrossPlatform ();
		}

		void ComputeUnityInputClassAndCrossPlatform ()
		{

			boolValue = Input.GetKeyDown (keyCode);

		}
	}
}
