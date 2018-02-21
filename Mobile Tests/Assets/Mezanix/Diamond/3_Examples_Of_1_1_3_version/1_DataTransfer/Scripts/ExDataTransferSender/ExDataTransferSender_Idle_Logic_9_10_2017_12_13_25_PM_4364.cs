using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExDataTransferSender_Idle_Logic_9_10_2017_12_13_25_PM_4364 
	{
		ExDataTransferSender_Idle_Logic exDataTransferSender_Idle_Logic;

		public bool boolValue = false;

		public KeyCode keyCode;

		public ExDataTransferSender_Idle_Logic_9_10_2017_12_13_25_PM_4364 (ExDataTransferSender_Idle_Logic setExDataTransferSender_Idle_Logic) 
		{
			exDataTransferSender_Idle_Logic = setExDataTransferSender_Idle_Logic;

			exDataTransferSender_Idle_Logic.IAmHere ();

			keyCode = KeyCode.S;
		}

		public void LogicNodeUpdate ()
		{
			ComputeUnityInputClassAndCrossPlatform ();
		}

		void ComputeUnityInputClassAndCrossPlatform ()
		{

			boolValue = Input.GetKeyDown (keyCode);

		}
	}
}
