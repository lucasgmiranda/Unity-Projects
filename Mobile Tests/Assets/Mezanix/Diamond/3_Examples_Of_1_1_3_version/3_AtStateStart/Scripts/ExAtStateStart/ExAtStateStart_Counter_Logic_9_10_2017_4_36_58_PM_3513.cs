using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Counter_Logic_9_10_2017_4_36_58_PM_3513 
	{
		ExAtStateStart_Counter_Logic exAtStateStart_Counter_Logic;

		public bool doIT = false;

		public int [] intValues = new int[3];

		public string stringValue;

		public ExAtStateStart_Counter_Logic_9_10_2017_4_36_58_PM_3513 (ExAtStateStart_Counter_Logic setExAtStateStart_Counter_Logic) 
		{
			exAtStateStart_Counter_Logic = setExAtStateStart_Counter_Logic;

			exAtStateStart_Counter_Logic.IAmHere ();

			intValues [0] = 0;
			intValues [1] = 0;
			intValues [2] = 0;

		}

		public void LogicNodeUpdate ()
		{
			intValues [0] = exAtStateStart_Counter_Logic.exAtStateStart_Counter_Logic_9_10_2017_4_36_26_PM_6665.intValue;

			doIT = exAtStateStart_Counter_Logic.exAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725.boolValue;

			ComputeInt ();
		}

		void ComputeInt ()
		{
			if ( ! doIT)
			{
				return;
			}


			stringValue = intValues [0].ToString ();

		}
	}
}
