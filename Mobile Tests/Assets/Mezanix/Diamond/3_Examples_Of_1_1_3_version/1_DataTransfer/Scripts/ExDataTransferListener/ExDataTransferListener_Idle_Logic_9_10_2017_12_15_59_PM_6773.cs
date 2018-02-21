using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExDataTransferListener_Idle_Logic_9_10_2017_12_15_59_PM_6773 
	{
		ExDataTransferListener_Idle_Logic exDataTransferListener_Idle_Logic;

		public bool boolValue = false;

		public KeyCode keyCode;

		public ExDataTransferListener_Idle_Logic_9_10_2017_12_15_59_PM_6773 (ExDataTransferListener_Idle_Logic setExDataTransferListener_Idle_Logic) 
		{
			exDataTransferListener_Idle_Logic = setExDataTransferListener_Idle_Logic;

			exDataTransferListener_Idle_Logic.IAmHere ();

			keyCode = KeyCode.L;
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
