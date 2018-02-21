using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExLoadScene_B_Idle_Logic_Button 
	{
		ExLoadScene_B_Idle_Logic exLoadScene_B_Idle_Logic;

		UnityEngine.UI.Button uiButton = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public bool boolValue = false;

		public float [] floatValues = new float[3];

		float downTimeCounter;

		public ExLoadScene_B_Idle_Logic_Button (ExLoadScene_B_Idle_Logic setExLoadScene_B_Idle_Logic) 
		{
			exLoadScene_B_Idle_Logic = setExLoadScene_B_Idle_Logic;

			exLoadScene_B_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("7_17_2017_4_09_49_PM_1508.gameObjectValues_0");
			}

			floatValues [0] = 3f;
			floatValues [1] = 1f;
			floatValues [2] = 1f;

			downTimeCounter = floatValues [0]*Time.deltaTime;

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exLoadScene_B_Idle_Logic.exLoadScene_B_Idle.exLoadScene_B.attachedToGameObject;

			doIT = true;

			ComputeUiButton ();
		}

		void TaskOnClick()
		{
			boolValue = true;
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

		void UiButtonCheck ()
		{
			if ( ! doIT)
			{
				return;
			}

			uiButton = gameObjectValues [0].GetComponent <UnityEngine.UI.Button> ();

			if (uiButton == null)
			{
				doIT = false;
			}
		}

		void ComputeUiButton ()
		{
			GameObjectCheck ();

			UiButtonCheck ();

			if ( ! doIT)
			{
				return;
			}

			gameObjectValue = gameObjectValues [0];

			uiButton.onClick.AddListener (TaskOnClick);

			if (boolValue)
			{
				downTimeCounter -= Time.deltaTime;

				if (downTimeCounter <= 0f)
				{
					downTimeCounter = floatValues [0]*Time.deltaTime;

					boolValue = false;
				}
			}


		}
	}
}
