using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveSphere_Idle_Logic_Speed 
	{
		ExMoveSphere_Idle_Logic exMoveSphere_Idle_Logic;

		public bool doIT = false;

		public Vector3 vector3Value = new Vector3 ();

		public Vector3 [] vector3Values = new Vector3[2];

		public float [] floatValues = new float[3];

		public ExMoveSphere_Idle_Logic_Speed (ExMoveSphere_Idle_Logic setExMoveSphere_Idle_Logic) 
		{
			exMoveSphere_Idle_Logic = setExMoveSphere_Idle_Logic;

			exMoveSphere_Idle_Logic.IAmHere ();

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

			floatValues [0] = 5f;
			floatValues [1] = 1f;
			floatValues [2] = 1f;

		}

		public void LogicNodeUpdate ()
		{
			floatValues [0] = exMoveSphere_Idle_Logic.exMoveSphere_Idle.exMoveSphere.Idle_Logic_Speed_floatValues_0;

			vector3Values [0] = exMoveSphere_Idle_Logic.exMoveSphere_Idle_Logic_AddVector.vector3Value;

			doIT = true;

			ComputeVector3 ();
		}

		void ComputeVector3 ()
		{
			if ( ! doIT)
			{
				return;
			}

			vector3Value = vector3Values [0] * floatValues [0];

		}
	}
}
