using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveCursor_Idle_logic_screenToWorldPoint 
	{
		ExMoveCursor_Idle_logic exMoveCursor_Idle_logic;

		Camera cam;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public Vector3 vector3Value = new Vector3 ();

		public Vector3 [] vector3Values = new Vector3[2];

		public ExMoveCursor_Idle_logic_screenToWorldPoint (ExMoveCursor_Idle_logic setExMoveCursor_Idle_logic) 
		{
			exMoveCursor_Idle_logic = setExMoveCursor_Idle_logic;

			exMoveCursor_Idle_logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("7_17_2017_8_14_08_AM_2631.gameObjectValues_0");
			}

			vector3Values [0] = new Vector3 (108f, 273f, 10f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exMoveCursor_Idle_logic.exMoveCursor_Idle.exMoveCursor.attachedToGameObject;

			vector3Values [0] = exMoveCursor_Idle_logic.exMoveCursor_Idle_logic_offset_Z.vector3Value;

			doIT = true;

			ComputeCamera ();
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

		void CameraCheck ()
		{
			if ( ! doIT)
			{
				return;
			}

			cam = gameObjectValues [0].GetComponent <Camera>();

			if (cam == null)
			{
				doIT = false;
			}
		}

		void ComputeCamera ()
		{
			GameObjectCheck ();

			CameraCheck ();

			if ( ! doIT)
			{
				return;
			}

			gameObjectValue = gameObjectValues [0];

			vector3Value = cam.ScreenToWorldPoint (vector3Values [0]);

		}
	}
}
