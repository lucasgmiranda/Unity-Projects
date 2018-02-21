using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExZoom_deZoom_Logic_setFieldOfView 
	{
		ExZoom_deZoom_Logic exZoom_deZoom_Logic;

		Camera cam;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public float [] floatValues = new float[3];

		public ExZoom_deZoom_Logic_setFieldOfView (ExZoom_deZoom_Logic setExZoom_deZoom_Logic) 
		{
			exZoom_deZoom_Logic = setExZoom_deZoom_Logic;

			exZoom_deZoom_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("7_16_2017_4_47_45_PM_1350.gameObjectValues_0");
			}

			floatValues [0] = 60f;
			floatValues [1] = 1f;
			floatValues [2] = 1f;

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exZoom_deZoom_Logic.exZoom_deZoom.exZoom.attachedToGameObject;

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

			cam.fieldOfView = floatValues [0];

		}
	}
}
