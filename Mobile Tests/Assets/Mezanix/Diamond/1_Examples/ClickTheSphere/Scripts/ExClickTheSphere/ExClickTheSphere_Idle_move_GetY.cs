using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_move_GetY 
	{
		ExClickTheSphere_Idle_move exClickTheSphere_Idle_move;

		public bool doIT = false;

		public Vector2 [] vector2Values = new Vector2[2];

		public float floatValue = 0f;

		public ExClickTheSphere_Idle_move_GetY (ExClickTheSphere_Idle_move setExClickTheSphere_Idle_move) 
		{
			exClickTheSphere_Idle_move = setExClickTheSphere_Idle_move;

			exClickTheSphere_Idle_move.IAmHere ();

			vector2Values [0] = new Vector2 (0f, 0f);
			vector2Values [1] = new Vector2 (0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			vector2Values [0] = exClickTheSphere_Idle_move.exClickTheSphere_Idle_move_RandomDirection.vector2Value;

			doIT = exClickTheSphere_Idle_move.exClickTheSphere_Idle_move_Start.boolValue;

			ComputeVector2 ();
		}

		void ComputeVector2 ()
		{
			if ( ! doIT)
			{
				return;
			}

			floatValue = vector2Values [0].y;

		}
	}
}
