using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExDataTransferSender_Idle_Logic_9_10_2017_12_13_56_PM_7413 
	{
		ExDataTransferSender_Idle_Logic exDataTransferSender_Idle_Logic;

		public string [] stringValues = new string[2];

		public Vector3 [] vector3Values = new Vector3[2];

		public bool doIT = false;

		GameObject mddtGameObjectHolder = null;
		public ExDataTransferSender_Idle_Logic_9_10_2017_12_13_56_PM_7413 (ExDataTransferSender_Idle_Logic setExDataTransferSender_Idle_Logic) 
		{
			exDataTransferSender_Idle_Logic = setExDataTransferSender_Idle_Logic;

			exDataTransferSender_Idle_Logic.IAmHere ();

			stringValues [0] = "sent_vector";
			stringValues [1] = "";
			vector3Values [0] = new Vector3 (12f, 5.4f, 2f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

			mddtGameObjectHolder = GameObject.Find (
				ScriptsCreatedByDiamond.MezanixDiamondDataTransfer.gameObjectHolderName);

		}

		public void LogicNodeUpdate ()
		{
			doIT = exDataTransferSender_Idle_Logic.exDataTransferSender_Idle_Logic_9_10_2017_12_13_25_PM_4364.boolValue;

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

		void MezanixDiamondSetVector3 (string eName)
		{
			ScriptsCreatedByDiamond.MezanixDiamondDataTransfer mddt = Getmddt ();

			if (mddt == null)
				return;

			mddt.SetVector3 (eName, vector3Values [0]);
		}
		void ComputeVector3 ()
		{
			if ( ! doIT)
			{
				return;
			}

			MezanixDiamondSetVector3 (stringValues [0]);


		}
	}
}
