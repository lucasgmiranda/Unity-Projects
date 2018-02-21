using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveAndColor_Idle_Move_RigidBodyVelocity 
	{
		ExMoveAndColor_Idle_Move exMoveAndColor_Idle_Move;

		Rigidbody rigidBody = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public Vector3 [] vector3Values = new Vector3[2];

		public ExMoveAndColor_Idle_Move_RigidBodyVelocity (ExMoveAndColor_Idle_Move setExMoveAndColor_Idle_Move) 
		{
			exMoveAndColor_Idle_Move = setExMoveAndColor_Idle_Move;

			exMoveAndColor_Idle_Move.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("8_4_2017_9_04_47_AM_3788.gameObjectValues_0");
			}

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 0f);

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exMoveAndColor_Idle_Move.exMoveAndColor_Idle.exMoveAndColor.attachedToGameObject;

			vector3Values [0] = exMoveAndColor_Idle_Move.exMoveAndColor_Idle_Move_Speed.vector3Value;

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
