using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExClickTheSphere_Idle_move_SetVelocity 
	{
		ExClickTheSphere_Idle_move exClickTheSphere_Idle_move;

		Rigidbody rigidBody = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public Vector3 [] vector3Values = new Vector3[2];

		ForceMode forceMode;

		public ExClickTheSphere_Idle_move_SetVelocity (ExClickTheSphere_Idle_move setExClickTheSphere_Idle_move) 
		{
			exClickTheSphere_Idle_move = setExClickTheSphere_Idle_move;

			exClickTheSphere_Idle_move.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("7_17_2017_12_12_03_PM_6688.gameObjectValues_0");
			}

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

			forceMode = ForceMode.Impulse;

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exClickTheSphere_Idle_move.exClickTheSphere_Idle.exClickTheSphere.attachedToGameObject;

			vector3Values [0] = exClickTheSphere_Idle_move.exClickTheSphere_Idle_move_Intensity.vector3Value;

			doIT = exClickTheSphere_Idle_move.exClickTheSphere_Idle_move_Start.boolValue;

			ComputeRigidbody ();
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

		void RigidbodyCheck ()
		{
			if ( ! doIT)
			{
				return;
			}

			rigidBody = gameObjectValues [0].GetComponent <Rigidbody> ();

			if (rigidBody == null)
			{
				doIT = false;
			}
		}

		void ComputeRigidbody ()
		{
			GameObjectCheck ();

			RigidbodyCheck ();

			if ( ! doIT)
			{
				return;
			}

			gameObjectValue = gameObjectValues [0];

			rigidBody.AddForce (vector3Values [0], forceMode);

		}
	}
}
