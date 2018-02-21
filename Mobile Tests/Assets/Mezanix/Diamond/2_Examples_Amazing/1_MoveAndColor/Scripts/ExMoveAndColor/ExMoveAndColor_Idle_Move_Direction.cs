using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveAndColor_Idle_Move_Direction 
	{
		ExMoveAndColor_Idle_Move exMoveAndColor_Idle_Move;

		public bool doIT = false;

		public Vector3 vector3Value = new Vector3 ();

		public Vector3 [] vector3Values = new Vector3[2];

		public ExMoveAndColor_Idle_Move_Direction (ExMoveAndColor_Idle_Move setExMoveAndColor_Idle_Move) 
		{
			exMoveAndColor_Idle_Move = setExMoveAndColor_Idle_Move;

			exMoveAndColor_Idle_Move.IAmHere ();

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			vector3Values [0] = exMoveAndColor_Idle_Move.exMoveAndColor_Idle_Move_HorizontalToX.vector3Value;

			vector3Values [1] = exMoveAndColor_Idle_Move.exMoveAndColor_Idle_Move_VerticalToY.vector3Value;

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
