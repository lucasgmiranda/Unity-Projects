using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAtStateStart_Counter_Logic_9_10_2017_4_37_16_PM_5421 
	{
		ExAtStateStart_Counter_Logic exAtStateStart_Counter_Logic;

		UnityEngine.UI.Text unityText = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public string [] stringValues = new string[2];

		public ExAtStateStart_Counter_Logic_9_10_2017_4_37_16_PM_5421 (ExAtStateStart_Counter_Logic setExAtStateStart_Counter_Logic) 
		{
			exAtStateStart_Counter_Logic = setExAtStateStart_Counter_Logic;

			exAtStateStart_Counter_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("9_10_2017_4_37_16_PM_5320.gameObjectValues_0");
			}

			stringValues [0] = "";
			stringValues [1] = "";
		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exAtStateStart_Counter_Logic.exAtStateStart_Counter.exAtStateStart.attachedToGameObject;

			stringValues [0] = exAtStateStart_Counter_Logic.exAtStateStart_Counter_Logic_9_10_2017_4_36_58_PM_3513.stringValue;

			doIT = exAtStateStart_Counter_Logic.exAtStateStart_Counter_Logic_9_10_2017_4_36_10_PM_0725.boolValue;

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
