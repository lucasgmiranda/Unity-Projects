using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExPathFinder_1_Idle_Logic_TargetFound 
	{
		ExPathFinder_1_Idle_Logic exPathFinder_1_Idle_Logic;

		public bool doIT = false;

		public float [] floatValues = new float[3];

		public bool boolValue = false;

		public ExPathFinder_1_Idle_Logic_TargetFound (ExPathFinder_1_Idle_Logic setExPathFinder_1_Idle_Logic) 
		{
			exPathFinder_1_Idle_Logic = setExPathFinder_1_Idle_Logic;

			exPathFinder_1_Idle_Logic.IAmHere ();

			floatValues [0] = 1f;
			floatValues [1] = 1f;
			floatValues [2] = 1f;

		}

		public void LogicNodeUpdate ()
		{
			floatValues [0] = exPathFinder_1_Idle_Logic.exPathFinder_1_Idle_Logic_RemainingDistance.floatValue;

			doIT = true;

			ComputeFloat ();
		}

		void ComputeFloat ()
		{
			if ( ! doIT)
			{
				return;
			}


			boolValue = floatValues [0] < floatValues [1];

		}
	}
}
