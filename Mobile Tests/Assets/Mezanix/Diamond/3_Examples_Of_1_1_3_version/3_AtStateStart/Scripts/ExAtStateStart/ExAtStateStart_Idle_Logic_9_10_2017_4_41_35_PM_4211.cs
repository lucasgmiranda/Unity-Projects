using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Idle_Logic_9_10_2017_4_41_35_PM_4211 
	{
		ExAtStateStart_Idle_Logic exAtStateStart_Idle_Logic;

		public bool [] boolValues = new bool[2];

		public ExAtStateStart_Idle_Logic_9_10_2017_4_41_35_PM_4211 (ExAtStateStart_Idle_Logic setExAtStateStart_Idle_Logic) 
		{
			exAtStateStart_Idle_Logic = setExAtStateStart_Idle_Logic;

			exAtStateStart_Idle_Logic.IAmHere ();

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			boolValues [0] = exAtStateStart_Idle_Logic.exAtStateStart_Idle_Logic_9_10_2017_4_41_20_PM_2875.boolValue;

			GoToState ();
		}

		void GoToState ()
		{
			if ( ! boolValues [0])
			{
				return; 
			}

			exAtStateStart_Idle_Logic.exAtStateStart_Idle.ToCounter ();

		}

	}
}
