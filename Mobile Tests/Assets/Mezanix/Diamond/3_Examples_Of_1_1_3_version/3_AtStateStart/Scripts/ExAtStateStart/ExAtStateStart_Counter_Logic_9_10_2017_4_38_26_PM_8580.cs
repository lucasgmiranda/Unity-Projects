using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Counter_Logic_9_10_2017_4_38_26_PM_8580 
	{
		ExAtStateStart_Counter_Logic exAtStateStart_Counter_Logic;

		public int intValue = 0;

		public int [] intValues = new int[3];

		public bool [] boolValues = new bool[2];

		public ExAtStateStart_Counter_Logic_9_10_2017_4_38_26_PM_8580 (ExAtStateStart_Counter_Logic setExAtStateStart_Counter_Logic) 
		{
			exAtStateStart_Counter_Logic = setExAtStateStart_Counter_Logic;

			exAtStateStart_Counter_Logic.IAmHere ();

			intValue = 0;
			intValues [0] = 0;
			intValues [1] = 0;
			intValues [2] = 0;

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			ComputeInt ();
		}

		void ForGet_int_Compute ()
		{
			if (boolValues [0])
			{
				intValue = intValues [0];
			}
		}		void ComputeInt ()
		{

			ForGet_int_Compute ();

		}
	}
}
