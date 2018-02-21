using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExPathFinder_1_Idle_Logic_Or 
	{
		ExPathFinder_1_Idle_Logic exPathFinder_1_Idle_Logic;

		public bool boolValue = false;

		public bool [] boolValues = new bool[2];

		public ExPathFinder_1_Idle_Logic_Or (ExPathFinder_1_Idle_Logic setExPathFinder_1_Idle_Logic) 
		{
			exPathFinder_1_Idle_Logic = setExPathFinder_1_Idle_Logic;

			exPathFinder_1_Idle_Logic.IAmHere ();

			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			boolValues [0] = exPathFinder_1_Idle_Logic.exPathFinder_1_Idle_Logic_Start.boolValue;

			boolValues [1] = exPathFinder_1_Idle_Logic.exPathFinder_1_Idle_Logic_TargetFound.boolValue;

			boolValue = (boolValues [0] || boolValues [1]);
		}

	}
}
