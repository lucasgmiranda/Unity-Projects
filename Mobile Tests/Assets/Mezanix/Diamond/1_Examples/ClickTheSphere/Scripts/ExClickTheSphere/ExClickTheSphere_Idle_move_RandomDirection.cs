using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_move_RandomDirection 
	{
		ExClickTheSphere_Idle_move exClickTheSphere_Idle_move;

		public bool doIT = false;

		public Vector2 vector2Value = new Vector2 ();

		public ExClickTheSphere_Idle_move_RandomDirection (ExClickTheSphere_Idle_move setExClickTheSphere_Idle_move) 
		{
			exClickTheSphere_Idle_move = setExClickTheSphere_Idle_move;

			exClickTheSphere_Idle_move.IAmHere ();

		}

		public void LogicNodeUpdate ()
		{
			doIT = exClickTheSphere_Idle_move.exClickTheSphere_Idle_move_Start.boolValue;

			ComputeVector2 ();
		}

		void ComputeVector2 ()
		{
			if ( ! doIT)
			{
				return;
			}

			Vector3 v3 = UnityEngine.Random.onUnitSphere;
			vector2Value = new Vector2 (v3.x, v3.z).normalized;

		}
	}
}
