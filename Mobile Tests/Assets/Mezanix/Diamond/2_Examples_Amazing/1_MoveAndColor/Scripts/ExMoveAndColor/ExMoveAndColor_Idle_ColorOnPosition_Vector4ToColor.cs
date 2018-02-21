using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveAndColor_Idle_ColorOnPosition_Vector4ToColor 
	{
		ExMoveAndColor_Idle_ColorOnPosition exMoveAndColor_Idle_ColorOnPosition;

		public bool doIT = false;

		public Vector4 [] vector4Values = new Vector4[2];

		public Color colorValue;

		public ExMoveAndColor_Idle_ColorOnPosition_Vector4ToColor (ExMoveAndColor_Idle_ColorOnPosition setExMoveAndColor_Idle_ColorOnPosition) 
		{
			exMoveAndColor_Idle_ColorOnPosition = setExMoveAndColor_Idle_ColorOnPosition;

			exMoveAndColor_Idle_ColorOnPosition.IAmHere ();

			vector4Values [0] = new Vector4 (0f, 0f, 0f, 0f);
			vector4Values [1] = new Vector4 (0f, 0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			vector4Values [0] = exMoveAndColor_Idle_ColorOnPosition.exMoveAndColor_Idle_ColorOnPosition_PositionToVector4.vector4Value;

			doIT = true;

			ComputeVector4 ();
		}

		void ComputeVector4 ()
		{
			if ( ! doIT)
			{
				return;
			}

			colorValue = new Color (Mathf.Clamp (vector4Values [0].x, 0f, 1f), Mathf.Clamp (vector4Values [0].y, 0f, 1f), Mathf.Clamp (vector4Values [0].z, 0f, 1f), Mathf.Clamp (vector4Values [0].w, 0f, 1f));

		}
	}
}
