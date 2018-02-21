using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveSphere_Idle_Logic_Horizontal 
	{
		ExMoveSphere_Idle_Logic exMoveSphere_Idle_Logic;

		public float floatValue = 0f;

		public string [] stringValues = new string[2];

		public ExMoveSphere_Idle_Logic_Horizontal (ExMoveSphere_Idle_Logic setExMoveSphere_Idle_Logic) 
		{
			exMoveSphere_Idle_Logic = setExMoveSphere_Idle_Logic;

			exMoveSphere_Idle_Logic.IAmHere ();

			stringValues [0] = "Horizontal";
			stringValues [1] = "";
		}

		public void LogicNodeUpdate ()
		{
			ComputeUnityInputClassAndCrossPlatform ();
		}

		void ComputeUnityInputClassAndCrossPlatform ()
		{

				floatValue = Input.GetAxis (stringValues [0]);

		}
	}
}
