using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Counter_Logic_9_10_2017_4_38_06_PM_1356 
	{
		ExAtStateStart_Counter_Logic exAtStateStart_Counter_Logic;

		public bool [] boolValues = new bool[2];

		public ExAtStateStart_Counter_Logic_9_10_2017_4_38_06_PM_1356 (ExAtStateStart_Counter_Logic setExAtStateStart_Counter_Logic) 
		{
			exAtStateStart_Counter_Logic = setExAtStateStart_Counter_Logic;

			exAtStateStart_Counter_Logic.IAmHere ();

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			boolValues [0] = exAtStateStart_Counter_Logic.exAtStateStart_Counter_Logic_9_10_2017_4_37_47_PM_0516.boolValue;

			GoToState ();
		}

		void GoToState ()
		{
			if ( ! boolValues [0])
			{
				return; 
			}

			exAtStateStart_Counter_Logic.exAtStateStart_Counter.ToIdle ();

		}

	}
}
