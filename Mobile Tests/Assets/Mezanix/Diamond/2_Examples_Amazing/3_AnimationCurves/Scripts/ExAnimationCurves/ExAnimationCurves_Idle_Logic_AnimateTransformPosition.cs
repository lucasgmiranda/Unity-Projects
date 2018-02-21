using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExAnimationCurves_Idle_Logic_AnimateTransformPosition 
	{
		ExAnimationCurves_Idle_Logic exAnimationCurves_Idle_Logic;

		Transform transform_ = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public float [] floatValues = new float[3];

		AnimationCurve animationCurve_;
		public bool [] boolValues = new bool[2];

		public Vector3 vector3Value = new Vector3 ();

		public Vector3 [] vector3Values = new Vector3[2];

		public ExAnimationCurves_Idle_Logic_AnimateTransformPosition (ExAnimationCurves_Idle_Logic setExAnimationCurves_Idle_Logic) 
		{
			exAnimationCurves_Idle_Logic = setExAnimationCurves_Idle_Logic;

			exAnimationCurves_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("8_15_2017_10_50_59_AM_1844.gameObjectValues_0");
			}

			floatValues [0] = 1f;
			floatValues [1] = 1f;
			floatValues [2] = 3f;

			boolValues [0] = false;
			animationCurve_ = new AnimationCurve (
				new Keyframe []
				{
					new Keyframe (0f, 0f, 0f, 0f),
					new Keyframe (1f, 1f, 0f, 0f)
				});

			vector3Values [0] = new Vector3 (0f, 0f, 0f);
			vector3Values [1] = new Vector3 (1f, 1.5f, 2f);

			vector3Value = new Vector3 (0f, 0f, 0f);
		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exAnimationCurves_Idle_Logic.exAnimationCurves_Idle.exAnimationCurves.attachedToGameObject;

			floatValues [2] = exAnimationCurves_Idle_Logic.exAnimationCurves_Idle_Logic_AnimationDuration.floatValue;

			doIT = exAnimationCurves_Idle_Logic.exAnimationCurves_Idle_Logic_A_key.boolValue;

			ComputeTransform ();
		}

		IEnumerator RunAnimationCurve_transformPosition ()
		{
			if ( ! boolValues [0])
			{
				boolValues [0] = true;
		
				float t = 0f;
		
				float rate = 1f / floatValues [2];
		
				while (t < 1f)
				{
					t += Time.deltaTime * rate;
		
					transform_.position = Vector3.Lerp (vector3Values [0], vector3Values [1], animationCurve_.Evaluate (t));
		
					yield return 0;
				}
		
				boolValues [0] = false;
			}
			yield return 0;
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

			exAnimationCurves_Idle_Logic.exAnimationCurves_Idle.exAnimationCurves.StartCoroutine (RunAnimationCurve_transformPosition ());

		}
	}
}
