using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAds_Idle_Ad_Button 
	{
		ExAds_Idle_Ad exAds_Idle_Ad;

		UnityEngine.UI.Button uiButton = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public bool boolValue = false;

		public float [] floatValues = new float[3];

		int downTimeCounterInt;
		public ExAds_Idle_Ad_Button (ExAds_Idle_Ad setExAds_Idle_Ad) 
		{
			exAds_Idle_Ad = setExAds_Idle_Ad;

			exAds_Idle_Ad.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("12_26_2017_11_21_02_AM_5544.gameObjectValues_0");
			}

			floatValues [0] = 1f;
			floatValues [1] = 1f;
			floatValues [2] = 1f;

			downTimeCounterInt = Mathf.CeilToInt (floatValues [0]);
		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exAds_Idle_Ad.exAds_Idle.exAds.attachedToGameObject;

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
				downTimeCounterInt--;

				if (downTimeCounterInt < 0)
				{
					downTimeCounterInt = Mathf.CeilToInt (floatValues [0]);

					boolValue = false;
				}
			}


		}
	}
}
