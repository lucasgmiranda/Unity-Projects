using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExPathFinder_1_Idle_Logic_Start 
	{
		ExPathFinder_1_Idle_Logic exPathFinder_1_Idle_Logic;

		public bool boolValue = false;

		public int [] intValues = new int[3];

		public ExPathFinder_1_Idle_Logic_Start (ExPathFinder_1_Idle_Logic setExPathFinder_1_Idle_Logic) 
		{
			exPathFinder_1_Idle_Logic = setExPathFinder_1_Idle_Logic;

			exPathFinder_1_Idle_Logic.IAmHere ();

			boolValue = true;

			intValues [0] = 0;
			intValues [1] = 1;
			intValues [2] = 0;

			intValues [0] = 0;
		}

		public void LogicNodeUpdate ()
		{
			intValues [0]++;

			if (intValues [0] > intValues [1])
			{
				boolValue = false;
			}
		}

	}
}
