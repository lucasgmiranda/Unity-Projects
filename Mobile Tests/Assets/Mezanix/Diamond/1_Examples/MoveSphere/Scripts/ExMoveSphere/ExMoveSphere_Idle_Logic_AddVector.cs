using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveSphere_Idle_Logic_AddVector 
	{
		ExMoveSphere_Idle_Logic exMoveSphere_Idle_Logic;

		public bool doIT = false;

		public Vector3 vector3Value = new Vector3 ();

		public Vector3 [] vector3Values = new Vector3[2];

		public ExMoveSphere_Idle_Logic_AddVector (ExMoveSphere_Idle_Logic setExMoveSphere_Idle_Logic) 
		{
			exMoveSphere_Idle_Logic = setExMoveSphere_Idle_Logic;

			exMoveSphere_Idle_Logic.IAmHere ();

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			vector3Values [0] = exMoveSphere_Idle_Logic.exMoveSphere_Idle_Logic_PutX.vector3Value;

			vector3Values [1] = exMoveSphere_Idle_Logic.exMoveSphere_Idle_Logic_PutY.vector3Value;

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
