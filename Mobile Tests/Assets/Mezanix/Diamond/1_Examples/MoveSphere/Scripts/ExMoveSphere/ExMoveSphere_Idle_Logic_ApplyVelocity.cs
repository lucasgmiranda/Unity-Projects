using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveSphere_Idle_Logic_ApplyVelocity 
	{
		ExMoveSphere_Idle_Logic exMoveSphere_Idle_Logic;

		Rigidbody rigidBody = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public Vector3 [] vector3Values = new Vector3[2];

		public ExMoveSphere_Idle_Logic_ApplyVelocity (ExMoveSphere_Idle_Logic setExMoveSphere_Idle_Logic) 
		{
			exMoveSphere_Idle_Logic = setExMoveSphere_Idle_Logic;

			exMoveSphere_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("7_16_2017_5_11_50_PM_5311.gameObjectValues_0");
			}

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exMoveSphere_Idle_Logic.exMoveSphere_Idle.exMoveSphere.attachedToGameObject;

			vector3Values [0] = exMoveSphere_Idle_Logic.exMoveSphere_Idle_Logic_Speed.vector3Value;

			doIT = true;

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

			rigidBody.velocity = vector3Values [0];

		}
	}
}
