using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveAndColor_Idle_Move_Horizontal 
	{
		ExMoveAndColor_Idle_Move exMoveAndColor_Idle_Move;

		public float floatValue = 0f;

		public string [] stringValues = new string[2];

		public ExMoveAndColor_Idle_Move_Horizontal (ExMoveAndColor_Idle_Move setExMoveAndColor_Idle_Move) 
		{
			exMoveAndColor_Idle_Move = setExMoveAndColor_Idle_Move;

			exMoveAndColor_Idle_Move.IAmHere ();

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
