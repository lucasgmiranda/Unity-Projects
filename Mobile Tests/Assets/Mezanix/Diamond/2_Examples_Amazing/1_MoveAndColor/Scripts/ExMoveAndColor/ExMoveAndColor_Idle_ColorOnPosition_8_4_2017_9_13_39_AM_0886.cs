using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveAndColor_Idle_ColorOnPosition_8_4_2017_9_13_39_AM_0886 
	{
		ExMoveAndColor_Idle_ColorOnPosition exMoveAndColor_Idle_ColorOnPosition;

		public bool doIT = false;

		public float floatValue = 0f;

		public float [] floatValues = new float[3];

		public ExMoveAndColor_Idle_ColorOnPosition_8_4_2017_9_13_39_AM_0886 (ExMoveAndColor_Idle_ColorOnPosition setExMoveAndColor_Idle_ColorOnPosition) 
		{
			exMoveAndColor_Idle_ColorOnPosition = setExMoveAndColor_Idle_ColorOnPosition;

			exMoveAndColor_Idle_ColorOnPosition.IAmHere ();

			floatValues [0] = 1f;
			floatValues [1] = 16f;
			floatValues [2] = 1f;

		}

		public void LogicNodeUpdate ()
		{
			floatValues [0] = exMoveAndColor_Idle_ColorOnPosition.exMoveAndColor_Idle_ColorOnPosition_8_4_2017_9_14_26_AM_3386.floatValue;

			doIT = true;

			ComputeFloat ();
		}

		void ComputeFloat ()
		{
			if ( ! doIT)
			{
				return;
			}


			if (floatValues [1] == 0f)
			{
				floatValue = floatValues [0];
			}
			else
			{
				floatValue = floatValues [0] / floatValues [1];
			}

		}
	}
}
