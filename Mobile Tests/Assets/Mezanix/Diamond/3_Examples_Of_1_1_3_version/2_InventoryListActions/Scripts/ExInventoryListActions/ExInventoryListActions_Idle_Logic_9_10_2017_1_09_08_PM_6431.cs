using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExInventoryListActions_Idle_Logic_9_10_2017_1_09_08_PM_6431 
	{
		ExInventoryListActions_Idle_Logic exInventoryListActions_Idle_Logic;

		public bool doIT = false;

		public enum InventoryListAction
		{
			Add,

			Remove,

			makeObjectOfFollowingIndexReady,

			nameEntireList,

			RemoveAtThisIndex,
		}
		public InventoryListAction inventoryListAction;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public List <GameObject> gameObjectsListValue = new List <GameObject> ();

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public int intValue = 0;

		public int [] intValues = new int[3];

		public string stringValue;

		public string [] stringValues = new string[2];

		public bool [] boolValues = new bool[2];

		public List <GameObject>[] gameObjectsListValues = new List<GameObject>[2];

		public ExInventoryListActions_Idle_Logic_9_10_2017_1_09_08_PM_6431 (ExInventoryListActions_Idle_Logic setExInventoryListActions_Idle_Logic) 
		{
			exInventoryListActions_Idle_Logic = setExInventoryListActions_Idle_Logic;

			exInventoryListActions_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
			}

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("9_10_2017_1_09_08_PM_7471.gameObjectValues_0");
				gameObjectValue = (GameObject)identifiedObjects.GetIdentifiedObject ("9_10_2017_1_09_08_PM_7471.gameObjectValue");
			}

			intValues [0] = 0;
			intValues [1] = 0;
			intValues [2] = 0;

			intValue = 0;
			stringValues [0] = "";
			stringValues [1] = "";
			stringValue = "";
			inventoryListAction = InventoryListAction.makeObjectOfFollowingIndexReady;

			boolValues [0] = true;
			boolValues [1] = false;
			gameObjectsListValues [0] = new List<GameObject> ();
			gameObjectsListValues [1] = new List<GameObject> ();
		}

		public void LogicNodeUpdate ()
		{
			intValues [0] = exInventoryListActions_Idle_Logic.exInventoryListActions_Idle_Logic_9_10_2017_1_10_05_PM_6744.intValue;

			gameObjectsListValues [0] = exInventoryListActions_Idle_Logic.exInventoryListActions_Idle_Logic_9_10_2017_1_04_24_PM_2638.gameObjectsListValue;

			doIT = exInventoryListActions_Idle_Logic.exInventoryListActions_Idle_Logic_9_10_2017_4_15_47_PM_0650.boolValue;

			ComputeGameObjectList ();
		}

		void InventoryListAction_gameobject_Compute ()
		{
			if (boolValues [0])
			{
				gameObjectsListValue = gameObjectsListValues [0];
			}

			switch (inventoryListAction)
			{
			case InventoryListAction.Add:
				gameObjectsListValue.Add (gameObjectValues [0]);
				break;

			case InventoryListAction.makeObjectOfFollowingIndexReady:
				if (gameObjectsListValue.Count == 0)
					return;

				if (intValues [0] < 0 || intValues [0] > gameObjectsListValue.Count -1)
					return;

				gameObjectValue = gameObjectsListValue [intValues [0]];
				break;

			case InventoryListAction.nameEntireList:
				stringValue = stringValues [0];
				break;

			case InventoryListAction.Remove:
				gameObjectsListValue.Remove (gameObjectValues [0]);
				break;

			case InventoryListAction.RemoveAtThisIndex:
				if (intValues [0] < 0 || intValues [0] > gameObjectsListValue.Count -1)
					return;

				gameObjectsListValue.RemoveAt (intValues [0]);
				break;
			}

			intValue = gameObjectsListValue.Count;
		}

		void ComputeGameObjectList ()
		{
			if ( ! doIT)
			{
				return;
			}

			InventoryListAction_gameobject_Compute ();

		}
	}
}
