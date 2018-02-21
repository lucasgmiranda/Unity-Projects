using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace ScriptsCreatedByDiamond 
{
	public class ExKeplerOrbit_Idle_Logic_Ellipse 
	{
		ExKeplerOrbit_Idle_Logic exKeplerOrbit_Idle_Logic;

		Transform transform_ = null;

		public bool doIT = false;

		ScriptsCreatedByDiamond.IdentifiedObjects identifiedObjects = null;

		public GameObject gameObjectValue = null;

		public GameObject [] gameObjectValues = new GameObject[2];

		public Vector3 [] vector3Values = new Vector3[2];

		public Vector4 [] vector4Values = new Vector4[2];

		public float [] floatValues = new float[3];

		public int intValue = 0;


		float keplerRadius = 1f;
		public ExKeplerOrbit_Idle_Logic_Ellipse (ExKeplerOrbit_Idle_Logic setExKeplerOrbit_Idle_Logic) 
		{
			exKeplerOrbit_Idle_Logic = setExKeplerOrbit_Idle_Logic;

			exKeplerOrbit_Idle_Logic.IAmHere ();

			identifiedObjects = GameObject.Find (ScriptsCreatedByDiamond.IdentifiedObjectsActions.gameObjectHolderName).GetComponent<ScriptsCreatedByDiamond.IdentifiedObjects>();

			if (identifiedObjects != null)
			{
				gameObjectValues [0] = (GameObject)identifiedObjects.GetIdentifiedObject ("8_4_2017_6_59_43_PM_5874.gameObjectValues_0");
			}

			vector3Values [0] = new Vector3 (3f, 0f, 0f);
			vector3Values [1] = new Vector3 (0f, 0f, 1f);

			vector4Values [0] = new Vector4 (1f, 0f, 0f, 0.8f);
			vector4Values [1] = new Vector4 (0f, 0f, 0f, 0f);

			floatValues [0] = 8f;
			floatValues [1] = 15f;
			floatValues [2] = 669.1257f;

			intValue = -1;
		}

		public void LogicNodeUpdate ()
		{
			gameObjectValues [0] = exKeplerOrbit_Idle_Logic.exKeplerOrbit_Idle.exKeplerOrbit.attachedToGameObject;

			doIT = true;

			ComputeTransform ();
		}

		void KeplerOrbit ()
		{
			intValue++;

			if (intValue > 10)
				intValue = 1;

			Vector3 focus = vector3Values [0];

			Vector3 pseudoNormal = vector3Values [1].normalized;

			Vector3 aAVu = new Vector3 (vector4Values [0].x, vector4Values [0].y, vector4Values [0].z).normalized;

			float aA = floatValues [0];

			float ecc = Mathf.Clamp (vector4Values [0].w, 0f, 0.9f);

			Vector3 bAV = Vector3.Cross (aAVu, pseudoNormal);

			if (bAV.magnitude == 0f)
				Vector3.Cross (new Vector3 (aAVu.x, aAVu.y, aAVu.z + 10f), pseudoNormal);

			Vector3 bAVu = bAV.normalized;


			if (intValue == 0)
			{
				floatValues [2] = 0f;

				keplerRadius = PolarEllipticEquation (floatValues [2], aA, ecc);
			}


			float dT = 0.02f;

			float tetha_dot = floatValues [1] / (100f * (keplerRadius == 0f?1f:keplerRadius) * dT );

			floatValues [2] = tetha_dot == 0f?1f:tetha_dot * dT + floatValues [2];


			keplerRadius = PolarEllipticEquation (floatValues [2], aA, ecc);

			Vector3 dir = (aAVu * Mathf.Cos (floatValues [2]) + bAVu * Mathf.Sin (floatValues [2])).normalized;

			transform_.position = focus + dir * keplerRadius;
		}
		float PolarEllipticEquation (float angle, float a, float ecc)
		{
			float ecc_2 = ecc*ecc;

			float num = a * (1f - ecc_2);

			float den = 1f + ecc * Mathf.Cos (angle);


			return num / den;
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

			KeplerOrbit ();

		}
	}
}
