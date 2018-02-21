using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveAndColor_Idle_ColorOnPosition_NormalizedPosition 
	{
		ExMoveAndColor_Idle_ColorOnPosition exMoveAndColor_Idle_ColorOnPosition;

		public bool doIT = false;

		public Vector3 vector3Value = new Vector3 ();

		public Vector3 [] vector3Values = new Vector3[2];

		public ExMoveAndColor_Idle_ColorOnPosition_NormalizedPosition (ExMoveAndColor_Idle_ColorOnPosition setExMoveAndColor_Idle_ColorOnPosition) 
		{
			exMoveAndColor_Idle_ColorOnPosition = setExMoveAndColor_Idle_ColorOnPosition;

			exMoveAndColor_Idle_ColorOnPosition.IAmHere ();

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			vector3Values [0] = exMoveAndColor_Idle_ColorOnPosition.exMoveAndColor_Idle_ColorOnPosition_8_4_2017_9_15_42_AM_6474.vector3Value;

			vector3Values [1] = exMoveAndColor_Idle_ColorOnPosition.exMoveAndColor_Idle_ColorOnPosition_8_4_2017_9_17_28_AM_6030.vector3Value;

			doIT = true;

			ComputeVector3 ();
		}

		void ComputeVector3 ()
		{
			if ( ! doIT)
			{
				return;
			}

			vector3Value = vector3Values [0] + vector3Values [1];

		}
	}
}
