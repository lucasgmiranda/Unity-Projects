using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExInventoryListActions_Idle_Logic_9_10_2017_1_04_24_PM_2638 
	{
		ExInventoryListActions_Idle_Logic exInventoryListActions_Idle_Logic;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public List <GameObject> gameObjectsListValue = new List <GameObject> ();

		public List <GameObject>[] gameObjectsListValues = new List<GameObject>[2];

		public bool [] boolValues = new bool[2];

		public int intValue = 0;

		public ExInventoryListActions_Idle_Logic_9_10_2017_1_04_24_PM_2638 (ExInventoryListActions_Idle_Logic setExInventoryListActions_Idle_Logic) 
		{
			exInventoryListActions_Idle_Logic = setExInventoryListActions_Idle_Logic;

			exInventoryListActions_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectsListValue.Add ((GameObject)identifiedObjects.GetIdentifiedObject ("9_10_2017_1_04_24_PM_4405.gameObjectsList_0"));
				gameObjectsListValue.Add ((GameObject)identifiedObjects.GetIdentifiedObject ("9_10_2017_1_04_24_PM_4405.gameObjectsList_1"));
				gameObjectsListValue.Add ((GameObject)identifiedObjects.GetIdentifiedObject ("9_10_2017_1_04_24_PM_4405.gameObjectsList_2"));
				gameObjectsListValue.Add ((GameObject)identifiedObjects.GetIdentifiedObject ("9_10_2017_1_04_24_PM_4405.gameObjectsList_3"));
				gameObjectsListValue.Add ((GameObject)identifiedObjects.GetIdentifiedObject ("9_10_2017_1_04_24_PM_4405.gameObjectsList_4"));
				gameObjectsListValue.Add ((GameObject)identifiedObjects.GetIdentifiedObject ("9_10_2017_1_04_24_PM_4405.gameObjectsList_5"));
			}

			gameObjectsListValues [0] = new List<GameObject> ();
			gameObjectsListValues [1] = new List<GameObject> ();
			boolValues [0] = false;
			boolValues [1] = false;
		}

		public void LogicNodeUpdate ()
		{
			ComputeGameObjectList ();
		}

		void ForGet_gameObjectList_Compute ()
		{
			if (boolValues [0])
			{
				gameObjectsListValue = gameObjectsListValues [0];
			}

			intValue = gameObjectsListValue.Count;
		}
		void ComputeGameObjectList ()
		{

			ForGet_gameObjectList_Compute ();

		}
	}
}
