using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_clickMe_Tag 
	{
		ExClickTheSphere_Idle_clickMe exClickTheSphere_Idle_clickMe;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public string [] stringValues = new string[2];

		public bool boolValue = false;

		public ExClickTheSphere_Idle_clickMe_Tag (ExClickTheSphere_Idle_clickMe setExClickTheSphere_Idle_clickMe) 
		{
			exClickTheSphere_Idle_clickMe = setExClickTheSphere_Idle_clickMe;

			exClickTheSphere_Idle_clickMe.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("7_17_2017_12_54_09_PM_7782.gameObjectValues_0");
			}

			stringValues [0] = "Player";
			stringValues [1] = "";
		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_Ray_.raycastHitGameObject;

			doIT = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_LeftClick.boolValue;

			ComputeGameObject ();
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

		void ComputeGameObject ()
		{
			GameObjectCheck ();

			if ( ! doIT)
			{
				return;
			}

			gameObjectValue = gameObjectValues [0];


			boolValue = gameObjectValue.tag == stringValues [0];

		}
	}
}
