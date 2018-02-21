using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_clickMe_ScreenPointToRay 
	{
		ExClickTheSphere_Idle_clickMe exClickTheSphere_Idle_clickMe;

		Camera cam;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public Vector3 [] vector3Values = new Vector3[2];

		public Ray rayValue;

		public Vector3 rayValueOrigin;

		public Vector3 rayDirectionValueNotNormalized;

		public ExClickTheSphere_Idle_clickMe_ScreenPointToRay (ExClickTheSphere_Idle_clickMe setExClickTheSphere_Idle_clickMe) 
		{
			exClickTheSphere_Idle_clickMe = setExClickTheSphere_Idle_clickMe;

			exClickTheSphere_Idle_clickMe.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("7_17_2017_12_48_43_PM_3204.gameObjectValues_0");
			}

			vector3Values [0] = new Vector3 (414f, 267f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			vector3Values [0] = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_MousePosition.vector3Value;

			doIT = exClickTheSphere_Idle_clickMe.exClickTheSphere_Idle_clickMe_LeftClick.boolValue;

			ComputeCamera ();
		}

		void SetRayValueOrigine (Vector3 v)
		{
			rayValueOrigin = v; 

			rayValue.origin = v;
		}

		void SetRayDirectionValue (Vector3 v)
		{
			rayDirectionValueNotNormalized = v;

			rayValue.direction = v.normalized;
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

			rayValue = cam.ScreenPointToRay (vector3Values [0]);

			SetRayValueOrigine (rayValue.origin);

			SetRayDirectionValue (rayValue.direction);

		}
	}
}
