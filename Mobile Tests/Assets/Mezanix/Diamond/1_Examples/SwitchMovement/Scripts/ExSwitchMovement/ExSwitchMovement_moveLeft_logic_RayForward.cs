using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExSwitchMovement_moveLeft_logic_RayForward 
	{
		ExSwitchMovement_moveLeft_logic exSwitchMovement_moveLeft_logic;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public float [] floatValues = new float[3];

		public int [] intValues = new int[3];

		public bool boolValue = false;

		public string stringValue;

		public ExSwitchMovement_moveLeft_logic_RayForward (ExSwitchMovement_moveLeft_logic setExSwitchMovement_moveLeft_logic) 
		{
			exSwitchMovement_moveLeft_logic = setExSwitchMovement_moveLeft_logic;

			exSwitchMovement_moveLeft_logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("7_17_2017_10_51_42_AM_0534.gameObjectValues_0");
			}

			floatValues [0] = 0.5f;
			floatValues [1] = 1f;
			floatValues [2] = 1f;

			intValues [0] = -1;
			intValues [1] = 0;
			intValues [2] = 0;

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exSwitchMovement_moveLeft_logic.exSwitchMovement_moveLeft.exSwitchMovement.attachedToGameObject;

			doIT = true;

			ComputeGameObject ();
		}

		void OtherGameObjectFoundOnMyWayAtDistance_Compute ()
		{
			Vector3 dori = Vector3.zero;

			Rigidbody rigi = gameObjectValues [0].GetComponent <Rigidbody> ();

			bool noRigi = rigi == null;

			if (noRigi)
				return;

			dori = rigi.velocity.normalized;

			if (Vector3.Dot (dori, dori) == 0f)
				return;

			Vector3 ori = gameObjectValues [0].transform.position;
			
			float gap = 0.03f;

			Collider colli = gameObjectValues [0].GetComponent <Collider> ();

			bool noColli = colli == null;
			if ( ! noColli)
			{
				Bounds boundi = colli.bounds;				

				float semiDiag = (boundi.max - boundi.center).magnitude + gap;
				ori = boundi.center + dori*semiDiag;
			}


			RaycastHit hiti;

			boolValue = false;
			boolValue = Physics.Raycast (ori, dori, out hiti, floatValues [0], intValues [0]);

			//Debug.DrawLine (ori, ori + dori*floatValues [0], Color.cyan, 0.2f);

			gameObjectValue = null;
			stringValue = "";
			if (boolValue)
			{
				if (gameObjectValue == gameObjectValues [0])
				{
					gameObjectValue = null;
				}

				gameObjectValue = hiti.transform.gameObject;

				stringValue = gameObjectValue.tag;
				return;
			}
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

		void ComputeGameObject ()
		{
			GameObjectCheck ();

			if ( ! doIT)
			{
				return;
			}


			OtherGameObjectFoundOnMyWayAtDistance_Compute ();

		}
	}
}
