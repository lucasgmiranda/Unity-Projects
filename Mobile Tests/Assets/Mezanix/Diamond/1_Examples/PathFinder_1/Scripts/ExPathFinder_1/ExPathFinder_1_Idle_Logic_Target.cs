using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExPathFinder_1_Idle_Logic_Target 
	{
		ExPathFinder_1_Idle_Logic exPathFinder_1_Idle_Logic;

		Transform transform_ = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public Vector3 vector3Value = new Vector3 ();

		public ExPathFinder_1_Idle_Logic_Target (ExPathFinder_1_Idle_Logic setExPathFinder_1_Idle_Logic) 
		{
			exPathFinder_1_Idle_Logic = setExPathFinder_1_Idle_Logic;

			exPathFinder_1_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("8_6_2017_9_02_07_AM_1288.gameObjectValues_0");
			}

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exPathFinder_1_Idle_Logic.exPathFinder_1_Idle_Logic_TargetsList.gameObjectValue;

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

			vector3Value = transform_.position;

		}
	}
}
