using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExMoveAndColor_Idle_ColorOnPosition_Position 
	{
		ExMoveAndColor_Idle_ColorOnPosition exMoveAndColor_Idle_ColorOnPosition;

		Transform transform_ = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public Vector3 vector3Value = new Vector3 ();

		public ExMoveAndColor_Idle_ColorOnPosition_Position (ExMoveAndColor_Idle_ColorOnPosition setExMoveAndColor_Idle_ColorOnPosition) 
		{
			exMoveAndColor_Idle_ColorOnPosition = setExMoveAndColor_Idle_ColorOnPosition;

			exMoveAndColor_Idle_ColorOnPosition.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("8_4_2017_9_07_39_AM_7567.gameObjectValues_0");
			}

		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exMoveAndColor_Idle_ColorOnPosition.exMoveAndColor_Idle.exMoveAndColor.attachedToGameObject;

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
