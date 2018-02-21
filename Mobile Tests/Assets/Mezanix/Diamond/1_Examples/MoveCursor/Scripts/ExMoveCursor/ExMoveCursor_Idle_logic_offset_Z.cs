using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveCursor_Idle_logic_offset_Z 
	{
		ExMoveCursor_Idle_logic exMoveCursor_Idle_logic;

		public bool doIT = false;

		public Vector3 vector3Value = new Vector3 ();

		public Vector3 [] vector3Values = new Vector3[2];

		public ExMoveCursor_Idle_logic_offset_Z (ExMoveCursor_Idle_logic setExMoveCursor_Idle_logic) 
		{
			exMoveCursor_Idle_logic = setExMoveCursor_Idle_logic;

			exMoveCursor_Idle_logic.IAmHere ();

			vector3Values [0] = new Vector3 (108f, 273f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 10f);

		}

		public void LogicNodeUpdate ()
		{
			vector3Values [0] = exMoveCursor_Idle_logic.exMoveCursor_Idle_logic_mousePosition.vector3Value;

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
