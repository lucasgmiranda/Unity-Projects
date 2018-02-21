using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveAndColor_Idle_ColorOnPosition_PositionToVector4 
	{
		ExMoveAndColor_Idle_ColorOnPosition exMoveAndColor_Idle_ColorOnPosition;

		public bool doIT = false;

		public Vector3 [] vector3Values = new Vector3[2];

		public float [] floatValues = new float[3];

		public Vector4 vector4Value;

		public ExMoveAndColor_Idle_ColorOnPosition_PositionToVector4 (ExMoveAndColor_Idle_ColorOnPosition setExMoveAndColor_Idle_ColorOnPosition) 
		{
			exMoveAndColor_Idle_ColorOnPosition = setExMoveAndColor_Idle_ColorOnPosition;

			exMoveAndColor_Idle_ColorOnPosition.IAmHere ();

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

			floatValues [0] = 1f;
			floatValues [1] = 1f;
			floatValues [2] = 1f;

		}

		public void LogicNodeUpdate ()
		{
			vector3Values [0] = exMoveAndColor_Idle_ColorOnPosition.exMoveAndColor_Idle_ColorOnPosition_NormalizedPosition.vector3Value;

			doIT = true;

			ComputeVector4 ();
		}

		void ComputeVector4 ()
		{
			if ( ! doIT)
			{
				return;
			}

			vector4Value = new Vector4 (vector3Values [0].x, vector3Values [0].y, vector3Values [0].z, floatValues [0]);

		}
	}
}
