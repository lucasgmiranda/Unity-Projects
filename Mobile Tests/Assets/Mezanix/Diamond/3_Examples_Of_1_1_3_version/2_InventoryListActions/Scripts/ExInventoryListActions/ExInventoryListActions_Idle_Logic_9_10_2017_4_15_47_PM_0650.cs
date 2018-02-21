using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExInventoryListActions_Idle_Logic_9_10_2017_4_15_47_PM_0650 
	{
		ExInventoryListActions_Idle_Logic exInventoryListActions_Idle_Logic;

		public bool boolValue = false;

		public bool [] boolValues = new bool[2];

		public ExInventoryListActions_Idle_Logic_9_10_2017_4_15_47_PM_0650 (ExInventoryListActions_Idle_Logic setExInventoryListActions_Idle_Logic) 
		{
			exInventoryListActions_Idle_Logic = setExInventoryListActions_Idle_Logic;

			exInventoryListActions_Idle_Logic.IAmHere ();

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			boolValues [0] = exInventoryListActions_Idle_Logic.exInventoryListActions_Idle_Logic_9_10_2017_1_03_55_PM_2253.boolValue;

			boolValues [1] = exInventoryListActions_Idle_Logic.exInventoryListActions_Idle_Logic_9_10_2017_4_15_11_PM_6138.boolValue;

			boolValue = (boolValues [0] || boolValues [1]);
		}

	}
}
