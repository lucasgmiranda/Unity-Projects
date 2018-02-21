using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExInventoryListActions_Idle_Logic_9_10_2017_1_10_05_PM_6744 
	{
		ExInventoryListActions_Idle_Logic exInventoryListActions_Idle_Logic;

		public bool doIT = false;

		public int intValue = 0;

		public int [] intValues = new int[3];

		public ExInventoryListActions_Idle_Logic_9_10_2017_1_10_05_PM_6744 (ExInventoryListActions_Idle_Logic setExInventoryListActions_Idle_Logic) 
		{
			exInventoryListActions_Idle_Logic = setExInventoryListActions_Idle_Logic;

			exInventoryListActions_Idle_Logic.IAmHere ();

			intValues [0] = 0;
			intValues [1] = 4;
			intValues [2] = 0;

		}

		public void LogicNodeUpdate ()
		{
			intValues [1] = exInventoryListActions_Idle_Logic.exInventoryListActions_Idle_Logic_9_10_2017_1_04_24_PM_2638.intValue;

			doIT = exInventoryListActions_Idle_Logic.exInventoryListActions_Idle_Logic_9_10_2017_1_03_55_PM_2253.boolValue;

			ComputeInt ();
		}

		void Loop ()
		{
			if (intValues [0] == intValues [1])
			{
				intValue = intValues [0];
			}
			else if (intValues [0] < intValues [1])
			{
				intValue = Mathf.Clamp (intValue, intValues [0], intValues [1]);

				intValue++;

				if (intValue >= intValues [1])
				{
					intValue = intValues [0];
				}
			}
			else if (intValues [0] > intValues [1])
			{
				intValue = Mathf.Clamp (intValue, intValues [1], intValues [0]);

				intValue--;

				if (intValue <= intValues [1])
				{
					intValue = intValues [0];
				}
			}
		}
		void ComputeInt ()
		{
			if ( ! doIT)
			{
				return;
			}


			Loop ();

		}
	}
}
