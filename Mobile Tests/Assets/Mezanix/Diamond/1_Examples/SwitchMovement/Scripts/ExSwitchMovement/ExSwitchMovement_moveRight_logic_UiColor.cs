using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_moveRight_logic_UiColor 
	{
		ExSwitchMovement_moveRight_logic exSwitchMovement_moveRight_logic;

		UnityEngine.UI.Text unityText = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public Color[] colorValues = new Color[2];

		public ExSwitchMovement_moveRight_logic_UiColor (ExSwitchMovement_moveRight_logic setExSwitchMovement_moveRight_logic) 
		{
			exSwitchMovement_moveRight_logic = setExSwitchMovement_moveRight_logic;

			exSwitchMovement_moveRight_logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("7_17_2017_11_19_22_AM_0781.gameObjectValues_0");
			}

			colorValues [0] = new Color (0.2804961f, 1f, 0.228f, 1f);
			colorValues [1] = new Color (1f, 1f, 1f, 1f);

		}

		public void LogicNodeUpdate ()
		{
			doIT = exSwitchMovement_moveRight_logic.exSwitchMovement_moveRight_logic_tagComparison.boolValue;

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

			unityText.color = colorValues [0];

		}
	}
}
