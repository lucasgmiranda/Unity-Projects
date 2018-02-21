using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExPathFinder_1_Idle_Logic_TargetsList 
	{
		ExPathFinder_1_Idle_Logic exPathFinder_1_Idle_Logic;

		public bool doIT = false;

		public enum InventoryListAction
		{
			Add,

			Remove,

			makeObjectOfFollowingIndexReady,

			nameEntireList,
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

		public ExPathFinder_1_Idle_Logic_TargetsList (ExPathFinder_1_Idle_Logic setExPathFinder_1_Idle_Logic) 
		{
			exPathFinder_1_Idle_Logic = setExPathFinder_1_Idle_Logic;

			exPathFinder_1_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectsListValue.Add ((GameObject)identifiedObjects.GetIdentifiedObject ("8_6_2017_9_00_09_AM_2012.gameObjectsList_0"));
				gameObjectsListValue.Add ((GameObject)identifiedObjects.GetIdentifiedObject ("8_6_2017_9_00_09_AM_2012.gameObjectsList_1"));
				gameObjectsListValue.Add ((GameObject)identifiedObjects.GetIdentifiedObject ("8_6_2017_9_00_09_AM_2012.gameObjectsList_2"));
				gameObjectsListValue.Add ((GameObject)identifiedObjects.GetIdentifiedObject ("8_6_2017_9_00_09_AM_2012.gameObjectsList_3"));
				gameObjectsListValue.Add ((GameObject)identifiedObjects.GetIdentifiedObject ("8_6_2017_9_00_09_AM_2012.gameObjectsList_4"));
			}

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("8_6_2017_9_00_09_AM_2012.gameObjectValues_0");
				gameObjectValue = (GameObject)identifiedObjects.GetIdentifiedObject ("8_6_2017_9_00_09_AM_2012.gameObjectValue");
			}

			intValues [0] = 0;
			intValues [1] = 0;
			intValues [2] = 0;

			intValue = 5;
			stringValues [0] = "";
			stringValues [1] = "";
			stringValue = "";
			inventoryListAction = InventoryListAction.makeObjectOfFollowingIndexReady;

		}

		public void LogicNodeUpdate ()
		{
			intValues [0] = exPathFinder_1_Idle_Logic.exPathFinder_1_Idle_Logic_DivideInt.intValue;

			doIT = true;

			ComputeGameObjectList ();
		}

		void InventoryListAction_Compute ()
		{
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
			}
		}

		void ComputeGameObjectList ()
		{
			if ( ! doIT)
			{
				return;
			}

			InventoryListAction_Compute ();

		}
	}
}
