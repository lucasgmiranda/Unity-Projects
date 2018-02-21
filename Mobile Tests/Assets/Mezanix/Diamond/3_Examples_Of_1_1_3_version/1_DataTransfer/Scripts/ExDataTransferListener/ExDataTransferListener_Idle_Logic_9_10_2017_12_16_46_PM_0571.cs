using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExDataTransferListener_Idle_Logic_9_10_2017_12_16_46_PM_0571 
	{
		ExDataTransferListener_Idle_Logic exDataTransferListener_Idle_Logic;

		UnityEngine.UI.Text unityText = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public string [] stringValues = new string[2];

		public ExDataTransferListener_Idle_Logic_9_10_2017_12_16_46_PM_0571 (ExDataTransferListener_Idle_Logic setExDataTransferListener_Idle_Logic) 
		{
			exDataTransferListener_Idle_Logic = setExDataTransferListener_Idle_Logic;

			exDataTransferListener_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("9_10_2017_12_16_46_PM_0483.gameObjectValues_0");
			}

			stringValues [0] = "";
			stringValues [1] = "";
		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exDataTransferListener_Idle_Logic.exDataTransferListener_Idle.exDataTransferListener.attachedToGameObject;

			stringValues [0] = exDataTransferListener_Idle_Logic.exDataTransferListener_Idle_Logic_9_10_2017_12_17_08_PM_5612.stringValue;

			doIT = exDataTransferListener_Idle_Logic.exDataTransferListener_Idle_Logic_9_10_2017_12_15_59_PM_6773.boolValue;

			ComputeUnityText ();
		}

		void GameObjectCheck ()
		{
			if ( ! doIT)
			{
				return;
			}

			if (gameObjectValues [0] == null)
			{
				doIT = false;
			}
		}

		void UnityTextCheck ()
		{
			if ( ! doIT)
			{
				return;
			}

			unityText = gameObjectValues [0].GetComponent <UnityEngine.UI.Text> ();

			if (unityText == null)
			{
				doIT = false;
			}
		}

		void ComputeUnityText ()
		{
			GameObjectCheck ();

			UnityTextCheck ();

			if ( ! doIT)
			{
				return;
			}

			gameObjectValue = gameObjectValues [0];

			unityText.text = stringValues [0];

		}
	}
}
