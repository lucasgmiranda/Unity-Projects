using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExDataTransferListener_Idle_Logic_9_10_2017_12_17_08_PM_5612 
	{
		ExDataTransferListener_Idle_Logic exDataTransferListener_Idle_Logic;

		public bool doIT = false;

		public Vector3 [] vector3Values = new Vector3[2];

		public string stringValue;

		public ExDataTransferListener_Idle_Logic_9_10_2017_12_17_08_PM_5612 (ExDataTransferListener_Idle_Logic setExDataTransferListener_Idle_Logic) 
		{
			exDataTransferListener_Idle_Logic = setExDataTransferListener_Idle_Logic;

			exDataTransferListener_Idle_Logic.IAmHere ();

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			vector3Values [0] = exDataTransferListener_Idle_Logic.exDataTransferListener_Idle_Logic_9_10_2017_12_16_20_PM_6657.vector3Value;

			doIT = exDataTransferListener_Idle_Logic.exDataTransferListener_Idle_Logic_9_10_2017_12_15_59_PM_6773.boolValue;

			ComputeVector3 ();
		}

		void ComputeVector3 ()
		{
			if ( ! doIT)
			{
				return;
			}

			stringValue = vector3Values [0].ToString ();

		}
	}
}
