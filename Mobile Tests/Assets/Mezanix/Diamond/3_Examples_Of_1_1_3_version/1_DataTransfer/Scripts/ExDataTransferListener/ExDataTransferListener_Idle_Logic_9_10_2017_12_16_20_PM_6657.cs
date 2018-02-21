using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExDataTransferListener_Idle_Logic_9_10_2017_12_16_20_PM_6657 
	{
		ExDataTransferListener_Idle_Logic exDataTransferListener_Idle_Logic;

		public string [] stringValues = new string[2];

		public bool [] boolValues = new bool[2];

		public bool doIT = false;

		GameObject mddtGameObjectHolder = null;
		public Vector3 vector3Value = new Vector3 ();

		public Vector3 [] vector3Values = new Vector3[2];

		public ExDataTransferListener_Idle_Logic_9_10_2017_12_16_20_PM_6657 (ExDataTransferListener_Idle_Logic setExDataTransferListener_Idle_Logic) 
		{
			exDataTransferListener_Idle_Logic = setExDataTransferListener_Idle_Logic;

			exDataTransferListener_Idle_Logic.IAmHere ();

			stringValues [0] = "sent_vector";
			stringValues [1] = "";
			boolValues [0] = false;
			boolValues [1] = false;
			mddtGameObjectHolder = GameObject.Find (
				ScriptsCreatedByDiamond.MezanixDiamondDataTransfer.gameObjectHolderName);

			vector3Values [0] = new Vector3 (0f, 1f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			doIT = exDataTransferListener_Idle_Logic.exDataTransferListener_Idle_Logic_9_10_2017_12_15_59_PM_6773.boolValue;

			ComputeVector3 ();
		}

		ScriptsCreatedByDiamond.MezanixDiamondDataTransfer Getmddt ()
		{
			if ( ! doIT)
				return null;

			if (mddtGameObjectHolder == null)
				mddtGameObjectHolder = GameObject.Find (ScriptsCreatedByDiamond.MezanixDiamondDataTransfer.gameObjectHolderName);

			if (mddtGameObjectHolder == null)
				return null;

			return mddtGameObjectHolder.GetComponent <ScriptsCreatedByDiamond.MezanixDiamondDataTransfer> ();
		}

		Vector3 MezanixDiamondGetVector3 (string eName)
		{
			ScriptsCreatedByDiamond.MezanixDiamondDataTransfer mddt = Getmddt ();

			if (mddt == null)
				return vector3Values [0];

			return mddt.GetVector3 (eName, vector3Values [0]);
		}
		void MezanixDiamondRemoveVector3 (string eName)
		{
			ScriptsCreatedByDiamond.MezanixDiamondDataTransfer mddt = Getmddt ();

			if (mddt == null)
				return;

			mddt.RemoveVector3 (eName);
		}
		void ComputeVector3 ()
		{
			if ( ! doIT)
			{
				return;
			}

			vector3Value = MezanixDiamondGetVector3 (stringValues [0]);
			if (boolValues [0])
			{
				MezanixDiamondRemoveVector3 (stringValues [0]);
			}

		}
	}
}
