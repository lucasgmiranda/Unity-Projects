using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExPathFinder_1_Idle_Logic_DivideInt 
	{
		ExPathFinder_1_Idle_Logic exPathFinder_1_Idle_Logic;

		public bool doIT = false;

		public int intValue = 0;

		public int [] intValues = new int[3];

		public ExPathFinder_1_Idle_Logic_DivideInt (ExPathFinder_1_Idle_Logic setExPathFinder_1_Idle_Logic) 
		{
			exPathFinder_1_Idle_Logic = setExPathFinder_1_Idle_Logic;

			exPathFinder_1_Idle_Logic.IAmHere ();

			intValues [0] = 0;
			intValues [1] = 5;
			intValues [2] = 0;

		}

		public void LogicNodeUpdate ()
		{
			intValues [0] = exPathFinder_1_Idle_Logic.exPathFinder_1_Idle_Logic_Increment.intValue;

			doIT = true;

			ComputeInt ();
		}

		void ComputeInt ()
		{
			if ( ! doIT)
			{
				return;
			}


			if (intValues [1] != 0)
			{
				intValue = intValues [0] % intValues [1];
			}
			else
			{
				intValue = intValues [0];
			}

		}
	}
}
