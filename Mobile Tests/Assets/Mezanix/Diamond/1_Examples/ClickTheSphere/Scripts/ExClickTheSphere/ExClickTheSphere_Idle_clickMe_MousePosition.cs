using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_clickMe_MousePosition 
	{
		ExClickTheSphere_Idle_clickMe exClickTheSphere_Idle_clickMe;

		public Vector3 vector3Value = new Vector3 ();

		public ExClickTheSphere_Idle_clickMe_MousePosition (ExClickTheSphere_Idle_clickMe setExClickTheSphere_Idle_clickMe) 
		{
			exClickTheSphere_Idle_clickMe = setExClickTheSphere_Idle_clickMe;

			exClickTheSphere_Idle_clickMe.IAmHere ();

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
