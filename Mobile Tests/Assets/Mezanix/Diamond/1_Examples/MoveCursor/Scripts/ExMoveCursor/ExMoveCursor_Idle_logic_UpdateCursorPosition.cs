using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveCursor_Idle_logic_UpdateCursorPosition 
	{
		ExMoveCursor_Idle_logic exMoveCursor_Idle_logic;

		Transform transform_ = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public Vector3 [] vector3Values = new Vector3[2];

		public ExMoveCursor_Idle_logic_UpdateCursorPosition (ExMoveCursor_Idle_logic setExMoveCursor_Idle_logic) 
		{
			exMoveCursor_Idle_logic = setExMoveCursor_Idle_logic;

			exMoveCursor_Idle_logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("7_17_2017_8_17_58_AM_3042.gameObjectValues_0");
			}

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			vector3Values [0] = exMoveCursor_Idle_logic.exMoveCursor_Idle_logic_screenToWorldPoint.vector3Value;

			doIT = true;

			ComputeTransform ();
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

		void TransformCheck ()
		{
			if ( ! doIT)
			{
				return;
			}

			transform_ = gameObjectValues [0].GetComponent <Transform> ();

			if (transform_ == null)
			{
				doIT = false;
			}
		}

		void ComputeTransform ()
		{
			GameObjectCheck ();

			TransformCheck ();

			if ( ! doIT)
			{
				return;
			}

			gameObjectValue = gameObjectValues [0];

			transform_.position = vector3Values [0];

		}
	}
}
