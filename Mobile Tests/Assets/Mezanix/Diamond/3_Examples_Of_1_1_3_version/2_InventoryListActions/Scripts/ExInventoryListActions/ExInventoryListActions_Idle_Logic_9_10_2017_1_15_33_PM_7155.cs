using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExInventoryListActions_Idle_Logic_9_10_2017_1_15_33_PM_7155 
	{
		ExInventoryListActions_Idle_Logic exInventoryListActions_Idle_Logic;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public string stringValue;

		public ExInventoryListActions_Idle_Logic_9_10_2017_1_15_33_PM_7155 (ExInventoryListActions_Idle_Logic setExInventoryListActions_Idle_Logic) 
		{
			exInventoryListActions_Idle_Logic = setExInventoryListActions_Idle_Logic;

			exInventoryListActions_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("9_10_2017_1_15_33_PM_4387.gameObjectValues_0");
			}

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exInventoryListActions_Idle_Logic.exInventoryListActions_Idle_Logic_9_10_2017_1_09_08_PM_6431.gameObjectValue;

			doIT = exInventoryListActions_Idle_Logic.exInventoryListActions_Idle_Logic_9_10_2017_4_15_47_PM_0650.boolValue;

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


			stringValue = gameObjectValues [0].name;

		}
	}
}
