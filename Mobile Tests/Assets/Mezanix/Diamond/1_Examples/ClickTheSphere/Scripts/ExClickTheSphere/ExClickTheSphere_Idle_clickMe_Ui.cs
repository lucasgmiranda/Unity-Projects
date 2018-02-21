using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_clickMe_Ui 
	{
		ExClickTheSphere_Idle_clickMe exClickTheSphere_Idle_clickMe;

		UnityEngine.UI.Text unityText = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public string [] stringValues = new string[2];

		public ExClickTheSphere_Idle_clickMe_Ui (ExClickTheSphere_Idle_clickMe setExClickTheSphere_Idle_clickMe) 
		{
			exClickTheSphere_Idle_clickMe = setExClickTheSphere_Idle_clickMe;

			exClickTheSphere_Idle_clickMe.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("7_17_2017_12_55_33_PM_1006.gameObjectValues_0");
			}

			stringValues [0] = "";
			stringValues [1] = "";
		}

		public void LogicNodeUpdate ()
		{
			stringValues [0] = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_toString.stringValue;

			doIT = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_LeftClick.boolValue;

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
