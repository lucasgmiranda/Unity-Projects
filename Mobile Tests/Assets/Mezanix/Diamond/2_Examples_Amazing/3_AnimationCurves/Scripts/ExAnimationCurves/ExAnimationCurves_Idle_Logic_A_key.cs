using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAnimationCurves_Idle_Logic_A_key 
	{
		ExAnimationCurves_Idle_Logic exAnimationCurves_Idle_Logic;

		public bool boolValue = false;

		public KeyCode keyCode;

		public ExAnimationCurves_Idle_Logic_A_key (ExAnimationCurves_Idle_Logic setExAnimationCurves_Idle_Logic) 
		{
			exAnimationCurves_Idle_Logic = setExAnimationCurves_Idle_Logic;

			exAnimationCurves_Idle_Logic.IAmHere ();

			keyCode = KeyCode.A;
		}

		public void LogicNodeUpdate ()
		{
			ComputeUnityInputClassAndCrossPlatform ();
		}

		void ComputeUnityInputClassAndCrossPlatform ()
		{

			boolValue = Input.GetKeyUp (keyCode);

		}
	}
}
