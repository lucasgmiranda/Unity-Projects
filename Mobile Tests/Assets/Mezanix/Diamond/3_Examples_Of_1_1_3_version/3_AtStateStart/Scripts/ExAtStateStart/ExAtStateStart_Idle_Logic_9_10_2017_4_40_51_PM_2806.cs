using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Idle_Logic_9_10_2017_4_40_51_PM_2806 
	{
		ExAtStateStart_Idle_Logic exAtStateStart_Idle_Logic;

		public bool boolValue = false;

		public int [] intValues = new int[3];

		public ExAtStateStart_Idle_Logic_9_10_2017_4_40_51_PM_2806 (ExAtStateStart_Idle_Logic setExAtStateStart_Idle_Logic) 
		{
			exAtStateStart_Idle_Logic = setExAtStateStart_Idle_Logic;

			exAtStateStart_Idle_Logic.IAmHere ();

			intValues [0] = 0;
			intValues [1] = 1;
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
