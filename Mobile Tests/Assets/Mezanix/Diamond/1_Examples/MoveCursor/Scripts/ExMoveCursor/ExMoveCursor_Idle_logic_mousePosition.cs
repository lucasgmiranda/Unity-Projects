using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveCursor_Idle_logic_mousePosition 
	{
		ExMoveCursor_Idle_logic exMoveCursor_Idle_logic;

		public Vector3 vector3Value = new Vector3 ();

		public ExMoveCursor_Idle_logic_mousePosition (ExMoveCursor_Idle_logic setExMoveCursor_Idle_logic) 
		{
			exMoveCursor_Idle_logic = setExMoveCursor_Idle_logic;

			exMoveCursor_Idle_logic.IAmHere ();

		}

		public void LogicNodeUpdate ()
		{
			ComputeMouseInput ();
		}

		void ComputeMouseInput ()
		{

			Cursor.lockState = CursorLockMode.None;
			vector3Value = Input.mousePosition;

		}
	}
}
