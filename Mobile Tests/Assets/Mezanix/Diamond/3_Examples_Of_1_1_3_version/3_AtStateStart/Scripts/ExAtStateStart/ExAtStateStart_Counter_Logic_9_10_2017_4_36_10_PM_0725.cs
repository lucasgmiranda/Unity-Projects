using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725 
	{
		ExAtStateStart_Counter_Logic exAtStateStart_Counter_Logic;

		public bool boolValue = false;

		public int [] intValues = new int[3];

		public ExAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725 (ExAtStateStart_Counter_Logic setExAtStateStart_Counter_Logic) 
		{
			exAtStateStart_Counter_Logic = setExAtStateStart_Counter_Logic;

			exAtStateStart_Counter_Logic.IAmHere ();

			intValues [0] = 0;
			intValues [1] = 32;
			intValues [2] = 0;

			intValues [0] = 0;
		}

		public void LogicNodeUpdate ()
		{
			if (intValues [0] < intValues [1]+1)
			{
				intValues [0]++;
				boolValue = true;
			}

			if (intValues [0] >= intValues [1]+1)
			{
				boolValue = false;
			}

		}

	}
}
