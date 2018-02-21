using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Counter_Logic_9_10_2017_4_37_47_PM_0516 
	{
		ExAtStateStart_Counter_Logic exAtStateStart_Counter_Logic;

		public bool boolValue = false;

		public KeyCode keyCode;

		public ExAtStateStart_Counter_Logic_9_10_2017_4_37_47_PM_0516 (ExAtStateStart_Counter_Logic setExAtStateStart_Counter_Logic) 
		{
			exAtStateStart_Counter_Logic = setExAtStateStart_Counter_Logic;

			exAtStateStart_Counter_Logic.IAmHere ();

			keyCode = KeyCode.R;
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
