using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Counter_Logic_9_10_2017_4_36_26_PM_6665 
	{
		ExAtStateStart_Counter_Logic exAtStateStart_Counter_Logic;

		public bool doIT = false;

		public int intValue = 0;

		public int [] intValues = new int[3];

		public ExAtStateStart_Counter_Logic_9_10_2017_4_36_26_PM_6665 (ExAtStateStart_Counter_Logic setExAtStateStart_Counter_Logic) 
		{
			exAtStateStart_Counter_Logic = setExAtStateStart_Counter_Logic;

			exAtStateStart_Counter_Logic.IAmHere ();

			intValues [0] = 0;
			intValues [1] = 1;
			intValues [2] = 0;

		}

		public void LogicNodeUpdate ()
		{
			if (exAtStateStart_Counter_Logic.exAtStateStart_Counter_Logic_9_10_2017_4_37_47_PM_0516.boolValue)
				intValues [0] = exAtStateStart_Counter_Logic.exAtStateStart_Counter_Logic_9_10_2017_4_38_26_PM_8580.intValue;

			doIT = exAtStateStart_Counter_Logic.exAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725.boolValue;

			ComputeInt ();
		}

		void ComputeInt ()
		{
			if ( ! doIT)
			{
				return;
			}


			intValue = intValues [0] + intValues [1];
			intValues [0] = intValue;

		}
	}
}
